namespace Lib20070319.Types
{
    /// <summary>
    /// <b>special_scenariocompare</b> data type for the 20070319 file format.
    /// </summary>
    public class ScenarioCompare
    {
        /// <summary>
        /// The scenario category index.
        /// </summary>
        public short ScenarioCategory { get; set; }

        /// <summary>
        /// The scenario state index.
        /// </summary>
        public short ScenarioState { get; set;}


        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioCompare"/> class.
        /// </summary>
        public ScenarioCompare()
        {
            ScenarioCategory = -1;
            ScenarioState = -1;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioCompare"/> class.
        /// </summary>
        /// <param name="scenarioCategory">The scenario category index.</param>
        /// <param name="scenarioState">The scenario state index.</param>
        public ScenarioCompare(short scenarioCategory, short scenarioState)
        {
            ScenarioCategory = scenarioCategory;
            ScenarioState = scenarioState;
        }
    }
}
