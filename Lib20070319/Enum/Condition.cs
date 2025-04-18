namespace Lib20070319.Enum
{
    /// <summary>
    /// Conditions used in the <see cref="Types.ScenarioStatus"/> data type.
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// A condition should be present but none was found.
        /// </summary>
        ERROR = 0,

        /// <summary>
        /// No condition. Used as a stop code to stop processing further conditions.
        /// </summary>
        NONE = -1,

        /// <summary>
        /// AND condition.
        /// </summary>
        AND = -2,

        /// <summary>
        /// OR condition.
        /// </summary>
        OR = -3,
    }
}
