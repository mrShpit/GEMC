using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows;

namespace GEMC
{
    public sealed class PostClient
        
    {

        static readonly PostClient _instance = new PostClient();

        PostClient()
        {

        }

        public static PostClient Instance
        {
            get
            {
                return _instance;
            }
        }

        public void SendLetterSMTP(Profile sender, Letter letter)
        {
            SmtpClient Smtp = new SmtpClient("smtp." + sender.Server, sender.Port); //Сервер и порт
            Smtp.Credentials = new System.Net.NetworkCredential(sender.Adress, sender.Password);  //Логин и пароль
            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(sender.Adress);
            Message.To.Add(new MailAddress(letter.To));
            Message.Subject = letter.Subject;
            Message.Body = letter.Body;
            Smtp.EnableSsl = true;
            //Отправка
            Smtp.Send(Message);
        }



        public void SendLetterTest()
        {

            //mtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            //SmtpClient Smtp = new SmtpClient("smtp.yandex.ru", 25);
            //SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);


            //MailMessage mail = new MailMessage();
            //System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

            //mail.From = new MailAddress("shpytagleb@gmail.com");
            //mail.To.Add("shpyta-gleb@mail.ru");
            //mail.Subject = "Test Mail";
            //mail.Body = "This is for testing SMTP mail from GMAIL";

            
            ////465
            //SmtpServer.Credentials = new System.Net.NetworkCredential("shpytagleb@gmail.com", "utybfkmyjcnm");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);

            //Авторизация на SMTP сервере
            SmtpClient Smtp = new SmtpClient("pop3.gmail.com", 995); //Сервер и порт
            Smtp.Credentials = new System.Net.NetworkCredential("shpytagleb@gmail.com", "utybfkmyjcnm");  //Логин и пароль
            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("shpytagleb@gmail.com"); //
            Message.To.Add(new MailAddress("shpyta-gleb@mail.ru"));//Кому
            Message.Subject = "Hello!"; //Тема
            Message.Body = "28 сентября состоится полное лунное затмение (совпадающее с суперлунием) с утренней видимостью в западных регионах" +
            "России.В результате давки в Мекке погибло не менее 769 человек, не менее 863 получили ранения." +
            "   Следственный комитет России эксгумировал останки Николая II и возобновил расследование по факту гибели царской семьи (на фото).";
            Smtp.EnableSsl = true;
            //Отправка
            Smtp.Send(Message);
        }


        public List<Letter> CheckForNewLetters(Profile user)
        {
            List<Letter> NewMail = new List<Letter>();
            //
            //
            return NewMail;
        }


        public List<Letter> DownloadAllLetters(Profile user)
        {
            List<Letter> Mail = new List<Letter>();
            //
            //
            return Mail;
        }
    }
}
