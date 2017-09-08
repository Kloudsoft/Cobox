using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
    public class EmailUtilities
    {
        public string To { get; }
        public string From { get; }
        public string Body { get; }
        public string SenderName { get; }
        public string Subject { get; }
        private string Password { get; }
        public int Port { get; }
        public string Host { get; }
        public Encoding EncodingType { get; set; }
        public bool IsBodyHtml { get; }
        public SmtpDeliveryMethod SmtpDeliveryType { get; set; }
        public bool EnableSSL { get; }
        public int SmtpTimeout { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<AlternateView> AlternativeViews { get; set; }
        //public SmtpClient SmtpClient { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public EmailUtilities(string to, string from,string password, string body, string senderName, string subject, int port, string host,out Exception exception, bool isBodyHtml = true,bool enableSSL = false)
        {
            exception = null;
            try
            {
                if (string.IsNullOrEmpty(to) || string.IsNullOrWhiteSpace(to)) { throw (new Exception("Reciever email is required")); }
                if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password)) { throw (new Exception("Password is required")); }
                if (string.IsNullOrEmpty(body) || string.IsNullOrWhiteSpace(body)) { throw (new Exception("Email body is required")); }
                if (string.IsNullOrEmpty(senderName) || string.IsNullOrWhiteSpace(senderName)) { throw (new Exception("Sender's name is required")); }
                if (string.IsNullOrEmpty(from) || string.IsNullOrWhiteSpace(from)) { throw (new Exception("Sender's email is required")); }
                if (string.IsNullOrEmpty(host) || string.IsNullOrWhiteSpace(host)) { throw (new Exception("Host address is required")); }
                if (port <= 0) { throw (new Exception("Email server port is required")); }
                this.To = to;
                this.From = from;
                this.Body = body;
                this.Port = port;
                this.Subject = subject;
                this.Host = host;
                this.SenderName = senderName;
                this.IsBodyHtml = isBodyHtml;
                this.EnableSSL = enableSSL;
                this.Password = password;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        public bool SendEmail(out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var mailMessage = new MailMessage(this.From, this.To, this.Subject, this.Body))
                {
                    if (this.EncodingType == null) { this.EncodingType = Encoding.UTF8; }
                    mailMessage.BodyEncoding = this.EncodingType;
                    mailMessage.IsBodyHtml = this.IsBodyHtml;
                    if (Attachments != null)
                    {
                        if (Attachments.Count > 0)
                        {
                            foreach (var attachment in Attachments)
                            {
                                mailMessage.Attachments.Add(attachment);
                            }
                        }
                    }
                    if (AlternativeViews != null)
                    {
                        if (AlternativeViews.Count > 0)
                        {
                            foreach (var alternativeView in AlternativeViews)
                            {
                                mailMessage.AlternateViews.Add(alternativeView);
                            }
                        }
                    }
                    using (var smtpClient = new SmtpClient(this.Host,this.Port))
                    {
                        smtpClient.UseDefaultCredentials = this.UseDefaultCredentials;
                        if (this.EncodingType == null) { this.SmtpDeliveryType = SmtpDeliveryMethod.Network; }
                        smtpClient.Credentials = new System.Net.NetworkCredential(this.From,this.Password);
                        smtpClient.DeliveryMethod = this.SmtpDeliveryType;
                        smtpClient.EnableSsl = this.EnableSSL;
                        if (this.SmtpTimeout <= 0) { this.SmtpTimeout = 600000; }
                        smtpClient.Timeout = SmtpTimeout;
                        smtpClient.Send(mailMessage);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


    }
}
