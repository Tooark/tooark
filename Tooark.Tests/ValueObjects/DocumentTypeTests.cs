using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;
using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class DocumentTypeTests
{
  // Teste para converter de int para DocumentType e retornar o int e a string corretas.
  [Theory]
  [InlineData(0, "None")]
  [InlineData(1, "CPF")]
  [InlineData(2, "RG")]
  [InlineData(3, "CNH")]
  [InlineData(4, "CNPJ")]
  [InlineData(5, "CPF_CNPJ")]
  [InlineData(6, "CPF_RG")]
  [InlineData(7, "CPF_RG_CNH")]
  public void DocumentType_ShouldBeValid_WhenGivenId(int id, string description)
  {
    // Arrange
    DocumentType documentType = id;

    // Act
    int documentTypeId = documentType;
    string documentTypeDescription = documentType;

    // Assert
    Assert.Equal(id, documentTypeId);
    Assert.Equal(description, documentTypeDescription);
  }

  // Teste para converter de string para DocumentType e retornar a string e o int corretos.
  [Theory]
  [InlineData("None", 0)]
  [InlineData("CPF", 1)]
  [InlineData("RG", 2)]
  [InlineData("CNH", 3)]
  [InlineData("CNPJ", 4)]
  [InlineData("CPF_CNPJ", 5)]
  [InlineData("CPF_RG", 6)]
  [InlineData("CPF_RG_CNH", 7)]
  public void DocumentType_ShouldBeValid_WhenGivenDescription(string description, int id)
  {
    // Arrange
    DocumentType documentType = description;

    // Act
    string documentTypeDescription = documentType;
    int documentTypeId = documentType;

    // Assert
    Assert.Equal(description, documentTypeDescription);
    Assert.Equal(id, documentTypeId);
  }

  // Teste para converter de DocumentType com ToInt.
  [Fact]
  public void DocumentType_ShouldConvertWithToInt()
  {
    // Arrange & Act
    int none = 0;
    int cpf = 1;
    int rg = 2;
    int cnh = 3;
    int cnpj = 4;
    int cpfCnpj = 5;
    int cpfRg = 6;
    int cpfRgCnh = 7;

    // Assert
    Assert.Equal(none, DocumentType.None.ToInt());
    Assert.Equal(cpf, DocumentType.CPF.ToInt());
    Assert.Equal(rg, DocumentType.RG.ToInt());
    Assert.Equal(cnh, DocumentType.CNH.ToInt());
    Assert.Equal(cnpj, DocumentType.CNPJ.ToInt());
    Assert.Equal(cpfCnpj, DocumentType.CPF_CNPJ.ToInt());
    Assert.Equal(cpfRg, DocumentType.CPF_RG.ToInt());
    Assert.Equal(cpfRgCnh, DocumentType.CPF_RG_CNH.ToInt());
  }

  // Teste para converter de DocumentType com ToString.
  [Fact]
  public void DocumentType_ShouldConvertWithToString()
  {
    // Arrange & Act
    string none = "None";
    string cpf = "CPF";
    string rg = "RG";
    string cnh = "CNH";
    string cnpj = "CNPJ";
    string cpfCnpj = "CPF_CNPJ";
    string cpfRg = "CPF_RG";
    string cpfRgCnh = "CPF_RG_CNH";

    // Assert
    Assert.Equal(none, DocumentType.None.ToString());
    Assert.Equal(cpf, DocumentType.CPF.ToString());
    Assert.Equal(rg, DocumentType.RG.ToString());
    Assert.Equal(cnh, DocumentType.CNH.ToString());
    Assert.Equal(cnpj, DocumentType.CNPJ.ToString());
    Assert.Equal(cpfCnpj, DocumentType.CPF_CNPJ.ToString());
    Assert.Equal(cpfRg, DocumentType.CPF_RG.ToString());
    Assert.Equal(cpfRgCnh, DocumentType.CPF_RG_CNH.ToString());
  }

  // Teste para converter de DocumentType com ToString.
  [Fact]
  public void DocumentType_ShouldConvertWithToRegex()
  {
    // Arrange & Act
    string none = @"^[a-zA-Z0-9.-]*$";
    string cpf = RegexPattern.Cpf;
    string rg = RegexPattern.Rg;
    string cnh = RegexPattern.Cnh;
    string cnpj = RegexPattern.Cnpj;
    string cpfCnpj = RegexPattern.CpfCnpj;
    string cpfRg = RegexPattern.CpfRg;
    string cpfRgCnh = RegexPattern.CpfRgCnh;

    // Assert
    Assert.Equal(none, DocumentType.None.ToRegex());
    Assert.Equal(cpf, DocumentType.CPF.ToRegex());
    Assert.Equal(rg, DocumentType.RG.ToRegex());
    Assert.Equal(cnh, DocumentType.CNH.ToRegex());
    Assert.Equal(cnpj, DocumentType.CNPJ.ToRegex());
    Assert.Equal(cpfCnpj, DocumentType.CPF_CNPJ.ToRegex());
    Assert.Equal(cpfRg, DocumentType.CPF_RG.ToRegex());
    Assert.Equal(cpfRgCnh, DocumentType.CPF_RG_CNH.ToRegex());
  }

  // Teste para converter de DocumentType para int.
  [Fact]
  public void DocumentType_ShouldImplicitConversionToInt()
  {
    // Arrange & Act
    int none = DocumentType.None;
    int cpf = DocumentType.CPF;
    int rg = DocumentType.RG;
    int cnh = DocumentType.CNH;
    int cnpj = DocumentType.CNPJ;
    int cpfCnpj = DocumentType.CPF_CNPJ;
    int cpfRg = DocumentType.CPF_RG;
    int cpfRgCnh = DocumentType.CPF_RG_CNH;

    // Assert
    Assert.Equal(0, none);
    Assert.Equal(1, cpf);
    Assert.Equal(2, rg);
    Assert.Equal(3, cnh);
    Assert.Equal(4, cnpj);
    Assert.Equal(5, cpfCnpj);
    Assert.Equal(6, cpfRg);
    Assert.Equal(7, cpfRgCnh);
  }

  // Teste para converter de DocumentType para string.
  [Fact]
  public void DocumentType_ShouldImplicitConversionToString()
  {
    // Arrange & Act
    string none = DocumentType.None;
    string cpf = DocumentType.CPF;
    string rg = DocumentType.RG;
    string cnh = DocumentType.CNH;
    string cnpj = DocumentType.CNPJ;
    string cpfCnpj = DocumentType.CPF_CNPJ;
    string cpfRg = DocumentType.CPF_RG;
    string cpfRgCnh = DocumentType.CPF_RG_CNH;

    // Assert
    Assert.Equal("None", none);
    Assert.Equal("CPF", cpf);
    Assert.Equal("RG", rg);
    Assert.Equal("CNH", cnh);
    Assert.Equal("CNPJ", cnpj);
    Assert.Equal("CPF_CNPJ", cpfCnpj);
    Assert.Equal("CPF_RG", cpfRg);
    Assert.Equal("CPF_RG_CNH", cpfRgCnh);
  }

  // Teste para retornar o tipo de documento "None" quando o id for inválido.
  [Theory]
  [InlineData(-1)]
  [InlineData(8)]
  public void DocumentType_ShouldDefault_WhenIdOutRange(int id)
  {
    // Arrange & Act
    DocumentType documentType = id;

    // Assert
    Assert.Equal(documentType, DocumentType.None);
  }

  // Teste para retornar o tipo de documento "None" quando a descrição for inválida.
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("invalid")]
  [InlineData("unknown")]
  public void DocumentType_ShouldDefault_WhenDescriptionOutRange(string? description)
  {
    // Arrange & Act
    DocumentType documentType = description!;

    // Assert
    Assert.Equal(documentType, DocumentType.None);
  }
}
