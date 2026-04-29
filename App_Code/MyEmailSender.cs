using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;  
using System.Net.Mail;
using System.Web;
using System.IO;

namespace Wedding_Planner.App_Code
{
    public class MyEmailSender
    {
        string MyEmail = "mrpal440@gmail.com", MyEmailPasscode = "mpbmtpmiqqfknbby";
        internal string SendTo { get; set; }
        internal string CC { get; set; }
        internal string BCC { get; set; }
        internal string Subject { get; set; }
        internal string Message { get; set; }
        internal HttpPostedFileBase AttachmentFile { get; set; }
        internal bool SendEmail()
        {
            try
            {
                // settings of message
                MailMessage msg = new MailMessage();
                MailAddress maSender = new MailAddress(MyEmail);
                msg.Sender = maSender;
                msg.From = maSender;
                msg.To.Add(SendTo);
                if (CC != null)
                    msg.CC.Add(CC);
                if (BCC != null)
                    msg.Bcc.Add(BCC);
                msg.Subject = Subject;
                msg.Body = Message;
                if (AttachmentFile != null)
                {
                    FileManager fm = new FileManager();
                    fm.PostedFile = AttachmentFile;
                    fm.FolderName = "Email Attachments";
                    string str = fm.UploadMyFile();
                    if (str == "SUCCESS")
                    {
                        string FilePathWithName = HttpContext.Current.Server.MapPath("~/Content/" + fm.FolderName + "/" + fm.FileName);
                        Attachment at = new Attachment(FilePathWithName);
                        msg.Attachments.Add(at);
                    }
                }
                // settings of protocol
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential(MyEmail, MyEmailPasscode);
                client.Credentials = nc;
                client.Host = "smtp.gmail.com";
                client.Send(msg);// sending email
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}


