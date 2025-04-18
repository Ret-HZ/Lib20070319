namespace Lib20070319.Enum
{
    /// <summary>
    /// Internal data type IDs used in the 20070319 format.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// C# type is <see cref="string"/>.
        /// </summary>
        String                      = 0x0,

        /// <summary>
        /// C# type is <see cref="string"/>.
        /// </summary>
        String_tbl                  = 0x1,

        /// <summary>
        /// C# type is <see cref="string"/>. Can be <see langword="null"/>.
        /// </summary>
        String_idx                  = 0x2,

        /// <summary>
        /// C# type is <see cref="string"/>.
        /// </summary>
        Value                       = 0x3,

        /// <summary>
        /// C# type is <see cref="string"/>.
        /// </summary>
        Value_tbl                   = 0x4,

        /// <summary>
        /// C# type is <see cref="string"/>. Can be <see langword="null"/>.
        /// </summary>
        Value_idx                   = 0x5,

        /// <summary>
        /// C# type is <see cref="int"/>.
        /// </summary>
        Special_scenariocategory    = 0x6,

        /// <summary>
        /// C# type is <see cref="Types.ScenarioStatus"/>. Can be <see langword="null"/>.
        /// </summary>
        Special_scenariostatus      = 0x7,

        /// <summary>
        /// C# type is <see cref="string"/>.
        /// </summary>
        Stageid                     = 0x8,

        /// <summary>
        /// C# type is <see cref="Types.ScenarioCompare"/>.
        /// </summary>
        Special_scenariocompare     = 0x9,

        /// <summary>
        /// C# type is <see cref="int"/>.
        /// </summary>
        Special_value               = 0xA,

        /// <summary>
        /// C# type is <see cref="int"/>.
        /// </summary>
        Itemid                      = 0xB,

        /// <summary>
        /// C# type is <see langword="null"/>. This type is not implemented.
        /// </summary>
        Comment                     = 0xC,

        /// <summary>
        /// C# type is <see cref="Types.BGM_ID"/>.
        /// </summary>
        BGM_ID                      = 0xD,

        /// <summary>
        /// C# type is <see cref="int"/>.
        /// </summary>
        USE_COUNTER                 = 0xF,

        /// <summary>
        /// C# type is <see cref="int"/>.
        /// </summary>
        ENTITY_UID                  = 0x10,
    }
}
