namespace Rhythm.Core.Enums
{

    /// <summary>
    /// The types of delimiters that can be used to split strings.
    /// </summary>
    public enum StringSplitDelimiters
    {

        /// <summary>
        /// Default will split by common delimiters (e.g., commas, line breaks, semicolons).
        /// </summary>
        Default,

        /// <summary>
        /// Split by line breaks.
        /// </summary>
        LineBreak,

        /// <summary>
        /// Split by commas.
        /// </summary>
        Comma,

        /// <summary>
        /// Split by semicolon.
        /// </summary>
        Semicolon,

        /// <summary>
        /// Split by tabs.
        /// </summary>
        Tab,

        /// <summary>
        /// Split by equals signs.
        /// </summary>
        Equals

    }

}