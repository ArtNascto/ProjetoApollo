using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using MailKit.Net.Smtp;

namespace ProjetoApollo.Email
{
    public class SmtpBuilder : DefaultMailKitSmtpBuilder
    {
        public SmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, IAbpMailKitConfiguration iAbpMailKitConfiguration)
       : base(smtpEmailSenderConfiguration, iAbpMailKitConfiguration)
        {
        }

        protected override void ConfigureClient(SmtpClient client)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            base.ConfigureClient(client);
        }
    }
}