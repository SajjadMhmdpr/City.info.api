namespace City.info.api.Services
{
    public class CloudeMailService : IMailService
    {
        private readonly string _MailTo = string.Empty;
        private readonly string _MailFrom = string.Empty;
        public CloudeMailService(IConfiguration configuration)
        {
            _MailTo = configuration["mailSetting:mailTo"];
            _MailFrom = configuration["mailSetting:mailFrom"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Email from {_MailFrom} to {_MailTo} with {nameof(CloudeMailService)}");
            Console.WriteLine($"Subject : {subject}");
            Console.WriteLine($"Message : {message}");
        }
    }
}
