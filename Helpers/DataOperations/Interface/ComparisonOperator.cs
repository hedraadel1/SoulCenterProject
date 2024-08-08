/// <summary>
/// This enum represents various comparison operators that can be used in data operations.
/// </summary>
namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Enum for comparison operators.
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// Represents the comparison operator for equality.
        /// </summary>
        Equal,
        /// <summary>
        /// Represents the comparison operator for inequality.
        /// </summary>
        NotEqual,
        /// <summary>
        /// Represents the comparison operator for greater than.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Represents the comparison operator for less than.
        /// </summary>
        LessThan,
        /// <summary>
        /// Represents the comparison operator for greater than or equal.
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// Represents the comparison operator for less than or equal.
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// Represents the comparison operator for contains.
        /// </summary>
        Contains,
        /// <summary>
        /// Represents the comparison operator for starts with.
        /// </summary>
        StartsWith,
        /// <summary>
        /// Represents the comparison operator for ends with.
        /// </summary>
        EndsWith
    }
}