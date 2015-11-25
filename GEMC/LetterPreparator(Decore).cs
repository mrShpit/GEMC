namespace GEMC
{
   using System.Net.Mail;

    public class LetterPreparator : LetterDecorator
    {
        public LetterPreparator(Letter letterItem) : base(letterItem)
        {
        }

        public void SendIt(Profile sender)
        {
            // SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            // SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 25);
            // SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
            SmtpClient smtp = new SmtpClient("smtp." + sender.Server, sender.SmtpPort); // Сервер и порт
            smtp.Credentials = new System.Net.NetworkCredential(sender.Adress, sender.Password);  // Логин и пароль
            ////Формирование письма
            MailMessage message = new MailMessage();
            message.From = new MailAddress(sender.Adress);
            message.To.Add(new MailAddress(letterItem.To));
            message.Subject = letterItem.Subject;
            message.Body = letterItem.Body;
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}
