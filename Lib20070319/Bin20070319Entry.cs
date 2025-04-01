using System;
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


        /// <summary>
        /// Gets the value for the specified column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <exception cref="InvalidCastException">The column type cannot be converted to the requested type.</exception>
        public T GetValueFromColumn<T>(string columnName)
        {
            object result = GetValueFromColumn(columnName);
            try
            {
                return (T)result;
            }
            catch
            {
                Type paramType = typeof(T);
                throw new InvalidCastException($"Could not cast to the requested type. Expected '{paramType}' but got '{(result == null ? "null" : result.GetType())}' instead.");
            }
        }


        /// <summary>
        /// Gets the value for the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <exception cref="InvalidCastException">The column type cannot be converted to the requested type.</exception>
        public T GetValueFromColumn<T>(Bin20070319Column column)
        {
            return GetValueFromColumn<T>(column.Name);
        }


        /// <summary>
        /// Sets the value for the specified column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="value">The value to write.</param>
        /// <exception cref="InvalidCastException">The column type does not match the type of the provided object and no cast is possible.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The column does not exist.</exception>
        public void SetValueFromColumn(string columnName, object value)
        {
            Bin20070319Column column = Parent.GetColumn(columnName);
            if (column != null)
            {
                // Try to convert the value to the expected type if they dont match
                Type columnType = column.GetDataType();
                if (columnType != value.GetType())
                {
                    try
                    {
                        value = Convert.ChangeType(value, columnType);
                    }
                    catch
                    {
                        throw new InvalidCastException($"Could not cast to the column type. Expected '{columnType}' but got '{value.GetType()}' instead.");
                    }
                }

                Data[columnName] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(columnName, "The column does not exist.");
            }
        }


        /// <summary>
        /// Sets the value for the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value to write.</param>
        /// <exception cref="InvalidCastException">The column type does not match the type of the provided object and no cast is possible.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The column does not exist.</exception>
        public void SetValueFromColumn(Bin20070319Column column, object value)
        {
            SetValueFromColumn(column.Name, value);
        }
    }
}
