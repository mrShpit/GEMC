using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GEMC
{
    /// <summary>
    /// Логика взаимодействия для WindowAddProfile.xaml
    /// </summary>
    public partial class WindowAddProfile : Window
    {
        public WindowAddProfile()
        {
            InitializeComponent();
            cbServer.Items.Add("mail.ru");
            cbServer.Items.Add("gmail.com");
            cbServer.Items.Add("yandex.ru");
        }

        private void byAccept_Click(object sender, RoutedEventArgs e)
        {
            if( tbAdress.Text!= "" && tbPassword.Password!= "" && tbProfileName.Text!= "" && cbServer.Text!="")
            {
                MainWindow main = this.Owner as MainWindow;

                Profile NewUser = new Profile(tbProfileName.Text, tbAdress.Text, tbPassword.Password, cbServer.Text);
                if(cbServer.Text=="mail.ru")
                {
                    NewUser.SmtpPort = 25;
                    NewUser.PopPort = 995;
                }
                if (cbServer.Text == "gmail.com")
                {
                    NewUser.SmtpPort = 587;
                    NewUser.PopPort = 995;
                }
                if (cbServer.Text == "mail.ru")
                {
                    NewUser.SmtpPort = 25;
                    NewUser.PopPort = 995;
                }
                NewUser.SetId();
                NewUser.LastTimeChecked = DateTime.Now;
                Profile.DB_Add(NewUser);

                
               
                this.Close();
                main.FillProfilesList();
                
            }
        }
    }
}
