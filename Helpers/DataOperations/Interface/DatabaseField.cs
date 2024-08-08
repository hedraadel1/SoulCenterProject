namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Represents a field in a database table.
    /// </summary>
    public class DatabaseField
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DatabaseField()
        {
            // Default values
            Name = string.Empty;
            DataType = string.Empty;
            Length = 0;
            IsNullable = true;
        }

        /// <summary>
        /// Constructor for DatabaseField with two arguments.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <remarks>
        /// Assuming default length is 100 and default is nullable.
        /// </remarks>
        public DatabaseField(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
            Length = 100;
            IsNullable = true;
        }

        /// <summary>
        /// Constructor for DatabaseField with three arguments.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <param name="isNullable">A boolean value indicating whether the field is nullable or not.</param>
        /// <remarks>
        /// Assuming default length is 100.
        /// </remarks>
        public DatabaseField(string name, string dataType, bool isNullable)
        {
            Name = name;
            DataType = dataType;
            Length = 100;
            IsNullable = isNullable;
        }

        /// <summary>
        /// Constructor for DatabaseField with four arguments.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <param name="length">The length of the field.</param>
        /// <param name="isNullable">A boolean value indicating whether the field is nullable or not.</param>
        public DatabaseField(string name, string dataType, int length, bool isNullable)
        {
            Name = name;
            DataType = dataType;
            Length = length;
            IsNullable = isNullable;
        }

        /// <summary>
        /// Constructor for DatabaseField with five arguments.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <param name="length">The length of the field.</param>
        /// <param name="isNullable">A boolean value indicating whether the field is nullable or not.</param>
        /// <param name="issutoIncrement">A boolean value indicating whether the field is an auto-increment field or not.</param>
        public DatabaseField(string name, string dataType, int length, bool isNullable, bool issutoIncrement)
        {
            Name = name;
            DataType = dataType;
            Length = length;
            IsNullable = isNullable;
            IsAutoIncrement = issutoIncrement;
        }

        /// <summary>
        /// Gets or sets the data type of the field.
        /// </summary>
        /// <value>
        /// The data type of the field.
        /// </value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is an auto-increment field.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the field is an auto-increment field; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutoIncrement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the field is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the length of the field.
        /// </summary>
        /// <value>
        /// The length of the field.
        /// </value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string Name { get; set; }
    }
}