namespace GEMC
{
    using System;
    using System.Windows;
    using System.Collections.Generic;

    /// <summary>
    /// Логика взаимодействия для WindowAddProfile.xaml
    /// </summary>
    public partial class WindowAddProfile : Window
    {
        public List<Profile> ProfilesList;

        public WindowAddProfile(Point location, double height, double width)
        {
            this.InitializeComponent();
            this.Left = (width / 2) - (this.Width / 2)  + location.X;
            this.Top = (height / 2) - (this.Height / 2)  + location.Y;
        }

        private void byAccept_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            if (tbAdress.Text != string.Empty && tbPassword.Password != string.Empty && tbProfileName.Text != string.Empty && cbServer.Text != string.Empty)
            {
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

                if (cbServer.Text == "yandex.ru")
                {
                    newUser.SmtpPort = 25;
                    newUser.PopPort = 995;
                }

                newUser.SetId();
                newUser.LastTimeChecked = DateTime.Now;
                Profile.DB_Add(newUser);
                main.FillProfilesListFull();

                this.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddProfileWindow.DragMove();
        }

        private void AddProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.Opacity = 0.00;
            dispatcherTimer.Tick += new EventHandler((sender1, e1) =>
            {
                if ((Opacity += 0.1) >= 1)
                {
                    dispatcherTimer.Stop();
                }
            });
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            dispatcherTimer.Start();
        }
    }
}
