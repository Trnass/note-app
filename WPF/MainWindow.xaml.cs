using System.ComponentModel;
using System.Windows;
using Notepad.BE;
using Notepad.Repositories;
using Notepad.Windows;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseCommunication databaseCommunication;
        private ApiUrlAdress apiUrlAdress;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.databaseCommunication = new DatabaseCommunication();
            this.apiUrlAdress = new ApiUrlAdress();

            UsernameTextBox.Text = Settings.Settings.Default.Username;
            PasswordBox.Password = Settings.Settings.Default.Password;
            SaveLoginCheckBox.IsChecked = Settings.Settings.Default.IsEnable;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {

            if((bool)SaveLoginCheckBox.IsChecked)
            {
                this.Save();
            }
            else
            {
                Settings.Settings.Default.Reset();
            }

            LoginUser loginUser = new LoginUser
            {
                username = UsernameTextBox.Text,
                password = this.databaseCommunication.HashPassword(PasswordBox.Password),
            };

            var data = await this.databaseCommunication.LoginAsync<LoginUser, User>(loginUser, apiUrlAdress.loginUser);

            if (data != null)
            {
                NotepadWindow notepadWindow = new NotepadWindow(data);

                notepadWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }

        }

        public void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            registerWindow.Show();
        }

        private void Save()
        {
            Settings.Settings.Default.Username = UsernameTextBox.Text;
            Settings.Settings.Default.Password = PasswordBox.Password;
            Settings.Settings.Default.IsEnable = (bool)SaveLoginCheckBox.IsChecked;

            Settings.Settings.Default.Save();
        }
    }
}
