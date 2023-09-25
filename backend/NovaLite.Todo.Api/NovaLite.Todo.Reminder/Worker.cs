using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NovaLite.Todo.Reminder
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public Worker(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var pullInterval = TimeSpan.FromSeconds(_configuration.GetValue<int>("PullInterval"));

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IToDoRepository>();
                var reminders = await repository.GetRemindersWithUsers();
                if (reminders.Any())
                    await SendEmail(reminders, repository);
                await Task.Delay(pullInterval, stoppingToken);
            }
        }

        private static async Task SendEmail(List<ReminderWithUserDTO> reminders, IToDoRepository repository)
        {
            var apiKey = Environment.GetEnvironmentVariable("APIKEYNOVALITESENDGRID");
            var client = new SendGridClient(apiKey);

            foreach (var reminder in reminders)
            {
                SendGridMessage msg = GetMessage(reminder);
                await client.SendEmailAsync(msg);

                await repository.ChangeReminderStatus(reminder.reminder.Id);
            }


        }

        private static SendGridMessage GetMessage(ReminderWithUserDTO reminder)
        {
            var from = new EmailAddress("vuga.sv53.2020@uns.ac.rs", "Nebojsa Vuga");
            var to = new EmailAddress(reminder.Email);
            var subject = "Todo Portal list reminder";
            var plainContent = "Reminder for your task at: " + reminder.reminder.Timestamp.LocalDateTime.ToString();
            var content = "Reminder for your task at: " + reminder.reminder.Timestamp.LocalDateTime.ToString();
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainContent, content);
            return msg;
        }
    }
}