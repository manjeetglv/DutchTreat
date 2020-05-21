using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to, string subject, string body)
        {
            // TODO: log the message
            _logger.LogInformation($"To: {to} \nSubject: {subject} \nBody: \n {body}");
        }
    }
}