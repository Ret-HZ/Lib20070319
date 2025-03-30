using Lib20070319.Enum;
using Lib20070319.Types;
using System;

namespace Lib20070319
{
    /// <summary>
    /// Column for the 20070319 file format.
    /// </summary>
    public class Bin20070319Column
    {
        /// <summary>
        /// Column name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The data type used internally.
        /// </summary>
        public DataType InternalDataType { get; internal set; }

        /// <summary>
        /// Amount of additional data. Usage varies per data type.
        /// </summary>
        internal int AdditionalDataCount { get; set; }

        /// <summary>
        /// Size of the entire data section.
        /// </summary>
        internal int Size { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Bin20070319Column"/> class.
        /// </summary>
        internal Bin20070319Column()
        {

        }


        /// <summary>
        /// Gets the C# type this column's data uses.
        /// </summary>
        /// <returns>A <see cref="Type"/>.</returns>
        public Type GetDataType()
        {
            return InternalDataType switch
            {
                DataType.String => typeof(string),
                DataType.String_tbl => typeof(string),
                DataType.String_idx => typeof(string),
                DataType.Value => typeof(int),
                DataType.Value_tbl => typeof(int),
                DataType.Value_idx => typeof(int),
                DataType.Special_scenariocategory => typeof(int),
                DataType.Special_scenariostatus => typeof(ScenarioStatus),
                DataType.Stageid => typeof(string),
                DataType.Special_scenariocompare => typeof(ScenarioCompare),
                DataType.Special_value => typeof(int),
                DataType.Itemid => typeof(int),
                // DataType.Comment => typeof(string),
                DataType.BGM_ID => typeof(BGM_ID),
                DataType.USE_COUNTER => typeof(int),
                DataType.ENTITY_UID => typeof(int),
                _ => null
            };
        }
    }
}
