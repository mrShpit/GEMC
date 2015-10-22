namespace GEMC
{
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

    /// <summary>
    /// Логика взаимодействия для WindowSendMessage.xaml
    /// </summary>
    public partial class WindowSendMessage : Window
    {
        private Profile mailSender;

        public WindowSendMessage(Profile prof)
        {
            this.InitializeComponent();
            this.mailSender = prof;
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdress.Text != string.Empty && tbMessage.Text != string.Empty && tbSubject.Text != string.Empty)
            {
                Letter letter = new Letter(this.mailSender.Id, tbSubject.Text, tbMessage.Text, this.mailSender.Adress, tbAdress.Text, "Default", DateTime.Now);
                letter.SetId();

                PostClient pc = PostClient.instance;
                pc.SendLetterSMTP(this.mailSender, letter);

                Letter.AddLetterToDB(this.mailSender, letter);
            }
        }
    }
}