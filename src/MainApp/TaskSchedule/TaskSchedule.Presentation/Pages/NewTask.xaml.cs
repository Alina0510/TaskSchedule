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
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : Page
    {
        public Action<int?> GoToBoard { get; set; }
        public User CurentUser { get; set; }
        public int? BoardId { get; set; }

        public NewTask(Action<int?> goToBoard, User currentUser, int? boardId)
        {
            GoToBoard = goToBoard;
            CurentUser = currentUser;
            BoardId = boardId;
            InitializeComponent();
        }

        private async void createTaskButton_Click(object sender, RoutedEventArgs e)
        {
            await SingletonContext.Instance.CreateTask(nameTextBox.Text, descriptionTextBox.Text, datePicker.DisplayDate, CurentUser.Id, BoardId);
            GoToBoard(BoardId);
        }
    }
}
