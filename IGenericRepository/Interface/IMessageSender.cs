using System.Threading.Tasks;

namespace AllInterfaces.Interface {
    public interface IMessageSender {
        public Task SendEmailAsync(string toEmail, string subject, string message, bool IsMessageHtml = false);
    }
}
