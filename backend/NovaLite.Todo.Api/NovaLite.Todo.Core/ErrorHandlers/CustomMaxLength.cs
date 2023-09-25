using System.ComponentModel.DataAnnotations;

namespace NovaLite.Todo.Core.ErrorHandlers
{
    public class CustomMaxLength : MaxLengthAttribute
    {
        public string AttributeName { get; private set; }
        public int MaxLength { get; private set; }
        public CustomMaxLength(string attributeName, int maxLength)
        {
            AttributeName = attributeName;
            MaxLength = maxLength;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{AttributeName} should have the less than {MaxLength} characters.";
        }

    }
}
