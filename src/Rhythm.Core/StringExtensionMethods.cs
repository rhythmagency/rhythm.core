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

        #region Properties

        private static Regex LineRegex { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static StringExtensionMethods()
        {
            var options = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase;
            LineRegex = new Regex(@"((?!\r|\n).)+", options);
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
                default:
                    return new[] { source };
            }

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