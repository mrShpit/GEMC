namespace GEMC
{
    using System.IO;
    using System.Text;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Threading.Tasks;

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        public static bool boolMailBoxOpened = false;
        public static bool boolLetterTabOpened = false;

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.Opacity = 0.00;
            dispatcherTimer.Tick += new EventHandler((sender1, e1) =>
            {
                if ((Opacity += 0.05) >= 1)
                {
                    dispatcherTimer.Stop();
                }
            });
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            dispatcherTimer.Start();
            SearchBox.SelectedIndex = 2;
            this.LoadingResources();
            this.DataContext = this.MainPostWindow;
        }

        public void LoadingResources()
        {
            LocalSQLConnection sqlconnectionClass = new LocalSQLConnection();
            sqlconnectionClass.DetectLocalDB();
            sqlconnectionClass.CloseConnection();

            this.FillProfilesListFull();
        }

        public List<Profile> profilesList = new List<Profile>();

        public List<Profile> ProfList
        {
            get
            {
                return this.profilesList;
            }
        }

        public void FillProfilesListFull()
        {
            this.profilesList = Profile.DB_Load();

            for (int i = 0; i < this.profilesList.Count; i++)
            {
                List<ProxyList> list = new List<ProxyList>();
                list.Add(ProxyList.GetSended(this.profilesList[i]));
                list.Add(ProxyList.GetRecieved(this.profilesList[i]));
                list.Add(ProxyList.GetTrash(this.profilesList[i]));
                list.Add(ProxyList.GetSpam(this.profilesList[i]));
                this.profilesList[i].ProxyMailFolders = list;
            }

            this.profilesList.Sort((x, y) => x.Name.CompareTo(y.Name));
            tvMain.Items.Refresh();
        }

        private void DecodeFrom64(string input)
        {
            // string strin = File.ReadAllText(@"D:\WriteLinesX.txt", Encoding.GetEncoding(1251));
            // this.DecodeFrom64(strin);
            // byte[] encodedDataAsBytes
            // = System.Convert.FromBase64String(encodedData);
            // string returnValue =
            // System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);

            // Attachment attachment = Attachment.CreateAttachmentFromString(string.Empty, encodedData);
            // MessageBox.Show(attachment.Name);
            // ________________
        }

        private void DragPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainPostWindow.DragMove();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btAddProfile_Click(object sender, RoutedEventArgs e)
        {
            Point location = this.PointToScreen(new Point(0, 0));
            double height = this.Height;
            double width = this.Width;
            WindowAddProfile wap = new WindowAddProfile(location, height, width);
            wap.Owner = this;
            wap.ShowDialog();
        }

        private void menuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            // In progress...
        }

        private void tvMain_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvMain.SelectedItem.GetType().ToString() == "GEMC.Profile")
            {
                if (boolMailBoxOpened)
                {
                    boolMailBoxOpened = false;
                    this.mailBoxAnimate(false);
                }

                lbMailBox.ItemsSource = null;
            }
            else
            {
                ProxyList selectedList = (ProxyList)tvMain.SelectedItem;

                //Анимация
                boolMailBoxOpened = true;
                this.mailBoxAnimate(true);

                lbMailBox.ItemsSource = selectedList.ProxyMailList;
            }

            ProxyLetter.CheckedItems.Clear();
        }

        private void mailBoxAnimate(bool stain)
        {
            if (stain)
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 42,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));
                Storyboard.SetTarget(heightAnimation, this.MailDesk);
                
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
                };

                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation, this.lbMailBox);

                DoubleAnimation opacityAnimation2 = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.8)
                };

                Storyboard.SetTargetProperty(opacityAnimation2, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation2, this.splitLetterBox);

                Storyboard s = new Storyboard();
                s.Children.Add(heightAnimation);
                s.Children.Add(opacityAnimation);
                s.Children.Add(opacityAnimation2);
                s.Begin();
            }
            else
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    From = 42,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));
                Storyboard.SetTarget(heightAnimation, this.MailDesk);

                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1)
                };

                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation, this.lbMailBox);

                DoubleAnimation opacityAnimation2 = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.8)
                };

                Storyboard.SetTargetProperty(opacityAnimation2, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation2, this.splitLetterBox);

                Storyboard s = new Storyboard();
                s.Children.Add(heightAnimation);
                s.Children.Add(opacityAnimation);
                s.Children.Add(opacityAnimation2);

                s.Begin();
            }
        }

        private void lbMailBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMailBox.SelectedIndex != -1)
            {
                Letter letterToShow = ((ProxyLetter)lbMailBox.SelectedItem).GetLetter();

                tabLetterBox.Items.Add(letterToShow);
                lbMailBox.SelectedIndex = -1;
                tabLetterBox.SelectedIndex = tabLetterBox.Items.Count - 1;
                
                if (boolLetterTabOpened == false)
                {
                    this.TabLetterAnimate(true);
                    boolLetterTabOpened = true;
                }
            }
        }

        private void TabLetterAnimate(bool stain)
        {
            if (stain)
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 48,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));
                Storyboard.SetTarget(heightAnimation, this.LetterReplyDesk);

                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation, this.tabLetterBox);

                Storyboard s = new Storyboard();
                s.Children.Add(heightAnimation);
                s.Children.Add(opacityAnimation);
                s.Begin();
            }
            else
            {
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    From = 48,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3)
                };

                Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(HeightProperty));
                Storyboard.SetTarget(heightAnimation, this.LetterReplyDesk);

                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
                Storyboard.SetTarget(opacityAnimation, this.tabLetterBox);

                Storyboard s = new Storyboard();
                s.Children.Add(heightAnimation);
                s.Children.Add(opacityAnimation);
                s.Begin();
            }
        }

        private void btCloseTab_Click(object sender, RoutedEventArgs e)
        {
            if (tabLetterBox.SelectedIndex != -1)
            {
                tabLetterBox.Items.RemoveAt(tabLetterBox.SelectedIndex);

                if (tabLetterBox.Items.Count == 0)
                {
                    this.TabLetterAnimate(false);
                    boolLetterTabOpened = false;
                }
            }
        }

        private void btCloseAllTabs_Click(object sender, RoutedEventArgs e)
        {
            tabLetterBox.Items.Clear();
            this.TabLetterAnimate(false);
            boolLetterTabOpened = false;
        }

        private void btSendLetter_Click(object sender, RoutedEventArgs e)
        {
            if (tvMain.SelectedItem != null)
            {
                this.CreateSendingWindow(string.Empty, string.Empty);
            }
            else
            {
                MessageBox.Show("Выберите профиль для отправки сообщения");
            }
        }

        private void CreateSendingWindow(string letterText, string adressesText)
        {
            Profile user = new Profile();

            if (tvMain.SelectedItem.GetType().ToString() == "GEMC.Profile")
            {
                user = (Profile)tvMain.SelectedItem;
            }
            else
            {
                ProxyList selectedList = (ProxyList)tvMain.SelectedItem;
                user = Profile.DB_GetByID(selectedList.ProfileId);
            }

            Point location = this.PointToScreen(new Point(0, 0));
            double height = this.Height;
            double width = this.Width;

            WindowSendMessage wsm = new WindowSendMessage(user, location, height, width, letterText, adressesText);
            wsm.Owner = this;
            wsm.ShowDialog();
        }

        private void CreateSendingWindowChoosingProfile(Profile user, string letterText, string adressesText)
        {
            Point location = this.PointToScreen(new Point(0, 0));
            double height = this.Height;
            double width = this.Width;

            WindowSendMessage wsm = new WindowSendMessage(user, location, height, width, letterText, adressesText);
            wsm.Owner = this;
            wsm.ShowDialog();
        }

        private void tabLetterBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                if (tabLetterBox.SelectedIndex != -1)
                {
                    tabLetterBox.Items.RemoveAt(tabLetterBox.SelectedIndex);

                    if (tabLetterBox.Items.Count == 0 && boolLetterTabOpened == true)
                    {
                        this.TabLetterAnimate(false);
                        boolLetterTabOpened = false;
                    }
                }
            }
        }

        private async void btGetMail_Click(object sender, RoutedEventArgs e)
        {
            if (tvMain.SelectedItem != null)
            {
                Profile user = new Profile();

                if (tvMain.SelectedItem.GetType().ToString() == "GEMC.Profile")
                {
                    user = (Profile)tvMain.SelectedItem;
                }
                else
                {
                    ProxyList selectedList = (ProxyList)tvMain.SelectedItem;
                    user = Profile.DB_GetByID(selectedList.ProfileId);
                }

                await this.PullingMail(user);
            }
            else
            {
                MessageBox.Show("Сначала выберите профиль");
            }
        }

        private async Task PullingMail(Profile user)
        {
            int userIndex = this.DetectUserIndexInTree(user);

            int inboxIndex = -1;
            for (int i = 0; i < this.profilesList[userIndex].ProxyMailFolders.Count; i++)
            {
                if (this.profilesList[userIndex].ProxyMailFolders[i].listName == "Входящие")
                {
                    inboxIndex = i;
                }
            }

            await Task.Run(() =>
                {
                    PostClient pc = PostClient.instance;
                    List<Letter> list = pc.PullFreshLetters(user);
                    foreach (Letter letter in list)
                    {
                        Letter.AddLetterToDB(user, letter);
                        ProxyLetter proxy = new ProxyLetter(letter.Id, letter.Subject, letter.SendingTime, letter.From);
                        this.profilesList[userIndex].ProxyMailFolders[inboxIndex].ProxyMailList.Add(proxy);
                        this.profilesList[userIndex].LastTimeChecked = DateTime.Now;
                    }

                    Dispatcher.BeginInvoke(new Action( () =>
   		            {
                        MessageBox.Show("Письма получены и список будет перезагружен");
                        tvMain.Items.Refresh();
   		            }));
                });
        }

        private int DetectUserIndexInTree(Profile user)
        {
            int userInd = -1;
            for (int i = 0; i < tvMain.Items.Count; i++)
            {
                if (user.Id == ((Profile)tvMain.Items[i]).Id)
                {
                    userInd = i;
                }
            }

            return userInd;
        }

        private void btSpam_Click(object sender, RoutedEventArgs e)
        {
            ProxyList selectedList = (ProxyList)tvMain.SelectedItem;
            Profile user = Profile.DB_GetByID(selectedList.ProfileId);
            int userInd = this.DetectUserIndexInTree(user);
            int curListInd = this.profilesList[userInd].ProxyMailFolders.IndexOf(selectedList);

            if (this.profilesList[userInd].ProxyMailFolders[curListInd].listName == "Спам" || ProxyLetter.CheckedItems.Count == 0)
            {
                return;
            }

            int spamInd = -1;
            for (int i = 0; i < this.profilesList[userInd].ProxyMailFolders.Count; i++)
            {
                if (this.profilesList[userInd].ProxyMailFolders[i].listName == "Спам")
                {
                    spamInd = i;
                }
            }

            foreach (ProxyLetter letter in ProxyLetter.CheckedItems)
            {
                this.profilesList[userInd].ProxyMailFolders[curListInd].ProxyMailList.Remove(letter);
                this.profilesList[userInd].ProxyMailFolders[spamInd].ProxyMailList.Add(letter);
                Letter.ChangeLetterFolderInDB(letter, "Spam");
            }

            ProxyLetter.UncheckAll();
            this.tvMain.Items.Refresh();

            return;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ProxyLetter.CheckedItems.Count == 0)
            {
                return;
            }

            ProxyList selectedList = (ProxyList)tvMain.SelectedItem;
            Profile user = Profile.DB_GetByID(selectedList.ProfileId);
            int userInd = this.DetectUserIndexInTree(user);
            int curListInd = this.profilesList[userInd].ProxyMailFolders.IndexOf(selectedList);

            if (selectedList.listName == "Корзина")
            {
                foreach (ProxyLetter letter in ProxyLetter.CheckedItems)
                {
                    this.profilesList[userInd].ProxyMailFolders[curListInd].ProxyMailList.Remove(letter);

                    Letter.DeleteLetterFromDB(letter.Id);
                }
            }
            else
            {
                int trashInd = -1;

                for (int i = 0; i < this.profilesList[userInd].ProxyMailFolders.Count; i++)
                {
                    if (this.profilesList[userInd].ProxyMailFolders[i].listName == "Корзина")
                    {
                        trashInd = i;
                    }
                }

                foreach (ProxyLetter letter in ProxyLetter.CheckedItems)
                {
                    this.profilesList[userInd].ProxyMailFolders[curListInd].ProxyMailList.Remove(letter);
                    this.profilesList[userInd].ProxyMailFolders[trashInd].ProxyMailList.Add(letter);

                    Letter.ChangeLetterFolderInDB(letter, "Trash");
                }
            }

            ProxyLetter.UncheckAll();
            this.tvMain.Items.Refresh();

            return;
        }

        private void btRestore_Click(object sender, RoutedEventArgs e)
        {
            if (ProxyLetter.CheckedItems.Count == 0)
            {
                return;
            }

            ProxyList selectedList = (ProxyList)tvMain.SelectedItem;
            Profile user = Profile.DB_GetByID(selectedList.ProfileId);
            int userInd = this.DetectUserIndexInTree(user);
            int curListInd = this.profilesList[userInd].ProxyMailFolders.IndexOf(selectedList);

            if (selectedList.listName != "Корзина" && selectedList.listName != "Спам")
            {
                return;
            }

            int inboxIndex = -1;
            int outboxIndex = -1;

            for (int i = 0; i < this.profilesList[userInd].ProxyMailFolders.Count; i++)
            {
                if (this.profilesList[userInd].ProxyMailFolders[i].listName == "Входящие")
                {
                    inboxIndex = i;
                }

                if (this.profilesList[userInd].ProxyMailFolders[i].listName == "Отправленные")
                {
                    outboxIndex = i;
                }
             }

            foreach (ProxyLetter proxyLetter in ProxyLetter.CheckedItems)
            {
                Letter letter = proxyLetter.GetLetter();
                if (letter.From == user.Adress)
                {
                    this.profilesList[userInd].ProxyMailFolders[curListInd].ProxyMailList.Remove(proxyLetter);
                    this.profilesList[userInd].ProxyMailFolders[outboxIndex].ProxyMailList.Add(proxyLetter);

                    Letter.ChangeLetterFolderInDB(proxyLetter, "Outbox");
                }
                else
                {
                    this.profilesList[userInd].ProxyMailFolders[curListInd].ProxyMailList.Remove(proxyLetter);
                    this.profilesList[userInd].ProxyMailFolders[inboxIndex].ProxyMailList.Add(proxyLetter);

                    Letter.ChangeLetterFolderInDB(proxyLetter, "Inbox");
                }
            }

            ProxyLetter.UncheckAll();
            this.tvMain.Items.Refresh();
            return;
        }

        private void ItemBorder_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = this.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
            {
                source = VisualTreeHelper.GetParent(source);
            }
 
            return source;
        }

        private void miEdit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tvMain.SelectedItem.GetType().ToString() == "GEMC.Profile")
            {
                MessageBoxResult dialogResult = MessageBox.Show(
                    "Вы действительно хотите удалить профиль " + ((Profile)tvMain.SelectedItem).Name + "?", 
                    "Удаление профиля", 
                    MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    Profile user = (Profile)tvMain.SelectedItem;
                    Profile.DB_Delete(user);
                    this.profilesList.Remove(user);
                    this.tvMain.Items.Refresh();
                }
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (tvMain.SelectedItem.GetType().ToString() == "GEMC.ProxyList")
            {
                if (e.Key == Key.Enter)
                {
                    if (SearchBox.Text == "Тема письма" && tbSearch.Text != string.Empty)
                    {
                        List<ProxyLetter> listToFill = new List<ProxyLetter>();
                        foreach (ProxyLetter p in lbMailBox.Items)
                        {
                            if (p.Subject.Contains(tbSearch.Text))
                            {
                                listToFill.Add(p);
                            }
                        }

                        lbMailBox.ItemsSource = listToFill;
                    }

                    if (SearchBox.Text == "Собеседники" && tbSearch.Text != string.Empty)
                    {
                        List<ProxyLetter> listToFill = new List<ProxyLetter>();
                        foreach (ProxyLetter p in lbMailBox.Items)
                        {
                            if (p.Interlocutor.Contains(tbSearch.Text))
                            {
                                listToFill.Add(p);
                            }
                        }

                        lbMailBox.ItemsSource = listToFill;
                    }

                    if (tbSearch.Text == string.Empty)
                    {
                        lbMailBox.ItemsSource = null;
                        lbMailBox.ItemsSource = ((ProxyList)tvMain.SelectedItem).ProxyMailList;
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите почтовый ящик");
            }
        }

        private void btAnswer_Click(object sender, RoutedEventArgs e)
        {
            Letter currentLetter  = ((Letter)tabLetterBox.SelectedItem);
            string senderAdress = currentLetter.From;
            Profile user = Profile.DB_GetByID(currentLetter.ProfileId);
            
            if (user.Adress == currentLetter.From)
            {
                return;
            }

            this.CreateSendingWindowChoosingProfile(user, string.Empty, senderAdress);
        }

        private void btResend_Click(object sender, RoutedEventArgs e)
        {
            string letterText = ((Letter)tabLetterBox.SelectedItem).Body;
            Profile user = Profile.DB_GetByID(((Letter)tabLetterBox.SelectedItem).ProfileId);
            this.CreateSendingWindowChoosingProfile(user, letterText, string.Empty);
        }

        private void checkBoxAbs_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lbMailBox.Items.Count; i++)
            {
                ((ProxyLetter)lbMailBox.Items[i]).IsChecked = true;
            }
        }

        private void checkBoxAbs_Unchecked(object sender, RoutedEventArgs e)
        {
            ProxyLetter.UncheckAll();
        }
    }
}