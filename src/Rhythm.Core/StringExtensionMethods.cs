namespace Rhythm.Core
{

    // Namespaces.
    using Enums;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensionMethods
    {

        #region Constants

        private const string SnakeCaseReplace = @"-${EDGE}";
        private const string CssReplace = @"-";

        #endregion

        #region Properties

        /// <summary>
        /// This regular expression matches the substrings that should have a dash inserted.
        /// before them.
        /// </summary>
        private static Regex SlugInsertionPointRegex { get; set; }

        /// <summary>
        /// This regular expression matches the a hyphen followed by any character.
        /// before them.
        /// </summary>
        private static Regex DashAndCharRegex { get; set; }

        /// <summary>
        /// This regular expression matches a line (i.e., everything except for line breaks).
        /// </summary>
        private static Regex LineRegex { get; set; }

        /// <summary>
        /// This regular expression matches the invalid CSS characters.
        /// </summary>
        private static Regex InvalidCssChars { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static StringExtensionMethods()
        {
            var options = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase;
            var options2 = RegexOptions.Compiled | RegexOptions.Singleline;
            LineRegex = new Regex(@"((?!\r|\n).)+", options);
            SlugInsertionPointRegex = new Regex(@"(?<EDGE>(?<![A-Z]+|-|^)[A-Z]+)", options2);
            DashAndCharRegex = new Regex("-(?<CHAR>.)", options);
            InvalidCssChars = new Regex(@"((?![a-z0-9]).)+", options);
        }

        #endregion

        #region Extension Methods

        /// <summary>
        /// Splits a string by the specified delimiter.
        /// </summary>
        /// <param name="source">
        /// The string to split.
        /// </param>
        /// <param name="delimiter">
        /// Optional. The delimiter. If unspecified, default delimiters will be used.
        /// </param>
        /// <returns>
        /// The split strings.
        /// </returns>
        public static IEnumerable<string> SplitBy(this string source,
            StringSplitDelimiters delimiter = StringSplitDelimiters.Default)
        {

            // If the source string is null, return an empty collection.
            if (source == null)
            {
                return Enumerable.Empty<string>();
            }

            // Split by delimiter type.
            switch (delimiter)
            {
                case StringSplitDelimiters.Default:
                    var lines = SplitByChars(source, ',', ';')
                        .SelectMany(x => SplitByLineBreaks(x));
                    return CleanItems(lines);
                case StringSplitDelimiters.Comma:
                    return CleanItems(SplitByChars(source, ','));
                case StringSplitDelimiters.LineBreak:
                    return CleanItems(SplitByLineBreaks(source));
                case StringSplitDelimiters.Semicolon:
                    return CleanItems(SplitByChars(source, ';'));
                case StringSplitDelimiters.Tab:
                    return CleanItems(SplitByChars(source, '\t'));
                case StringSplitDelimiters.Equals:
                    return CleanItems(SplitByChars(source, '='));
                case StringSplitDelimiters.BackSlash:
                    return CleanItems(SplitByChars(source, '\\'));
                case StringSplitDelimiters.ForwardSlash:
                    return CleanItems(SplitByChars(source, '/'));
                default:
                    return new[] { source };
            }

        }

        /// <summary>
        /// Converts a camel case string to snake case (e.g., converts "thisThing" to "this-thing").
        /// </summary>
        /// <param name="source">
        /// The camel case string to convert.
        /// </param>
        /// <returns>
        /// The snake case string.
        /// </returns>
        public static string ToSnakeCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }
            return SlugInsertionPointRegex.Replace(source, SnakeCaseReplace).ToLower();
        }

        /// <summary>
        /// Converts a string to camel case (e.g., converts "this-thing" to "thisThing").
        /// </summary>
        /// <param name="source">
        /// The string to convert.
        /// </param>
        /// <returns>
        /// The camel case string.
        /// </returns>
        public static string ToCamelCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            var camelCase = DashAndCharRegex.Replace(source, x =>
            {
                return x.Groups["CHAR"].Value.ToUpper();
            });

            return char.ToLowerInvariant(camelCase[0]) + camelCase.Substring(1);
        }

        /// <summary>
        /// Converts a snake case string to pascal case (e.g., converts "this-thing" to "ThisThing").
        /// </summary>
        /// <param name="source">
        /// The snake case string to convert.
        /// </param>
        /// <returns>
        /// The pascal case string.
        /// </returns>
        public static string ToPascalCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }
            var camelCase = ToCamelCase(source);
            return camelCase.Substring(0, 1).ToUpper() + camelCase.Substring(1);
        }

        /// <summary>
        /// Sanitizes a string for use as a CSS class (e.g., converts
        /// "some randomText" to "some-random-text").
        /// </summary>
        /// <param name="source">
        /// The string to sanitize.
        /// </param>
        /// <returns>
        /// The string that can be used as a CSS class.
        /// </returns>
        public static string SanitizeForCss(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }
            return InvalidCssChars
                .Replace(source, CssReplace)
                .ToSnakeCase()
                .Trim(CssReplace.ToCharArray());
        }

        /// <summary>
        /// Returns either the supplied string or null (in the case that the string is empty
        /// or whitespace).
        /// </summary>
        /// <param name="source">
        /// The string to return.
        /// </param>
        /// <returns>
        /// The specified string, or null.
        /// </returns>
        /// <remarks>
        /// Useful when you want to use the null-coalescing operator and you want to treat an
        /// empty or whitespace string as if it were null.
        /// </remarks>
        public static string PreferNull(this string source)
        {
            return string.IsNullOrWhiteSpace(source)
                ? null
                : source;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Split a string by line breaks.
        /// </summary>
        /// <param name="source">
        /// The string to split.
        /// </param>
        /// <returns>
        /// The split strings.
        /// </returns>
        private static IEnumerable<string> SplitByLineBreaks(string source)
        {
            return LineRegex
                .Matches(source)
                .Cast<Match>()
                .Select(x => x.Value);
        }

        /// <summary>
        /// Split a string by the specified characters.
        /// </summary>
        /// <param name="source">
        /// The string to split.
        /// </param>
        /// <param name="characters">
        /// The characters to split by.
        /// </param>
        /// <returns>
        /// The split strings.
        /// </returns>
        private static IEnumerable<string> SplitByChars(string source, params char[] characters)
        {
            return source.Split(characters);
        }

        /// <summary>
        /// Cleans the collection of strings, trimming whitespace and removing empties.
        /// </summary>
        /// <param name="items">
        /// The items to clean.
        /// </param>
        /// <returns>
        /// The cleaned strings.
        /// </returns>
        private static IEnumerable<string> CleanItems(IEnumerable<string> items)
        {
            return items
                .MakeSafe()
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x));
        }

        #endregion

    }

}