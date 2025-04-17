using System;

namespace Lib20070319.Types
{
    /// <summary>
    /// <b>BGM_ID</b> data type for the 20070319 file format.
    /// </summary>
    public class BGM_ID : IEquatable<BGM_ID>
    {
        /// <summary>
        /// Container ID.
        /// </summary>
        /// <remarks>The container filename usually is the hexadecimal representation of this number.</remarks>
        public short ContainerID { get; set; }

        /// <summary>
        /// Audio cue ID inside the container.
        /// </summary>
        public short CueID { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BGM_ID"/> class.
        /// </summary>
        public BGM_ID()
        {
            ContainerID = -1;
            CueID = -1;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BGM_ID"/> class.
        /// </summary>
        /// <param name="containerID">Container ID.</param>
        /// <param name="cueID">Audio cue ID inside the container.</param>
        public BGM_ID(short containerID, short cueID)
        {
            ContainerID = containerID;
            CueID = cueID;
        }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as BGM_ID);
        }


        /// <inheritdoc/>
        public bool Equals(BGM_ID other)
        {
            return other is not null &&
                   ContainerID == other.ContainerID &&
                   CueID == other.CueID;
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 637210922;
            hashCode = hashCode * -1521134295 + ContainerID.GetHashCode();
            hashCode = hashCode * -1521134295 + CueID.GetHashCode();
            return hashCode;
        }
    }
}
