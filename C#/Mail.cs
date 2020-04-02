using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Example.Utilities
{
    public class Mailer
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

        private MailAddress fromAddress { get; set; }
        private string fromPassword { get; set; }
        private string mailHost { get; set; }
        private int mailPort { get; set; }
        private Boolean mailSSL { get; set; }

        private bool custom;

        public Mailer()
        {
            custom = false;

        }

        public Mailer(string sFromAddress, string sFromAddressName, string sFromPassword, string host, int port, Boolean ssl)
        {
            custom = true;
            fromAddress = new MailAddress(sFromAddress, sFromAddressName);
            fromPassword = sFromPassword;
            mailHost = host;
            mailPort = port;
            mailSSL = ssl;

        }

        public bool enviar(String toEmail, String toName, string subject, string body, IList<string> attachments)
        {
            return enviar(toEmail, toName, null, null, null, subject, body, attachments);
        }

        public bool enviar(String toEmail, String toName, string sCcAddress, string sCcAddressName, string subject, string body, IList<string> attachments)
        {
            return enviar(toEmail, toName, sCcAddress, sCcAddressName, null, subject, body, attachments);
        }

        public bool enviar(String toEmail, String toName, string sCcAddress, string sCcAddressName, string sBccAddress, string subject, string body, IList<string> attachments)
        {
            return basicEnviar(toEmail, toName, sCcAddress, sCcAddressName, sBccAddress, subject, body, attachments, null, null);
        }

        public bool enviar(String toEmail, String toName, string subject, string body, IList<string> attachments, IDictionary<string, System.IO.Stream> attachments2)
        {
            return basicEnviar(toEmail, toName, null, null, null, subject, body, attachments, attachments2, null);
        }

        public bool enviar(String toEmail, String toName, string subject, string body, IList<string> attachments, IDictionary<string, System.IO.Stream> attachments2, IDictionary<string, string> embeddedAttachments)
        {
            return basicEnviar(toEmail, toName, null, null, null, subject, body, attachments, attachments2, embeddedAttachments);
        }
        public bool enviar(String toEmail, String toName, string subject, string body, IList<string> attachments, IDictionary<string, System.IO.Stream> attachments2, IDictionary<string, string> embeddedAttachments, bool sendBccCopy)
        {
            return basicEnviar(toEmail, toName, null, null, null, subject, body, attachments, attachments2, embeddedAttachments, sendBccCopy);
        }

        private bool basicEnviar(String toEmail, String toName, string sCcAddress, string sCcAddressName, string sBccAddress, string subject, string body, IList<string> attachments, IDictionary<string, System.IO.Stream> attachments2, IDictionary<string, string> embeddedAttachments)
        {
            return basicEnviar(toEmail, toName, sCcAddress, sCcAddressName, sBccAddress, subject, body, attachments, attachments2, embeddedAttachments, false);
        }

        private bool basicEnviar(String toEmail, String toName, string sCcAddress, string sCcAddressName, string sBccAddress, string subject, string body, IList<string> attachments, IDictionary<string, System.IO.Stream> attachments2, IDictionary<string, string> embeddedAttachments, bool sendBccCopy)
        {
            try
            {
                var toAddress = new MailAddress(toEmail, toName);
                var smtp = new SmtpClient();
                var message = new MailMessage()
                {
                    Subject = subject,
                    Body = body
                };

                message.To.Add(toAddress);
                if (custom)
                {
                    smtp.Host = mailHost;
                    smtp.Port = mailPort;
                    smtp.EnableSsl = mailSSL;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                    message.From = fromAddress;
                };

                if (sCcAddress != null)
                    message.CC.Add(new MailAddress(sCcAddress));

                if (sBccAddress != null)
                    message.Bcc.Add(new MailAddress(sBccAddress));

                if (sendBccCopy)
                {
                    try
                    {
                        System.Configuration.Configuration configurationFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/web.config");
                        var mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as System.Net.Configuration.MailSettingsSectionGroup;
                        if (mailSettings != null)
                        {
                            string usernameSettings = mailSettings.Smtp.From;
                            message.Bcc.Add(new MailAddress(usernameSettings));
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }

                message.IsBodyHtml = true;

                if (attachments != null && attachments.Count > 0)
                {
                    foreach (string s in attachments)
                    {
                        message.Attachments.Add(new Attachment(s));
                    }
                }
                if (attachments2 != null && attachments2.Count > 0)
                {
                    foreach (string s in attachments2.Keys)
                    {
                        message.Attachments.Add(new Attachment(attachments2[s], s));
                    }
                }
                if (embeddedAttachments != null && embeddedAttachments.Count > 0)
                {
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, System.Net.Mime.MediaTypeNames.Text.Html);
                    foreach (string s in embeddedAttachments.Keys)
                    {
                        LinkedResource imagelink = new LinkedResource(embeddedAttachments[s], getMediaType(embeddedAttachments[s]));
                        imagelink.ContentId = s;
                        imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        htmlView.LinkedResources.Add(imagelink);
                    }
                    message.AlternateViews.Add(htmlView);
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
                return true;
            }

            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        private string getMediaType(string file)
        {
            if (file.EndsWith(".jpg") || file.EndsWith(".jpeg"))
            {
                return System.Net.Mime.MediaTypeNames.Image.Jpeg;
            }
            else if (file.EndsWith(".gif"))
            {
                return System.Net.Mime.MediaTypeNames.Image.Gif;
            }
            else if (file.EndsWith(".png"))
            {
                return "image/png";
            }
            return "image/png";
        }

    }

}