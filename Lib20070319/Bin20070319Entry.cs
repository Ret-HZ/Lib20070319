using System.Collections.Generic;

namespace Lib20070319
{
    /// <summary>
    /// Entry for the 20070319 file format.
    /// </summary>
    public class Bin20070319Entry
    {
        /// <summary>
        /// Entry ID.
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// Entry data per column. (ColumnName : Data)
        /// </summary>
        internal Dictionary<string, object> Data;

        /// <summary>
        /// <see cref="Bin20070319"/> object this entry belongs to.
        /// </summary>
        internal Bin20070319 Parent { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Bin20070319Entry"/> class.
        /// </summary>
        internal Bin20070319Entry()
        {
            Data = new Dictionary<string, object>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Bin20070319Entry"/> class.
        /// </summary>
        /// <param name="parent">The <see cref="Bin20070319"/> object this entry belongs to.</param>
        internal Bin20070319Entry(Bin20070319 parent) : this()
        {
            Parent = parent;
        }


        /// <summary>
        /// Gets the value for the specified column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>An object whose type matches the corresponding column's data type. May be <see langword="null"/> if there is no value for the specified column.</returns>
        public object GetValueFromColumn(string columnName)
        {
            if (Data.ContainsKey(columnName))
            {
                return Data[columnName];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the value for the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>An object whose type matches the corresponding column's data type. May be <see langword="null"/> if there is no value for the specified column.</returns>
        public object GetValueFromColumn(Bin20070319Column column)
        {
            return GetValueFromColumn(column.Name);
        }
    }
}
