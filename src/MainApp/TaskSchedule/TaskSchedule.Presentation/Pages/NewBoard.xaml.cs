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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskSchedule.BLL.Services;
using TaskSchedule.BLL.Singletons;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for NewBoard.xaml
    /// </summary>
    public partial class NewBoard : Page
    {
        public Action GoToBoards { get; set; }
        public User CurrentUser { get; set; }
        public Action GoToMyTasks { get; set; }
        public Action GoToBoardsNav { get; set; }
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NewBoard(Action goToBoards, User user, Action goToMyTasks, Action goToBoardsNav)
        {
            GoToBoards = goToBoards;
            CurrentUser = user;
            InitializeComponent();
            GoToMyTasks = goToMyTasks;
            GoToBoardsNav = goToBoardsNav;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Create Board");
            try
            {
                await SingletonContext.Instance.CreateBoard(nameTextBox.Text, descriptionTextBox.Text, CurrentUser.Id);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            GoToBoards();
        }

        private void buttonBoards_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Go to Boards");
            GoToBoardsNav();
        }

        private void buttonMyTasks_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Go to My Tasks");
            GoToMyTasks();
        }
    }
}
