using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

public class GMailer
{
    private string smtpServer = "smtp.gmail.com";
    private int smtpPort = 587;
    private string smtpUser;
    private string smtpPassword;

    public GMailer()
    {
        // Récupération des valeurs depuis web.config
        smtpUser = ConfigurationManager.AppSettings["GmailUser"];
        smtpPassword = ConfigurationManager.AppSettings["GmailPassword"];
    }

    public bool SendEmail(string to, string subject, string body, bool isHtml = true)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(smtpUser);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isHtml;

                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
            return false;
        }
    }

    // Méthode pour envoyer l'email en mode synchrone
    public bool senMail(string to, string subject, string body, bool isHtml = true)
    {
        try
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(smtpUser); // smtpUser défini dans votre configuration
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isHtml;

                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)) // smtpServer et smtpPort depuis votre config
                {
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword); // smtpPassword défini dans votre config
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur : " + ex.Message);
            return false;
        }
    }


}