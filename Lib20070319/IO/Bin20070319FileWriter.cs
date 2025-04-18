using BinaryExtensions;
using Lib20070319.Enum;
using Lib20070319.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lib20070319.IO
{
    public static class Bin20070319FileWriter
    {
        static Bin20070319FileWriter()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        /// <summary>
        /// Writes a <see cref="Bin20070319"/> to the specified <see cref="Stream"/>.
        /// </summary>
        /// <param name="bin20070319">The <see cref="Bin20070319"/> to write.</param>
        /// <param name="stream">The destination <see cref="Stream"/>.</param>
        /// <param name="encoding">The encoding to use when writing the file. Some files use UTF-8 while others use Shift-JIS.</param>
        public static void WriteBin20070319(Bin20070319 bin20070319, Stream stream, Encoding encoding)
        {
            BinaryWriter writer = new BinaryWriter(stream, encoding, true);

            writer.Write(0x20070319, true); // Magic
            writer.Write(bin20070319.Columns.Count, true); // Column count
            writer.Write(bin20070319.Entries.Count, true); // Entry count
            writer.Write(0); // Padding

            // Placeholder column section
            writer.WriteTimes(0x00, 0x40 * bin20070319.Columns.Count);

            // Entry data
            long columnDataOffset = 0x10;
            foreach (Bin20070319Column column in bin20070319.Columns)
            {
                long sectionStartOffset = stream.Position;

                switch (column.InternalDataType)
                {
                    case DataType.String:
                    case DataType.Value:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                string value = (string)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.String_tbl:
                    case DataType.Value_tbl:
                        {
                            List<string> strings = new List<string>();
                            List<byte> indices = new List<byte>();
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                string value = (string)entry.GetValueFromColumn(column);
                                if (!strings.Contains(value))
                                {
                                    strings.Add(value);
                                    writer.Write(value, true);
                                }
                                indices.Add((byte)strings.IndexOf(value));
                            }
                            foreach (byte index in indices)
                            {
                                writer.Write(index);
                            }
                            column.AdditionalDataCount = strings.Count;
                            break;
                        }

                    case DataType.String_idx:
                    case DataType.Value_idx:
                        {
                            column.AdditionalDataCount = 0;
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                object value = entry.GetValueFromColumn(column);
                                if (value != null)
                                {
                                    writer.Write((short)entry.ID, true);
                                    writer.Write((string)value, true);
                                    column.AdditionalDataCount += 1;
                                }
                            }
                            break;
                        }

                    case DataType.Special_scenariocategory:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                int value = (int)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Special_scenariostatus:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                object value = entry.GetValueFromColumn(column);
                                if (value == null)
                                {
                                    writer.Write(-1, true);
                                }
                                else
                                {
                                    ScenarioStatus scenarioStatus = (ScenarioStatus)value;
                                    WriteScenarioStatus(writer, scenarioStatus);
                                }
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Stageid:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                string value = (string)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Special_scenariocompare:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                ScenarioCompare value = (ScenarioCompare)entry.GetValueFromColumn(column);
                                writer.Write(value.ScenarioCategory, true);
                                writer.Write(value.ScenarioState, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Special_value:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                int value = (int)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Itemid:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                int value = (int)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.Comment:
                        {
                            // The "Comment" data type is not used in any file.
                            break;
                        }

                    case DataType.BGM_ID:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                BGM_ID value = (BGM_ID)entry.GetValueFromColumn(column);
                                writer.Write(value.ContainerID, true);
                                writer.Write(value.CueID, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.USE_COUNTER:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                int value = (int)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }

                    case DataType.ENTITY_UID:
                        {
                            foreach (Bin20070319Entry entry in bin20070319.Entries)
                            {
                                int value = (int)entry.GetValueFromColumn(column);
                                writer.Write(value, true);
                            }
                            column.AdditionalDataCount = bin20070319.Entries.Count;
                            break;
                        }
                }

                // Align to 4 bytes
                writer.WritePadding(0x00, 0x4);

                long sectionEndOffset = stream.Position;

                // Column information
                writer.BaseStream.PushToPosition(columnDataOffset);
                writer.Write(column.Name, true); // Name
                writer.BaseStream.Position = columnDataOffset + 0x30;
                writer.Write((int)column.InternalDataType, true); // Data type ID
                writer.Write(column.AdditionalDataCount, true); // Additional data
                writer.Write((int)(sectionEndOffset - sectionStartOffset), true); // Section size
                writer.Write(0); // Padding
                columnDataOffset += 0x40;

                writer.BaseStream.PopPosition();
            }

            // Reset the stream position
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
        }


        /// <summary>
        /// Writes a <see cref="Bin20070319"/> to a file.
        /// </summary>
        /// <param name="bin20070319">The <see cref="Bin20070319"/> to write.</param>
        /// <param name="path">The destination file path.</param>
        /// <param name="encoding">The encoding to use when writing the file. Some files use UTF-8 while others use Shift-JIS.</param>
        public static void WriteBin20070319ToFile(Bin20070319 bin20070319, string path, Encoding encoding)
        {
            using (Stream stream = new MemoryStream())
            {
                WriteBin20070319(bin20070319, stream, encoding);
                File.WriteAllBytes(path, stream.ToArray());
            }
        }


        /// <summary>
        /// Writes a <see cref="Bin20070319"/> to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="bin20070319">The <see cref="Bin20070319"/> to write.</param>
        /// <param name="encoding">The encoding to use when writing the file. Some files use UTF-8 while others use Shift-JIS.</param>
        public static Stream WriteBin20070319ToStream(Bin20070319 bin20070319, Encoding encoding)
        {
            Stream stream = new MemoryStream();
            WriteBin20070319(bin20070319, stream, encoding);
            return stream;
        }


        /// <summary>
        /// Writes a <see cref="Bin20070319"/> to a byte array.
        /// </summary>
        /// <param name="bin20070319">The <see cref="Bin20070319"/> to write.</param>
        /// <param name="encoding">The encoding to use when writing the file. Some files use UTF-8 while others use Shift-JIS.</param>
        public static byte[] WriteBin20070319ToArray(Bin20070319 bin20070319, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                WriteBin20070319(bin20070319, stream, encoding);
                return stream.ToArray();
            }
        }


        /// <summary>
        /// Writes a <see cref="ScenarioStatus"/> type and its following conditions.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/>.</param>
        /// <param name="scenarioStatus">The <see cref="ScenarioStatus"/> to write.</param>
        private static void WriteScenarioStatus(BinaryWriter writer, ScenarioStatus scenarioStatus)
        {
            short scenarioCategory = scenarioStatus.ScenarioCategory;
            if (scenarioStatus.ExpectedResult)
            {
                scenarioCategory |= unchecked((short)0x8000);
            }
            writer.Write(scenarioCategory, true);
            writer.Write(scenarioStatus.ScenarioState, true);
            if (scenarioStatus.NextCondition.ScenarioStatus != null && scenarioStatus.NextCondition.Condition != Condition.NONE)
            {
                // Handle broken conditions. Do not write the condition ID if there is none.
                if (scenarioStatus.NextCondition.Condition != Condition.ERROR)
                {
                    writer.Write((int)scenarioStatus.NextCondition.Condition, true);
                }
                WriteScenarioStatus(writer, scenarioStatus.NextCondition.ScenarioStatus);
            }
            else
            {
                writer.Write((int)Condition.NONE, true);
            }
        }
    }
}
