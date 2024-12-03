using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Tooark.Utils;

namespace Tooark.Extensions;

/// <summary>
/// Classe estática que fornece métodos de extensão para operações com IQueryable.
/// Esta classe serve como a interface pública para a aplicação de ordenação dinâmica
/// e outras operações em conjuntos de dados IQueryable.
/// </summary>
public static class QueryableExtensions
{
  /// <summary>
  /// Aplica ordenação dinâmica a uma fonte de dados IQueryable com base no nome da propriedade.
  /// </summary>
  /// <typeparam name="T">O tipo de elemento da fonte de dados.</typeparam>
  /// <param name="source">A fonte de dados IQueryable.</param>
  /// <param name="propertyName">O nome da propriedade para ordenar.</param>
  /// <param name="ascending">Especifica se a ordenação deve ser ascendente (true) ou descendente (false).</param>
  /// <param name="propertyEquals">Nome da propriedade para condição de igualdade adicional (opcional).</param>
  /// <param name="valueEquals">Valor para a condição de igualdade adicional (opcional).</param>
  /// <returns>Um IQueryable ordenado conforme especificado.</returns>
  public static IQueryable<T> OrderByProperty<T>(
    this IQueryable<T> source,
    string? propertyName,
    bool ascending = true,
    string? propertyEquals = null,
    dynamic? valueEquals = null)
  {
    return InternalQueryable.OrderByProperty(source, propertyName, ascending, propertyEquals, valueEquals);
  }
}

/// <summary>
/// Classe estática interna que fornece métodos de extensão para operações com IQueryable.
/// </summary>
internal static class InternalQueryable
{
  private static bool IsCollection { get; set; } = false;
  
  private static readonly MethodInfo SelectMethod = typeof(Enumerable)
    .GetMethods(BindingFlags.Static | BindingFlags.Public)
    .First(m =>
      m.Name == "Select" &&
      m.GetParameters().Length == 2);
  private static readonly MethodInfo FirstOrDefaultMethod = typeof(Enumerable)
    .GetMethods(BindingFlags.Static | BindingFlags.Public)
    .First(m =>
      m.Name == "FirstOrDefault" &&
      m.GetParameters().Length == 1 &&
      m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
  private static readonly MethodInfo WhereMethod = typeof(Enumerable)
    .GetMethods(BindingFlags.Static | BindingFlags.Public)
    .First(m =>
      m.Name == "Where" &&
      m.GetParameters().Length == 2 &&
      m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
      m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>));

  /// <summary>
  /// Implementação interna da ordenação dinâmica de um IQueryable. Esta classe não é exposta publicamente.
  /// </summary>
  /// <typeparam name="T">O tipo de elemento da fonte de dados.</typeparam>
  /// <param name="source">A fonte de dados IQueryable.</param>
  /// <param name="propertyName">O nome da propriedade para ordenar.</param>
  /// <param name="ascending">Especifica se a ordenação deve ser ascendente (true) ou descendente (false).</param>
  /// <param name="propertyEquals">Nome da propriedade para condição de igualdade adicional (opcional).</param>
  /// <param name="valueEquals">Valor para a condição de igualdade adicional (opcional).</param>
  /// <returns>Um IQueryable ordenado conforme especificado.</returns>
  internal static IQueryable<T> OrderByProperty<T>(
    this IQueryable<T> source,
    string? propertyName,
    bool ascending = true,
    string? propertyEquals = null,
    dynamic? valueEquals = null)
  {
    ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
    Expression expression = StartOrderByProperty(parameterExpression, propertyName, propertyEquals, valueEquals);
    var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(expression, typeof(object)), parameterExpression);

    if (IsCollection)
    {
      var queryable = source.AsEnumerable().ToList();
      source = queryable.AsQueryable();
    }

    var order = ascending ? source.OrderBy(lambda) : source.OrderByDescending(lambda);

    return order;
  }

  private static Expression StartOrderByProperty(
    ParameterExpression parameterExpression,
    string? propertyName,
    string? propertyEquals = null,
    dynamic? valueEquals = null
  )
  {
    Expression? expression = null;

    if (!string.IsNullOrEmpty(propertyName))
    {
      expression = BuildExpressionRecursive(parameterExpression, parameterExpression, propertyName.Split('.'), 0, null, propertyEquals, valueEquals);
    }

    expression ??= GetDefaultExpression(parameterExpression);

    return expression;
  }

  private static Expression? BuildExpressionRecursive(
    ParameterExpression parameterExpression,
    Expression expression,
    string[] parts,
    int index,
    LambdaExpression? lambdaNotNull = null,
    string? propertyEquals = null,
    dynamic? valueEquals = null)
  {
    var (nextExpression, nextPropertyInfo) = GetExpressionProperty(expression, parts[index]);

    if (nextExpression == null || nextPropertyInfo == null)
    {
      return parameterExpression != expression ? expression : null!;
    }

    var (tempExpression, lambdaNull) = CheckLengthProperty(expression, nextExpression, index, parts, null, lambdaNotNull);

    if (ConditionalPropertyLength(index, parts))
    {
      return tempExpression;
    }
    else
    {
      if (IsCollectionProperty(nextExpression))
      {
        var elementType = nextPropertyInfo.PropertyType.GetGenericArguments()[0];
        ParameterExpression collectionParameterExpression = Expression.Parameter(elementType, Util.SequentialString(index));

        index++;
        var (collectionExpression, _) = GetExpressionProperty(collectionParameterExpression, parts[index]);

        if (collectionExpression != null)
        {
          // Cria uma lambda para equals
          LambdaExpression? lambdaEquals = GetLambdaEquals(collectionParameterExpression, Util.SequentialString(index), propertyEquals, valueEquals);

          var mountExpression = MountCollection(nextExpression, collectionParameterExpression, collectionExpression, elementType, lambdaEquals);

          collectionExpression = ExpressionConditionalNull(mountExpression, lambdaNull, expressionConditional: collectionExpression);

        }

        IsCollection = true;

        return collectionExpression;
      }
    }
    return BuildExpressionRecursive(parameterExpression, tempExpression, parts, index + 1, lambdaNull, propertyEquals, valueEquals);
  }

  private static Expression GetDefaultExpression(Expression parameterExpression)
  {
    var (expression, _) = GetExpressionProperty(parameterExpression, "Id")!;

    if (expression == null)
    {
      throw new ArgumentException($"NotFoundProperty;{parameterExpression.Type.Name}");
    }

    return expression;
  }

  private static (Expression? Expression, PropertyInfo? PropertyInfo) GetExpressionProperty(Expression? expression, string property)
  {
    if (expression != null)
    {
      PropertyInfo? propertyInfo = expression.Type.GetProperty(property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

      if (propertyInfo != null)
      {
        expression = Expression.Property(expression, propertyInfo);

        return (expression, propertyInfo);
      }
    }

    return (null, null);
  }

  private static (Expression Expression, LambdaExpression? LambdaExpression) CheckLengthProperty(
    Expression expression,
    Expression nextExpression,
    int index,
    string[] parts,
    LambdaExpression? lambdaNull = null,
    LambdaExpression? lambdaNotNull = null)
  {
    if (ConditionalPropertyLength(index, parts))
    {
      nextExpression = ExpressionConditionalNull(nextExpression, lambdaNotNull);
    }
    else
    {
      lambdaNull = GetLambdaNotNull(expression, parts[index]);

      if (lambdaNotNull != null)
      {
        if (lambdaNull != null)
        {
          // Cria uma expressão condicional
          lambdaNull = Expression.Lambda(
            Expression.AndAlso(
              lambdaNotNull.Body,
              lambdaNull.Body),
            lambdaNotNull.Parameters);
        }
        else
        {
          lambdaNull = lambdaNotNull;
        }
      }
    }

    return (nextExpression, lambdaNull);
  }

  private static bool ConditionalPropertyLength(int index, string[] parts)
  {
    var isBigger = index + 1 >= parts.Length;

    return isBigger;
  }

  private static Expression ExpressionConditionalNull(
    Expression expression,
    LambdaExpression? lambdaNotNull = null,
    Expression? expressionNotNull = null,
    Expression? expressionConditional = null)
  {
    expressionNotNull ??= expression;
    expressionConditional ??= expression;

    ConstantExpression constantExpression = GetDefaultValue(expressionConditional.Type);
    Expression? lambdaExpression;

    if (constantExpression.Value == null)
    {
      lambdaExpression = lambdaNotNull?.Body ?? GetLambdaExpressionNull(expressionConditional, equal: false);

      expression = Expression.Condition(
        lambdaExpression,
        expressionNotNull,
        constantExpression);
    }

    return expression;
  }

  private static BinaryExpression GetLambdaExpressionNull(Expression expression, Type? type = null, bool equal = true)
  {
    type ??= expression.Type;

    // Cria uma expressão de igualdade
    var expressionNull = equal ?
      Expression.Equal(expression, Expression.Constant(null, type)) :
      Expression.NotEqual(expression, Expression.Constant(null, type));

    return expressionNull;
  }

  private static LambdaExpression? GetLambdaNotNull(Expression expression, string property)
  {
    var lambdaExactly = GetLambdaExactly(expression, property, null!, false);

    return lambdaExactly;
  }

  private static LambdaExpression? GetLambdaEquals(Expression expression, string property, string? propertyEquals = null, dynamic? valueEquals = null)
  {
    string languageCodeProperty = "LanguageCode";

    if (propertyEquals == null || valueEquals == null)
    {
      if (property == languageCodeProperty)
      {
        return null;
      }

      propertyEquals = languageCodeProperty;
      valueEquals = CultureInfo.CurrentCulture.Name;
    }

    var lambdaExactly = GetLambdaExactly(expression, propertyEquals, valueEquals);

    return lambdaExactly;
  }

  private static LambdaExpression? GetLambdaExactly(Expression expression, string property, dynamic value, bool equal = true)
  {
    var (ExpressionProperty, _) = GetExpressionProperty(expression, property);

    if (ExpressionProperty != null)
    {
      // Cria uma constante para o valor
      var constantValue = Expression.Constant(value, ExpressionProperty.Type);

      // Cria uma expressão de igualdade
      var expressionExactly = equal ?
        Expression.Equal(ExpressionProperty, constantValue) :
        Expression.NotEqual(ExpressionProperty, constantValue);

      var parameterExpression = (ParameterExpression)expression;

      // Retorna a expressão lambda
      var lambdaExactly = Expression.Lambda(expressionExactly, parameterExpression);

      return lambdaExactly;
    }

    return null;
  }

  private static bool IsCollectionProperty(Expression expression)
  {
    var collectionProperty = typeof(System.Collections.IEnumerable).IsAssignableFrom(expression.Type);

    return collectionProperty;
  }

  private static MethodCallExpression MountCollection(
    Expression expression,
    ParameterExpression collectionParameter,
    Expression collectionExpression,
    Type elementType,
    LambdaExpression? lambdaEquals = null)
  {
    Expression defaultExpression = expression;

    if (lambdaEquals != null)
    {
      // Aplica WhereMethod à coleção
      var whereMethod = WhereMethod.MakeGenericMethod(elementType);
      defaultExpression = Expression.Call(whereMethod, defaultExpression, lambdaEquals);
    }

    // Cria uma expressão lambda para o Select, lidando com elementos nulos
    var nullCheckExpression = Expression.Condition(
        Expression.NotEqual(collectionParameter, Expression.Constant(null, elementType)),
        collectionExpression,
        GetDefaultValue(collectionExpression.Type)
    );

    // Cria uma expressão lambda para o Select
    var lambdaSelect = Expression.Lambda(nullCheckExpression, collectionParameter);

    // Aplica Select à coleção
    var selectMethod = SelectMethod.MakeGenericMethod(elementType, collectionExpression.Type);
    var selectExpression = Expression.Call(selectMethod, defaultExpression, lambdaSelect);

    // Aplica FirstOrDefault à coleção
    var firstOrDefaultMethod = FirstOrDefaultMethod.MakeGenericMethod(collectionExpression.Type);
    var firstOrDefaultExpression = Expression.Call(firstOrDefaultMethod, selectExpression);

    return firstOrDefaultExpression;
  }

  private static ConstantExpression GetDefaultValue(Type type)
  {
    if (type == null)
    {
      throw new ArgumentNullException(nameof(type));
    }

    if (!type.IsValueType)
    {
      return Expression.Constant(null, type);
    }

    if (type == typeof(Guid))
    {
      return Expression.Constant(default(Guid), typeof(Guid));
    }

    return Type.GetTypeCode(type) switch
    {
      TypeCode.Boolean => Expression.Constant(default(bool), typeof(bool)),
      TypeCode.Byte => Expression.Constant(default(byte), typeof(byte)),
      TypeCode.SByte => Expression.Constant(default(sbyte), typeof(sbyte)),
      TypeCode.Int32 => Expression.Constant(default(int), typeof(int)),
      TypeCode.UInt32 => Expression.Constant(default(uint), typeof(uint)),
      TypeCode.Int64 => Expression.Constant(default(long), typeof(long)),
      TypeCode.UInt64 => Expression.Constant(default(ulong), typeof(ulong)),
      TypeCode.Single => Expression.Constant(default(float), typeof(float)),
      TypeCode.Double => Expression.Constant(default(double), typeof(double)),
      TypeCode.Decimal => Expression.Constant(default(decimal), typeof(decimal)),
      TypeCode.Char => Expression.Constant(default(char), typeof(char)),
      TypeCode.String => Expression.Constant(default(string), typeof(string)),
      TypeCode.DateTime => Expression.Constant(default(DateTime), typeof(DateTime)),
      _ => Expression.Constant(Activator.CreateInstance(type), type),
    };
  }
}
