using Microsoft.IdentityModel.Tokens;
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
using TaskSchedule.BLL.Services;
using TaskSchedule.BLL.Singletons;

namespace TaskSchedule.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public Action GoToLogin { get; set; }
        public RegisterPage(Action goToLogin)
        {
            GoToLogin = goToLogin;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            GoToLogin();
        }

        private async void createUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(VilidateUser()) 
            {
                await SingletonContext.Instance.CreateUser(nameTextBox.Text, emailTextBox.Text, passwordTextBox.Text);
                GoToLogin(); 
            }
        }
        private bool VilidateUser()
        {
            labelErrorConfPass.Content = string.Empty;
            labelErrorPass.Content = string.Empty;
            labelErrorEmail.Content = string.Empty;
            labelErrorNickname.Content = string.Empty;
            bool valid = true;
            if (nameTextBox.Text.IsNullOrEmpty())
            {
                valid = false;
                labelErrorNickname.Content = "NickName is empty";
            }
            if ( nameTextBox.Text?.Length < 8)
            {
                valid = false;
                labelErrorNickname.Content = "NickName length is less then 8 symbols";
            }
            if (emailTextBox.Text.IsNullOrEmpty())
            {
                valid = false;
                labelErrorEmail.Content = "Email is empty";
            }
            if (passwordTextBox.Text.IsNullOrEmpty())
            {
                valid = false;
                labelErrorPass.Content = "Password is empty";
            }
            if (passwordTextBox.Text.Length < 8)
            {
                valid = false;
                labelErrorPass.Content = "Password length is less then 8 symbols";
            }
            if (passwordTextBox.Text != passwordTextBoxConfirm.Text)
            {
                valid = false;
                labelErrorConfPass.Content = "Confirm password doesn`t match password";
            }
            return valid;
        }
    }
}
