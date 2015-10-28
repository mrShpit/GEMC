namespace GEMC
{
    using System;
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для WindowAddProfile.xaml
    /// </summary>
    public partial class WindowAddProfile : Window
    {
        public WindowAddProfile()
        {
            this.InitializeComponent();
            cbServer.Items.Add("mail.ru");
            cbServer.Items.Add("gmail.com");
            cbServer.Items.Add("yandex.ru");
        }

        private void byAccept_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdress.Text != string.Empty && tbPassword.Password != string.Empty && tbProfileName.Text != string.Empty && cbServer.Text != string.Empty)
            {
                MainWindow main = this.Owner as MainWindow;

                Profile newUser = new Profile(tbProfileName.Text, tbAdress.Text, tbPassword.Password, cbServer.Text);
                if (cbServer.Text == "mail.ru")
                {
                    newUser.SmtpPort = 25;
                    newUser.PopPort = 995;
                }

                if (cbServer.Text == "gmail.com")
                {
                    newUser.SmtpPort = 587;
                    newUser.PopPort = 995;
                }

                if (cbServer.Text == "mail.ru")
                {
                    newUser.SmtpPort = 25;
                    newUser.PopPort = 995;
                }

                newUser.SetId();
                newUser.LastTimeChecked = DateTime.Now;
                Profile.DB_Add(newUser);
                this.Close();
                main.FillProfilesList();
            }
        }
    }
}
