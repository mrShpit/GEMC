namespace GEMC
{
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

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
            //  if (lbProfiles.SelectedIndex != -1)
            //  {
            //    PostClient ps = PostClient.instance;
            //    ps.CheckForNewLettersPOP((Profile)lbProfiles.SelectedItem);
            //  }
            PostClient ps = PostClient.instance;
            ps.CheckForNewLettersIMAP((Profile)lbProfiles.SelectedItem);
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
            try
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
            catch
            {
            }
        }

        private void DecodeFrom64(string encodedData)
        {
            // string strin = File.ReadAllText(@"D:\WriteLinesX.txt", Encoding.GetEncoding(1251));
            // this.DecodeFrom64(strin);
            byte[] encodedDataAsBytes
            = System.Convert.FromBase64String(encodedData);
            string returnValue =
            System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            MessageBox.Show(returnValue);
        }
    }
}
