using Ganss.Xss;

namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece métodos para sanitização de strings.
/// </summary>
public static class Sanitizer
{
    /// <summary>
    /// Sanitiza o HTML mantendo apenas os atributos e tags permitidos no editor Quill
    /// </summary>
    /// <param name="html">HTML a ser sanitizado</param>
    /// <returns>HTML sanitizado</returns>
    public static string SanitizeQuillEditorHtml(string html)
    {
        return InternalSanitizer.SanitizeQuillEditorHtml(html);
    }
}

/// <summary>
/// Classe estática interna que fornece métodos para sanitização de strings.
/// </summary>
internal static class InternalSanitizer
{
    /// <summary>
    /// Sanitiza o HTML mantendo apenas os atributos e tags permitidos no editor Quill
    /// </summary>
    /// <param name="html">HTML a ser sanitizado</param>
    /// <returns>HTML sanitizado</returns>
    public static string SanitizeQuillEditorHtml(string html)
    {
        HtmlSanitizer sanitizer = new();

        sanitizer.AllowedTags.Clear();
        sanitizer.AllowedAttributes.Clear();
        sanitizer.AllowedSchemes.Clear();
        sanitizer.AllowedAtRules.Clear();
        sanitizer.AllowedCssProperties.Clear();

        string[] allowedTags = ["p", "em", "u", "strong", "br", "ol", "ul", "li", "a", "iframe"];
        string[] allowedAttributes = ["class", "href", "src", "rel", "target", "frameborder", "allowfullscreen"];

        foreach (var tag in allowedTags)
        {
            sanitizer.AllowedTags.Add(tag);
        }

        foreach (var attribute in allowedAttributes)
        {
            sanitizer.AllowedAttributes.Add(attribute);
        }

        sanitizer.AllowedSchemes.Add("https");

        sanitizer.RemovingCssClass += (send, args) =>
        {
            if (args.CssClass.StartsWith("ql-"))
                args.Cancel = true;
        };

        return sanitizer.Sanitize(html);
    }
}
