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
        /// <see cref="Bin20070319"/> object this column belongs to.
        /// </summary>
        internal Bin20070319 Parent { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Bin20070319Column"/> class.
        /// </summary>
        /// <param name="parent">The <see cref="Bin20070319"/> object this column belongs to.</param>
        internal Bin20070319Column(Bin20070319 parent)
        {
            Parent = parent;
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


        /// <summary>
        /// Gets the default C# value for this column's data type.
        /// </summary>
        /// <returns>An object corresponding to the column's data type.</returns>
        public object GetDefaultValue()
        {
            return InternalDataType switch
            {
                DataType.String => string.Empty,
                DataType.String_tbl => string.Empty,
                DataType.String_idx => string.Empty,
                DataType.Value => (int)0,
                DataType.Value_tbl => (int)0,
                DataType.Value_idx => null,
                DataType.Special_scenariocategory => (int)-1,
                DataType.Special_scenariostatus => null,
                DataType.Stageid => string.Empty,
                DataType.Special_scenariocompare => new ScenarioCompare(),
                DataType.Special_value => (int)0,
                DataType.Itemid => (int)0,
                // DataType.Comment => string.Empty,
                DataType.BGM_ID => new BGM_ID(),
                DataType.USE_COUNTER => (int)0,
                DataType.ENTITY_UID => (int)-1,
                _ => null
            };
        }


        /// <summary>
        /// Changes this column's data type.
        /// </summary>
        /// <param name="dataType">The new data type.</param>
        /// <remarks>Any existing values that can not be converted to the new data type will be reset to their default instead.</remarks>
        public void ChangeDataType(DataType dataType)
        {
            Type oldType = GetDataType();
            InternalDataType = dataType;
            Type newType = GetDataType();

            if (oldType != newType)
            {
                foreach (Bin20070319Entry entry in Parent.Entries)
                {
                    object value = entry.GetValueFromColumn(Name);

                    try
                    {
                        value = Convert.ChangeType(value, newType);
                    }
                    catch
                    {
                        value = GetDefaultValue();
                    }

                    entry.Data[Name] = value;
                }
            }
        }


        /// <summary>
        /// Renames the column.
        /// </summary>
        /// <param name="newName">The new column name.</param>
        public void Rename(string newName)
        {
            string oldName = Name;
            Name = newName;

            foreach (Bin20070319Entry entry in Parent.Entries)
            {
                if (entry.Data.ContainsKey(oldName))
                {
                    object data = entry.Data[oldName];
                    entry.Data.Add(newName, data);
                    entry.Data.Remove(oldName);
                }
                else
                {
                    entry.Data.Add(newName, GetDefaultValue());
                }
            }
        }
    }
}
