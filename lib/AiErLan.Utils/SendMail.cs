
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Utils
{
    /// <summary>
    /// 发送email
    /// </summary>
    public class SendMail
    {
        private string sourceEmailAddress;
        private string sourceEmailAccount;
        private string sourceEmailSmtpServer;
        private string sourceEmailPwd;

        private MailAddress mailAddress;
        private MailMessage mailMsg;
        private NetworkCredential credential;
        private SmtpClient smtpClient;

        /// <summary>
        /// 构造发信操作类
        /// </summary>
        public SendMail(string config)
        {
            dynamic emailConfig = JsonConvert.DeserializeObject(config);
            this.sourceEmailAddress = emailConfig.Address;
            this.sourceEmailAccount = emailConfig.Account;
            this.sourceEmailSmtpServer = emailConfig.SmtpServer;
            this.sourceEmailPwd = emailConfig.EmailPwd;

            mailAddress = new MailAddress(sourceEmailAddress);
            credential = new NetworkCredential(sourceEmailAccount, sourceEmailPwd);
            smtpClient = new SmtpClient(sourceEmailSmtpServer);
            smtpClient.Credentials = credential;
        }


        /// <summary>
        /// 群发邮件
        /// </summary>
        /// <param name="toEmails">要发送的邮箱地址集合</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        public void SendToEmail(List<string> toEmails, string subject, string content)
        {
            foreach (string emailaddress in toEmails)
            {
                SendEmail(emailaddress, subject, content);
            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="targetEmailAccount">目标邮箱地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        public void SendEmail(string targetEmailAccount, string subject, string content)
        {
            mailMsg = new MailMessage(this.sourceEmailAddress, targetEmailAccount, subject, content);
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = Encoding.UTF8;
            smtpClient.Send(mailMsg);
        }

        /// <summary>
        /// 发送反馈
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void SendFeedback(string contact, string title, string content)
        {
            SendEmail("test@qq.com",
                string.Format("by ebd feedbook {0}", title),
                string.Format("from:{0}</br>{1}", contact, content));
        }
    }
}
