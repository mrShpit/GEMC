namespace GEMC
{
    using System;
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для WindowSendMessage.xaml
    /// </summary>
    public partial class WindowSendMessage : Window
    {
        private Profile mailSender;

        public WindowSendMessage(Profile prof, Point location, double height, double width, string letterText, string recievers)
        {
            this.InitializeComponent();
            this.mailSender = prof;
            this.Left = (width / 2) - (this.Width / 2) + location.X;
            this.Top = (height / 2) - (this.Height / 2) + location.Y;
            tbMessage.Text = letterText;
            tbAdress.Text = recievers;
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;

            if (tbAdress.Text != string.Empty && tbMessage.Text != string.Empty && tbSubject.Text != string.Empty)
            {
                string adressesText = tbAdress.Text.Remove(' ');
                string[] recievers = adressesText.Split(',');
                
                foreach (string reciever in recievers)
                {
                    Letter letter = new Letter(this.mailSender.Id, tbSubject.Text, tbMessage.Text, this.mailSender.Adress, tbAdress.Text, "Outbox", DateTime.Now);
                    letter.SetId();

                    PostClient pc = PostClient.instance;
                    pc.SendLetterSMTP(this.mailSender, letter);

                    Letter.AddLetterToDB(this.mailSender, letter);
                }

                this.Close();
                main.FillProfilesListFull();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SendMessageWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SendMessageWindow_Loaded(object sender, RoutedEventArgs e)
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