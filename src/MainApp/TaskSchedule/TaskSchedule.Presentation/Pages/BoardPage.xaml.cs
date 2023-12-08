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
    /// Interaction logic for BoardPage.xaml
    /// </summary>
    public partial class BoardPage : Page
    {
        public Board CurrentBoard { get; set; }
        public User CurrentUser { get; set; }
        public Role CurrentRole { get; set; }
        public Action<int?> GoToCreateTask { get; set; }
        public Action GoToMyTasks { get; set; }
        public Action GoToBoardsNav { get; set; }
        public Action<BoardTask, int?, bool, bool> GoToTask { get; set; }
        public List<BoardTask> ToDoTasks { get; set; }
        public List<BoardTask> InReviewTasks { get; set; }
        public List<BoardTask> DoneTasks { get; set; }
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BoardPage(int? boardId, Action<int?> goToCreateTask, Action<BoardTask, int?, bool, bool> goToTask, User user, Action goToMyTasks, Action goToBoardsNav)
        {
            CurrentBoard = SingletonContext.Instance.GetBoardById(boardId).Result;
            GoToCreateTask = goToCreateTask;
            (ToDoTasks, InReviewTasks, DoneTasks) = SingletonContext.Instance.GetTasksByBoardId(CurrentBoard.Id).Result;
            InitializeComponent();
            TaskToDoList.ItemsSource = ToDoTasks;
            TaskReviewList.ItemsSource = InReviewTasks;
            TaskFinishedList.ItemsSource = DoneTasks;
            GoToTask = goToTask;
            CurrentUser = user;
            CurrentRole = SingletonContext.Instance.GetUserRole(CurrentUser.Id, CurrentBoard.Id).Result;
            if (CurrentRole.TaskWrite == AccessLevelEnum.None)
            {
                createTaskButton.IsEnabled = false;
            }
            GoToMyTasks = goToMyTasks;
            GoToBoardsNav = goToBoardsNav;
        }

        private void createTaskButton_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Create task button clicked");
            GoToCreateTask(CurrentBoard.Id);
        }
        private void TaskList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            log.Info("Task list double clicked");
            if (sender is ListView list && list.SelectedItem is BoardTask selectedItem)
            {
                bool canDelete = false;
                if (CurrentRole.TaskDelete == AccessLevelEnum.All)
                {
                    canDelete = true;
                }
                if (CurrentRole.TaskDelete == AccessLevelEnum.Own && selectedItem.AssignedUserId == CurrentUser.Id)
                {
                    canDelete = true;
                }
                GoToTask(selectedItem, CurrentBoard.Id, CurrentRole.TaskWrite == AccessLevelEnum.All, canDelete);
            }
        }

        private void buttonBoards_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Boards button clicked");
            GoToBoardsNav();
        }

        private void buttonMyTasks_Click(object sender, RoutedEventArgs e)
        {
            log.Info("My tasks button clicked");
            GoToMyTasks();
        }
    }
}
