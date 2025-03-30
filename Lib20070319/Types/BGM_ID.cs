namespace Lib20070319.Types
{
    /// <summary>
    /// <b>BGM_ID</b> data type for the 20070319 file format.
    /// </summary>
    public class BGM_ID
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
    }
}
