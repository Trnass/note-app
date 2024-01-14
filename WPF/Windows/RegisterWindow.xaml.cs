using System.Windows;
using Notepad.BE;
using Notepad.Repositories;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private DatabaseCommunication databaseCommunication;
        private ApiUrlAdress apiUrlAdress;
        public RegisterWindow()
        {
            InitializeComponent();

            this.databaseCommunication = new DatabaseCommunication();
            this.apiUrlAdress = new ApiUrlAdress();
        }
        public async void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            var registerUser = new
            {
                username = UsernameTextBox.Text,
                password = this.databaseCommunication.HashPassword(PasswordBox.Password),
                name = NameTextBox.Text,
                surname = SurenameTextBox.Text,
                mail = EmailTextBox.Text,
            };

            var data = await this.databaseCommunication.PostDataAsync(registerUser, this.apiUrlAdress.registerUser);

            MessageBox.Show(data);
        }

        public void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
