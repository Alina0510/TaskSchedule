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
using TaskSchedule.BLL.Models;
using TaskSchedule.BLL.Singletons;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for MyTasksPage.xaml
    /// </summary>
    public partial class MyTasksPage : Page
    {
        public Action GoToMyTasks { get; set; }
        public Action GoToBoardsNav { get; set; }
        public User CurrentUser { get; set; }
        public MyTasksPage(Action goToBoards, User user, Action goToMyTasks, Action goToBoardsNav)
        {
            InitializeComponent();
            CurrentUser = user;
            GoToMyTasks = goToMyTasks;
            GoToBoardsNav = goToBoardsNav;
        }

        private void buttonBoards_Click(object sender, RoutedEventArgs e)
        {
            GoToBoardsNav();
        }

        private void buttonMyTasks_Click(object sender, RoutedEventArgs e)
        {
            GoToMyTasks();
        }
    }
}
