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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;

namespace GEMC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillProfilesList();
            cbCategory.Items.Add("Отправленные");
            cbCategory.Items.Add("Принятые");
            
            
        }



        public void FillProfilesList()
        {
            lbProfiles.ItemsSource = Profile.DB_Load();
        }

        private void btnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProfile WaP = new WindowAddProfile();
            WaP.Owner = this;
            WaP.ShowDialog();
        }

        private void btnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile.DB_Delete((Profile)lbProfiles.SelectedItem);
            FillProfilesList();
        }

        private void btnRefreshProfile_Click(object sender, RoutedEventArgs e)
        {
            string strin = File.ReadAllText(@"D:\WriteLinesX.txt", Encoding.GetEncoding(1251));
            DecodeFrom64(strin);
            if (lbProfiles.SelectedIndex != -1)
            {
                PostClient ps = PostClient.Instance;
                ps.CheckForNewLetters((Profile)lbProfiles.SelectedItem);
               
            }
        }

        private void btnSendEMail_Click(object sender, RoutedEventArgs e)
        {
            if (lbProfiles.SelectedIndex != -1)
            {
                WindowSendMessage WSM = new WindowSendMessage((Profile)lbProfiles.SelectedItem);

                WSM.ShowDialog();
            }
        }


        private void lbProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategory.SelectedIndex == -1)
            {
                cbCategory.SelectedIndex = 1;
            }
            else
            {
                int n = cbCategory.SelectedIndex;
                cbCategory.SelectedIndex = -1;
                cbCategory.SelectedIndex = n;
            }
        }


        private void lbMailBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMailBox.SelectedIndex != -1)
            {
                Letter letter = ((ProxyLetter)lbMailBox.SelectedItem).GetLetter();
                MessageBox.Show(letter.Body);
                lbMailBox.SelectedIndex = -1;
            }

        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProxyList letterProxies = new ProxyList();
            if(cbCategory.SelectedIndex==0)
            {
                letterProxies.SetFillStrategy(new FillSended());
                letterProxies.Fill((Profile)lbProfiles.SelectedItem);
                lbMailBox.ItemsSource = letterProxies.ReturnList();
                //lbMailBox.ItemsSource = ProxyLetter.GetSended((Profile)lbProfiles.SelectedItem);
            }
            if(cbCategory.SelectedIndex==1)
            {
                letterProxies.SetFillStrategy(new FillRecieved());
                letterProxies.Fill((Profile)lbProfiles.SelectedItem);
                lbMailBox.ItemsSource = letterProxies.ReturnList();
                //lbMailBox.ItemsSource = ProxyLetter.GetRecieved((Profile)lbProfiles.SelectedItem);
            }
        }

        public void DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
            = System.Convert.FromBase64String(encodedData);
            string returnValue =
            System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            MessageBox.Show(returnValue);
        }
    }
}
