namespace Tooark.Tests.Moq.Model.Language;

public class Language
{
  public string LanguageCode { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
}

public class LanguageOnlyLanguageCode
{
  public string LanguageCode { get; set; } = null!;
}

public class LanguageOnlyName
{
  public string Name { get; set; } = null!;
}
