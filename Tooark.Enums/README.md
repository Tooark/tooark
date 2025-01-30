# Tooark.Enums

O projeto contém classes que representam tipos enumerados para diferentes propósitos.

## Conteúdo

- [EDocumentType](#edocumenttype)
- [ECloudProvider](#ecloudprovider)

## EDocumentType

A classe `EDocumentType` representa diferentes tipos de documentos e fornece métodos para validação e conversão.

### Tipos de Documentos

- `None`
- `CPF`
- `RG`
- `CNH`
- `CNPJ`
- `CPF_CNPJ`
- `CPF_RG`
- `CPF_RG_CNH`

### Métodos

- `ToString()`: Retorna a descrição do tipo de documento.
- `ToInt()`: Retorna o ID do tipo de documento.
- `ToRegex()`: Retorna o padrão de regex do tipo de documento.
- `IsValid(string)`: Retorna a função de validação do tipo de documento.

### Exemplo de Uso

```csharp
using Tooark.Enums;

class Program
{
  static void Main()
  {
    EDocumentType docType = EDocumentType.CPF;
    Console.WriteLine(docType.ToString()); // Output: CPF
    Console.WriteLine(docType.ToInt()); // Output: 1
    Console.WriteLine(docType.ToRegex()); // Output: RegexPattern.Cpf
    Console.WriteLine(docType.IsValid("12345678909")); // Output: True ou False dependendo da validade do CPF
  }
}
```

## ECloudProvider

A classe `ECloudProvider` representa diferentes provedores de cloud.

### Provedores de Cloud

- `None`
- `Amazon` (AWS)
- `Google` (GCP)
- `Microsoft` (Azure)

### Métodos

- `ToString()`: Retorna a descrição do provedor de cloud.
- `ToInt()`: Retorna o ID do provedor de cloud.

### Exemplo de Uso

```csharp
using Tooark.Enums;

class Program
{
  static void Main()
  {
    ECloudProvider cloudProvider = ECloudProvider.Amazon;
    Console.WriteLine(cloudProvider.ToString()); // Output: AWS
    Console.WriteLine(cloudProvider.ToInt()); // Output: 1
  }
}
```
