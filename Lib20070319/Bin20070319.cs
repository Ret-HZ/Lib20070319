using System;
using System.Collections.Generic;

namespace Lib20070319
{
    /// <summary>
    /// 20070319 file format.
    /// </summary>
    public class Bin20070319
    {
        internal List<Bin20070319Entry> Entries;

        internal List<Bin20070319Column> Columns;


        /// <summary>
        /// Initializes a new instance of the <see cref="Bin20070319"/> class.
        /// </summary>
        internal Bin20070319()
        {
            Entries = new List<Bin20070319Entry>();
            Columns = new List<Bin20070319Column>();
        }


        /// <summary>
        /// Returns a specific entry.
        /// </summary>
        /// <param name="id">The entry ID.</param>
        /// <returns>A <see cref="Bin20070319Entry"/>.</returns>
        /// <exception cref="IndexOutOfRangeException">There is no entry with the specified ID.</exception>
        public Bin20070319Entry GetEntry(int id)
        {
            if (Entries.Count > id)
            {
                return Entries[id];
            }
            else
            {
                throw new IndexOutOfRangeException($"No entry with ID '{id}'.");
            }
        }


        /// <summary>
        /// Returns all entries.
        /// </summary>
        /// <returns>A <see cref="Bin20070319Entry"/> list.</returns>
        public IReadOnlyList<Bin20070319Entry> GetEntries()
        {
            return Entries.AsReadOnly();
        }


        /// <summary>
        /// Returns a specific column.
        /// </summary>
        /// <param name="name">The column name.</param>
        /// <returns>A <see cref="Bin20070319Column"/> or <see langword="null"/> if no column is found.</returns>
        public Bin20070319Column GetColumn(string name)
        {
            foreach (Bin20070319Column column in Columns)
            {
                if (column.Name.Equals(name))
                {
                    return column;
                }
            }
            return null;
        }


        /// <summary>
        /// Returns all columns.
        /// </summary>
        /// <returns>A <see cref="Bin20070319Column"/> list.</returns>
        public IReadOnlyList<Bin20070319Column> GetColumns()
        {
            return Columns.AsReadOnly();
        }


        /// <summary>
        /// Creates and adds a new entry to the <see cref="Bin20070319"/>.
        /// </summary>
        /// <returns>The newly created <see cref="Bin20070319Entry"/>.</returns>
        public Bin20070319Entry AddEntry()
        {
            Bin20070319Entry entry = new Bin20070319Entry(this);
            foreach(Bin20070319Column column in Columns)
            {
                entry.Data.Add(column.Name, column.GetDefaultValue());
            }
            Entries.Add(entry);
            return entry;
        }


        /// <summary>
        /// Removes the <see cref="Bin20070319Entry"/> matching the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entry to remove.</param>
        /// <returns>A <see cref="bool"/> indicating if the operation completed successfully.</returns>
        public bool RemoveEntry(int id)
        {
            if (Entries.Count > id)
            {
                Entries.RemoveAt(id);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
