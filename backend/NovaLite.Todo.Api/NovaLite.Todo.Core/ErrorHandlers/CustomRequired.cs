using System.ComponentModel.DataAnnotations;

namespace NovaLite.Todo.Core.ErrorHandlers
{
    public class CustomRequired : RequiredAttribute
    {

        public string AttributeName { get; private set; }
        public CustomRequired(string attributeName)
        {
            AttributeName = attributeName;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{AttributeName} should be entered.";
        }
    }
}
