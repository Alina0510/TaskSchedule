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
using TaskSchedule.DAL.Models;

namespace TaskSchedule.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public Action GoToRegister { get; set; }
        public Action<User> GoToBoards { get; set; }
        public LoginPage(Action goToRegister, Action<User> goToBoards)
        {
            InitializeComponent();
            GoToRegister = goToRegister;
            GoToBoards = goToBoards;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GoToRegister();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = await SingletonContext.Instance.LoginUser(emailTextBox.Text, passwordTextBox.Text);
            if (user == null)
            {
                return;
            }
            GoToBoards(user);
        }
    }
}
