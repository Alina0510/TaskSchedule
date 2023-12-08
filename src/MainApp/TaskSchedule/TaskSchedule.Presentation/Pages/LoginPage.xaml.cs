using log4net;
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
using System.Windows.Media.Animation;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LoginPage(Action goToRegister, Action<User> goToBoards)
        {
            InitializeComponent();
            GoToRegister = goToRegister;
            GoToBoards = goToBoards;
            var fadeInAnimation = (Storyboard)FindResource("FadeInStoryboard");
            Storyboard.SetTarget(fadeInAnimation, emailTextBox);
            fadeInAnimation.Begin();
            Storyboard.SetTarget(fadeInAnimation, passwordTextBox);
            fadeInAnimation.Begin();
            Storyboard.SetTarget(fadeInAnimation, loginButton);
            fadeInAnimation.Begin();
            Storyboard.SetTarget(fadeInAnimation, button);
            fadeInAnimation.Begin();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Go to register page");
            GoToRegister();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Login button clicked");

            var user = await SingletonContext.Instance.LoginUser(emailTextBox.Text, passwordTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("Wrong email or password");
                log.Info("Wrong email or password");
                AnimateShake(emailTextBox);
                AnimateShake(passwordTextBox);
                return;
            }
            log.Info("User logged in");
            GoToBoards(user);
        }
        private void AnimateShake(UIElement element)
        {
            var shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
            Storyboard.SetTarget(shakeAnimation, element);
            shakeAnimation.Begin();
        }
    }
}
