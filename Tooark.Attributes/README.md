# Tooark.Attributes

Biblioteca para criar validadores de atributos para propriedades ou campos.

## Atributos de Validação

### DocumentValidationAttribute

Valida se o valor é um documento válido.

#### Parâmetros

- `EDocumentType type`: Tipo de documento a ser validado.

#### Exemplo de Uso

```csharp
public class Pessoa
{
  [DocumentValidation(EDocumentType.CPF)]
  public string Documento { get; set; }
}
```

### EmailValidationAttribute

Valida se o valor é um endereço de email válido.

#### Exemplo de Uso

```csharp
public class Contato
{
  [EmailValidation]
  public string Email { get; set; }
}
```

### PasswordValidationAttribute

Valida se a senha atende aos critérios de complexidade especificados.

#### Parâmetros

- `bool lowercase`: Exige carácter minúsculo. Padrão: true.
- `bool uppercase`: Exige carácter maiúsculo. Padrão: true.
- `bool number`: Exige carácter numérico. Padrão: true.
- `bool symbol`: Exige carácter especial. Padrão: true.
- `int length`: Tamanho mínimo da senha. Padrão: 8.

#### Exemplo de Uso

```csharp
public class Usuario
{
  [PasswordValidation(lowercase: true, uppercase: true, number: true, symbol: true, length: 8)]
  public string Senha { get; set; }
}
```

### UrlValidationAttribute

Valida se o valor é uma URL válida.

#### Exemplo de Uso

```csharp
public class Website
{
  [UrlValidation]
  public string Url { get; set; }
}
```

### ZipCodeValidationAttribute

Valida se o valor é um código postal válido.

#### Exemplo de Uso

```csharp
public class Endereco
{
  [ZipCodeValidation]
  public string CodigoPostal { get; set; }
}
```
