namespace NovaLite.Todo.Api.Messages
{
    public static class ErrorMessages
    {
        public static string NotFound(string type, string attribute) => String.Format("The {0} with the given {1} doesn't exist.", type, attribute);
        public static string CurrentlyActiveReminder() => "You currently have an active reminder. Can't create a new one.";
        public static string InvalidReminderDate() => "The Reminder can't be in the past. You should atleast add a minute to it from your current time.";
    }
}
