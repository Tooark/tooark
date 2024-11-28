# Tipos Suportados de Arquivos

A biblioteca Tooark oferece métodos para verificar a validade de diferentes tipos de arquivos. Abaixo estão os tipos de arquivos suportados para os tipos padrões:

## Tipos de Arquivos de Imagem

Os seguintes tipos de arquivos de imagem são suportados:

- `.JPG`
- `.JPEG`
- `.PNG`
- `.GIF`
- `.BMP`
- `.SVG`

## Tipos de Arquivos de Documento

Os seguintes tipos de arquivos de documento são suportados:

- `.TXT`
- `.CSV`
- `.LOG`
- `.PDF`
- `.DOC`
- `.DOCX`
- `.XLS`
- `.XLSX`
- `.PPT`
- `.PPTX`

## Tipos de Arquivos de Vídeo

Os seguintes tipos de arquivos de vídeo são suportados:

- `.AVI`
- `.MP4`
- `.MPG`
- `.MPEG`
- `.WMV`

Para verificar a validade de um arquivo, utilize os métodos estáticos na class `Util` em [Tooark/Utils/ValidFile.cs](Tooark/Utils/ValidFile.cs):

- [`IsValidImageFile`](Tooark/Utils/ValidFile.cs): Verifica se o arquivo é uma imagem válida.
- [`IsValidDocumentFile`](Tooark/Utils/ValidFile.cs): Verifica se o arquivo é um documento válido.
- [`IsValidVideoFile`](Tooark/Utils/ValidFile.cs): Verifica se o arquivo é um vídeo válido.
- [`IsValidCustomExtensions`](Tooark/Utils/ValidFile.cs): Verifica se o arquivo é válido com extensões personalizadas.

## Tipo de Arquivo Personalizado

Para verificar a validade de um arquivo com uma extensão personalizada, utilize o método `IsValidCustom` da classe `InternalUtil` em [Tooark/Utils/ValidFile.cs](Tooark/Utils/ValidFile.cs). O método aceita uma lista de extensões personalizadas como parâmetro.

```csharp
using static Tooark.Utils.Util;

string[] customExtensions = { ".EXT1", ".EXT2", ".EXT3" };
bool isValid = IsValidCustomExtensions(filePath, fileSize, customExtensions);
```
