using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MasterSoft.WinUI
{
    /// <summary>
    /// 发邮件模块
    /// Author:tonyepaper.cnblogs.com
    /// </summary>
    public class Mail
    {
        private string senderAddress;
        /// <summary>
        /// 发件人
        /// </summary>
        public string SenderAddress
        {
            get { return senderAddress; }
            set { senderAddress = value; }
        }
        private string receiverAddess;
        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiverAddess
        {
            get { return receiverAddess; }
            set { receiverAddess = value; }
        }
        private string subject;
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        private string body;
        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        private string smtpHost;
        /// <summary>
        /// SMTP主机
        /// </summary>
        public string SmtpHost
        {
            get { return smtpHost; }
            set { smtpHost = value; }
        }
        private int smtpPort;
        /// <summary>
        /// SMTP端口
        /// </summary>
        public int SmtpPort
        {
            get { return smtpPort; }
            set { smtpPort = value; }
        }
        private string smtpPassword;
        /// <summary>
        /// SMTP密码
        /// </summary>
        public string Password
        {
            get { return smtpPassword; }
            set { this.smtpPassword = value; }
        }
        /// <summary>
        /// 从配置文件中读出SMTP相关信息
        /// </summary>
        public Mail()
        {
            senderAddress = ConfigurationManager.AppSettings["SmtpUser"];
            smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            smtpPort = Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        }
        /// <summary>
        /// 邮件
        /// </summary>
        /// <param name="receiverAddess">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public Mail(string receiverAddess, string subject, string body):this()
        {
            this.receiverAddess = receiverAddess;
            this.subject = subject;
            this.body = body;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        public bool Send()
        {
            MailMessage mailMessage = new MailMessage(senderAddress, receiverAddess);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
            //使用SSL加密连线
            smtpClient.EnableSsl=true;
            NetworkCredential networkCredential = new NetworkCredential(senderAddress, smtpPassword);
            smtpClient.Credentials = networkCredential;
            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}