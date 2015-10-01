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
    /// Логика взаимодействия для WindowSendMessage.xaml
    /// </summary>
    public partial class WindowSendMessage : Window
    {
        private Profile Sender;
        public WindowSendMessage(Profile p)
        {
            InitializeComponent();
            Sender = p;
        }



        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdress.Text != "" && tbMessage.Text != "" && tbSubject.Text != "")
            {
                LetterBuilderStandart letBuilder = new LetterBuilderStandart();
                LetterBuildingDirector letDirector = new LetterBuildingDirector(letBuilder);

                letDirector.ConstructDefault(Sender.Id, tbSubject.Text, tbMessage.Text, Sender.Adress, tbAdress.Text, "Default");
                Letter letter = letBuilder.GetResult();
                letter.SetId();

                PostClient PC = PostClient.Instance;
                PC.SendLetterSMTP(Sender, letter);
            }

        }

    }
}
