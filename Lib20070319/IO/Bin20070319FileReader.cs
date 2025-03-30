using BinaryExtensions;
using Lib20070319.Enum;
using Lib20070319.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lib20070319.IO
{
    public static class Bin20070319FileReader
    {
        static Bin20070319FileReader()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        /// <summary>
        /// Reads a 20070319 file.
        /// </summary>
        /// <param name="stream">The 20070319 file as <see cref="Stream"/>.</param>
        /// <param name="encoding">The encoding to use when reading the file. Some files use UTF-8 while others use Shift-JIS.</param>
        /// <returns>A <see cref="Bin20070319"/> object.</returns>
        public static Bin20070319 ReadBin20070319(Stream stream, Encoding encoding)
        {
            BinaryReader reader = new BinaryReader(stream, encoding, true);
            reader.BaseStream.Seek(0, SeekOrigin.Begin); // Ensure the position is at the start of the stream

            Bin20070319 bin = new Bin20070319();

            int magic = reader.ReadInt32(true);
            if (magic != 537330457)
            {
                throw new Exception("Magic does not correspond to 20070319");
            }

            int columnCount = reader.ReadInt32(true);
            int entryCount = reader.ReadInt32(true);

            // Start of column data
            reader.BaseStream.Seek(0x10);
            List<Bin20070319Column> columns = new List<Bin20070319Column>();
            for (int i = 0; i < columnCount; i++)
            {
                long baseOffset = reader.BaseStream.Position;
                Bin20070319Column column = new Bin20070319Column();
                column.Name = reader.ReadStringNullTerminated();
                reader.BaseStream.Seek(baseOffset + 0x30);
                int columnDataTypeID = reader.ReadInt32(true);
                column.InternalDataType = (DataType)columnDataTypeID;
                column.AdditionalDataCount = reader.ReadInt32(true);
                column.Size = reader.ReadInt32(true);
                reader.ReadBytes(0x4); // Padding/Unused

                bin.Columns.Add(column);
            }

            long columnDataSectionStartOffset = reader.BaseStream.Position;

            // Init entries
            for (int i = 0; i < entryCount; i++)
            {
                Bin20070319Entry entry = new Bin20070319Entry();
                entry.ID = bin.Entries.Count;
                bin.Entries.Add(entry);
            }

            // Read entry data per column
            foreach (Bin20070319Column column in bin.Columns)
            {
                switch (column.InternalDataType)
                {
                    case DataType.String:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                string value = reader.ReadStringNullTerminated();
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.String_tbl:
                        {
                            List<string> stringTable = new List<string>();
                            for (int i = 0; i < column.AdditionalDataCount; i++)
                            {
                                stringTable.Add(reader.ReadStringNullTerminated());
                            }
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                byte stringIndex = reader.ReadByte();
                                entry.Data.Add(column.Name, stringTable[stringIndex]);
                            }
                            break;
                        }

                    case DataType.String_idx:
                        {
                            for (int i = 0; i < column.AdditionalDataCount; i++)
                            {
                                int id = reader.ReadInt16(true);
                                string value = reader.ReadStringNullTerminated();
                                bin.Entries[id].Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Value:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = int.Parse(reader.ReadStringNullTerminated());
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Value_tbl:
                        {
                            List<int> valueTable = new List<int>();
                            for (int i = 0; i < column.AdditionalDataCount; i++)
                            {
                                valueTable.Add(int.Parse(reader.ReadStringNullTerminated()));
                            }
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                byte valueIndex = reader.ReadByte();
                                entry.Data.Add(column.Name, valueTable[valueIndex]);
                            }
                            break;
                        }

                    case DataType.Value_idx:
                        {
                            for (int i = 0; i < column.AdditionalDataCount; i++)
                            {
                                int id = reader.ReadInt16(true);
                                int value = int.Parse(reader.ReadStringNullTerminated());
                                bin.Entries[id].Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Special_scenariocategory:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = reader.ReadInt32(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Special_scenariostatus:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                ScenarioStatus value = null;
                                int stopCheck = reader.ReadInt32(true);
                                if (stopCheck != -1)
                                {
                                    // Go back to read the values properly
                                    reader.BaseStream.Seek(-0x4, SeekOrigin.Current);
                                    value = ReadScenarioStatus(reader);
                                }
                                    entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Stageid:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                string value = reader.ReadStringNullTerminated();
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Special_scenariocompare:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                ScenarioCompare value = new ScenarioCompare();
                                value.ScenarioCategory = reader.ReadInt16(true);
                                value.ScenarioState = reader.ReadInt16(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Special_value:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = reader.ReadInt32(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Itemid:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = reader.ReadInt32(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.Comment:
                        {
                            //TODO: Not used in any file (?)
                            throw new NotImplementedException("The 'Comment' data type is not implemented.");
                        }

                    case DataType.BGM_ID:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                BGM_ID value = new BGM_ID();
                                value.ContainerID = reader.ReadInt16(true);
                                value.CueID = reader.ReadInt16(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.USE_COUNTER:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = reader.ReadInt32(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }

                    case DataType.ENTITY_UID:
                        {
                            foreach (Bin20070319Entry entry in bin.Entries)
                            {
                                int value = reader.ReadInt32(true);
                                entry.Data.Add(column.Name, value);
                            }
                            break;
                        }
                }

                // Adjust section start offset for next column and move to it
                columnDataSectionStartOffset += column.Size;
                reader.BaseStream.Seek(columnDataSectionStartOffset);
            }

            return bin;
        }


        /// <summary>
        /// Reads a 20070319 file.
        /// </summary>
        /// <param name="fileBytes">The 20070319 file as byte array.</param>
        /// <param name="encoding">The encoding to use when reading the file. Some files use UTF-8 while others use Shift-JIS.</param>
        /// <param name="offset">The location in the array to start reading data from.</param>
        /// <param name="length">The number of bytes to read from the array.</param>
        /// <returns>A <see cref="Bin20070319"/> object.</returns>
        public static Bin20070319 ReadBin20070319(byte[] fileBytes, Encoding encoding, int offset = 0, int length = 0)
        {
            if (length == 0) length = fileBytes.Length;
            using (var stream = new MemoryStream(fileBytes, offset, length))
            {
                return ReadBin20070319(stream, encoding);
            }
        }


        /// <summary>
        /// Reads a 20070319 file.
        /// </summary>
        /// <param name="path">The path to the 20070319 file.</param>
        /// <param name="encoding">The encoding to use when reading the file. Some files use UTF-8 while others use Shift-JIS.</param>
        /// <returns>A <see cref="Bin20070319"/> object.</returns>
        public static Bin20070319 ReadBin20070319(string path, Encoding encoding)
        {
            using (Stream stream = new MemoryStream(File.ReadAllBytes(path)))
            {
                return ReadBin20070319(stream, encoding);
            }
        }


        /// <summary>
        /// Reads a <see cref="ScenarioStatus"/> type and its following conditions.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/>.</param>
        /// <returns>A <see cref="ScenarioStatus"/> object.</returns>
        private static ScenarioStatus ReadScenarioStatus(BinaryReader reader)
        {
            ScenarioStatus scenarioStatus = new ScenarioStatus();
            scenarioStatus.ScenarioCategory = reader.ReadInt16(true);
            scenarioStatus.ExpectedResult = (scenarioStatus.ScenarioCategory & 0x8000) != 0;
            scenarioStatus.ScenarioCategory &= 0x7FFF;
            scenarioStatus.ScenarioState = reader.ReadInt16(true);

            Condition nextCondition = (Condition)reader.ReadInt32(true);
            if (nextCondition != Condition.NONE)
            {
                scenarioStatus.NextCondition = (nextCondition, ReadScenarioStatus(reader));
            }
            return scenarioStatus;
        }
    }
}
