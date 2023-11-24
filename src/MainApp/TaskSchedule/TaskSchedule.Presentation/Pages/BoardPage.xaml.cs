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
        public Action<int?> GoToCreateTask { get; set; }
        public Action<BoardTask, int?> GoToTask { get; set; }
        public List<BoardTask> ToDoTasks { get; set; }
        public List<BoardTask> InReviewTasks { get; set; }
        public List<BoardTask> DoneTasks { get; set; }
        public BoardPage(int? boardId, Action<int?> goToCreateTask, Action<BoardTask, int?> goToTask)
        {
            CurrentBoard = SingletonContext.Instance.GetBoardById(boardId).Result;
            GoToCreateTask = goToCreateTask;
            (ToDoTasks, InReviewTasks, DoneTasks) = SingletonContext.Instance.GetTasksByBoardId(CurrentBoard.Id).Result;
            InitializeComponent();
            TaskToDoList.ItemsSource = ToDoTasks;
            TaskReviewList.ItemsSource = InReviewTasks;
            TaskFinishedList.ItemsSource = DoneTasks;
            GoToTask = goToTask;
        }

        private void createTaskButton_Click(object sender, RoutedEventArgs e)
        {
            GoToCreateTask(CurrentBoard.Id);
        }
        private void TaskList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView list && list.SelectedItem is BoardTask selectedItem)
            {
                GoToTask(selectedItem, CurrentBoard.Id);
            }
        }
    }
}
