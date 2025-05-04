using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class SanitizerTests
{
    // Teste para verificar se a função mantém os atributos/tags válidos
    [Theory]
    [InlineData("<p><a href=\"https://grupojacto.atlassian.net/browse/SINT-659\" rel=\"noopener noreferrer\" target=\"_blank\">Link</a>Link</p>")]
    [InlineData("<iframe class=\"ql-video\" frameborder=\"0\" allowfullscreen=\"true\" src=\"https://player.vimeo.com/video/741893324/\"></iframe>")]
    [InlineData("<p class=\"ql-align-justify\">conteúdo justificado</p>")]
    [InlineData("<ul><li>Item 1</li><li class=\"ql-indent-1\">item 2</li><li class=\"ql-indent-1\">item 3</li></ul>")]
    public void SanitizeQuillHtml_ValidHtml_ReturnsTheSameString(string html)
    {
        var sanitized = Sanitizer.SanitizeQuillEditorHtml(html);
        Assert.Equal(html, sanitized);
    }

    // Teste para verificar se a função remove as tags/atributos inválidos
    [Theory]
    [InlineData("<!DOCTYPE html><html lang=\"en\"><head></head><body></body></html>", "")]
    [InlineData("<script>alert('xss')</script><p></p>", "<p></p>")]
    [InlineData("<iframe src=javascript:alert(String.fromCharCode(88,83,83))></iframe>", "<iframe></iframe>")]
    [InlineData("<p><img src=\"\" alt=\"\">Image</p>", "<p>Image</p>")]
    public void SanitizeQuillHtml_InvalidHtml_ReturnsValidParts(string html, string expected)
    {
        var sanitized = Sanitizer.SanitizeQuillEditorHtml(html);
        var equal = sanitized.Equals(expected);
        Assert.True(equal);
    }
}