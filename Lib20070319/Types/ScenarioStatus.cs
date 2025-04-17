using Lib20070319.Enum;
using System;

namespace Lib20070319.Types
{
    /// <summary>
    /// <b>special_scenariostatus</b> data type for the 20070319 file format.
    /// </summary>
    public class ScenarioStatus : IEquatable<ScenarioStatus>
    {
        /// <summary>
        /// The expected result for the condition evaluation.
        /// </summary>
        public bool ExpectedResult { get; set; }

        /// <summary>
        /// The scenario category index.
        /// </summary>
        public short ScenarioCategory { get; set; }

        /// <summary>
        /// The scenario state index.
        /// </summary>
        /// <remarks>Relative to the category.</remarks>
        public short ScenarioState { get; set; }

        /// <summary>
        /// The next condition to evaluate.
        /// </summary>
        /// <remarks>If there are none, the <see cref="ScenarioStatus"/> value will be <see langword="null"/>.</remarks>
        public (Condition Condition, ScenarioStatus ScenarioStatus) NextCondition { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioStatus"/> class.
        /// </summary>
        public ScenarioStatus()
        {
            ExpectedResult = false;
            ScenarioCategory = -1;
            ScenarioState = -1;
            NextCondition = (Condition.NONE, null);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioStatus"/> class.
        /// </summary>
        /// <param name="expectedResult">The expected result for the condition evaluation.</param>
        /// <param name="scenarioCategory">The scenario category index.</param>
        /// <param name="scenarioState">The scenario state index. (Relative to the scenario category)</param>
        public ScenarioStatus(bool expectedResult, short scenarioCategory, short scenarioState)
        {
            ExpectedResult = expectedResult;
            ScenarioCategory = scenarioCategory;
            ScenarioState = scenarioState;
            NextCondition = (Condition.NONE, null);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioStatus"/> class.
        /// </summary>
        /// <param name="expectedResult">The expected result for the condition evaluation.</param>
        /// <param name="scenarioCategory">The scenario category index.</param>
        /// <param name="scenarioState">The scenario state index. (Relative to the scenario category)</param>
        /// <param name="nextCondition">The next condition to check against.</param>
        public ScenarioStatus(bool expectedResult, short scenarioCategory, short scenarioState, (Condition, ScenarioStatus) nextCondition)
        {
            ExpectedResult = expectedResult;
            ScenarioCategory = scenarioCategory;
            ScenarioState = scenarioState;
            NextCondition = nextCondition;
        }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as ScenarioStatus);
        }


        /// <inheritdoc/>
        public bool Equals(ScenarioStatus other)
        {
            return other is not null &&
                   ExpectedResult == other.ExpectedResult &&
                   ScenarioCategory == other.ScenarioCategory &&
                   ScenarioState == other.ScenarioState &&
                   NextCondition.Condition == other.NextCondition.Condition &&
                   Equals(NextCondition.ScenarioStatus, other.NextCondition.ScenarioStatus);
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -1493135272;
            hashCode = hashCode * -1521134295 + ExpectedResult.GetHashCode();
            hashCode = hashCode * -1521134295 + ScenarioCategory.GetHashCode();
            hashCode = hashCode * -1521134295 + ScenarioState.GetHashCode();
            hashCode = hashCode * -1521134295 + NextCondition.Condition.GetHashCode();
            hashCode = hashCode * -1521134295 + (NextCondition.ScenarioStatus?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}
