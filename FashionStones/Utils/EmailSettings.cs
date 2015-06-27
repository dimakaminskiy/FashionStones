using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FashionStones.Utils
{
    public class EmailSettings
    {
        public string Link = "www.fashion-stones.com.ua";
        public string MailFromAddress = "kapitoshka0777@gmail.com";
        public string ServerName = "smtp.gmail.com";
        public bool UseSsl = true;
        public int ServerPort = 587; //465;
        public string password = "8425999kapitoshka";
    }


    //public class GMailer
    //{
    //    public static string GmailUsername { get { return "kapitoshka0777@gmail.com"; } }
    //    public static string GmailPassword { get {return "8425999kapitoshka";} }
    //    public static int GmailPort { get; set; }
    //    public static bool GmailSSL { get; set; }

    //    public string ToEmail { get; set; }
    //    public string Subject { get; set; }
    //    public string Body { get; set; }
    //    public bool IsHtml { get; set; }

    //    static GMailer()
    //    {
    //        GmailHost = "smtp.gmail.com";
    //        GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
    //        GmailSSL = true;     
    //    }

        //public void Send()
        //{
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = GmailHost;
        //    smtp.Port = GmailPort;
        //    smtp.EnableSsl = GmailSSL;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

        //    using (var message = new MailMessage(GmailUsername, ToEmail))
        //    {
        //        message.Subject = Subject;
        //        message.Body = Body;
        //        message.IsBodyHtml = IsHtml;
        //        smtp.Send(message);
        //    }
        //}
  //  }


}