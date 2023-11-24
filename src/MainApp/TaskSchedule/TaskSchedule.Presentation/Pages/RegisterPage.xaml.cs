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
            if (nameTextBox.Text.IsNullOrEmpty() || nameTextBox.Text.Length < 8)
            {
                return false;
            }
            if (emailTextBox.Text.IsNullOrEmpty())
            {
                return false;
            }
            if (passwordTextBox.Text.IsNullOrEmpty() || passwordTextBox.Text.Length < 8 || passwordTextBox.Text != passwordTextBoxConfirm.Text)
            {
                return false;
            }
            return true;
        }
    }
}
