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
using TaskSchedule.BLL.Models;
using TaskSchedule.BLL.Services;
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
        public Action<BoardTask, int?, bool, bool> GoToTask { get; set; }
        public List<MyTaskVM> TasksList { get; set; }
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MyTasksPage(Action goToBoards, User user, Action goToMyTasks, Action goToBoardsNav, Action<BoardTask, int?, bool, bool> goToTask)
        {
            InitializeComponent();
            CurrentUser = user;
            GoToMyTasks = goToMyTasks;
            GoToBoardsNav = goToBoardsNav;
            TasksList = SingletonContext.Instance.GetMyTasksByUserId(CurrentUser.Id).Result;
            TaskToDoList.ItemsSource = TasksList;
            GoToTask = goToTask;
        }

        private void buttonBoards_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Boards Page Redirect");
            GoToBoardsNav();
        }

        private void buttonMyTasks_Click(object sender, RoutedEventArgs e)
        {
            log.Info("My Tasks Page Redirect");
            GoToMyTasks();
        }

        private void TaskList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            log.Info("Task Page Redirect");
            if (sender is ListView list && list.SelectedItem is MyTaskVM selectedItem)
            {
                bool canDelete = false;
                if (selectedItem.TaskRole.TaskDelete == AccessLevelEnum.All || selectedItem.TaskRole.TaskDelete == AccessLevelEnum.Own)
                {
                    canDelete = true;
                }
                GoToTask(selectedItem.BoardTask, selectedItem.BoardId, selectedItem.TaskRole.TaskWrite == AccessLevelEnum.All, canDelete);
            }
        }
    }
}
