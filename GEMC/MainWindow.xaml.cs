namespace GEMC
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
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
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.FillProfilesList();
            cbCategory.Items.Add("Отправленные");
            cbCategory.Items.Add("Принятые"); 
        }

        public void FillProfilesList()
        {
            lbProfiles.ItemsSource = Profile.DB_Load();
        }

        private void btnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProfile waP = new WindowAddProfile();
            waP.Owner = this;
            waP.ShowDialog();
        }

        private void btnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile.DB_Delete((Profile)lbProfiles.SelectedItem);
            this.FillProfilesList();
        }

        private void btnRefreshProfile_Click(object sender, RoutedEventArgs e)
        {
            string strin = File.ReadAllText(@"D:\WriteLinesX.txt", Encoding.GetEncoding(1251));
            this.DecodeFrom64(strin);
            if (lbProfiles.SelectedIndex != -1)
            {
                PostClient ps = PostClient.instance;
                ps.CheckForNewLetters((Profile)lbProfiles.SelectedItem);
            }
        }

        private void btnSendEMail_Click(object sender, RoutedEventArgs e)
        {
            if (lbProfiles.SelectedIndex != -1)
            {
                WindowSendMessage wsm = new WindowSendMessage((Profile)lbProfiles.SelectedItem);
                wsm.ShowDialog();
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
            if (cbCategory.SelectedIndex == 0)
            {
                letterProxies.SetFillStrategy(new FillSended());
                letterProxies.Fill((Profile)lbProfiles.SelectedItem);
                lbMailBox.ItemsSource = letterProxies.ReturnList();

                // lbMailBox.ItemsSource = ProxyLetter.GetSended((Profile)lbProfiles.SelectedItem);
            }

            if (cbCategory.SelectedIndex == 1)
            {
                letterProxies.SetFillStrategy(new FillRecieved());
                letterProxies.Fill((Profile)lbProfiles.SelectedItem);
                lbMailBox.ItemsSource = letterProxies.ReturnList();

                // lbMailBox.ItemsSource = ProxyLetter.GetRecieved((Profile)lbProfiles.SelectedItem);
            }
        }

        private void DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
            = System.Convert.FromBase64String(encodedData);
            string returnValue =
            System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            MessageBox.Show(returnValue);
        }
    }
}
