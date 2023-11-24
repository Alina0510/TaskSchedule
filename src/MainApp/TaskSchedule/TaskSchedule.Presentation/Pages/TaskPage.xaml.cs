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
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public Action<int?> GoToBoard { get; set; }
        public User CurrentUser { get; set; }
        public BoardTask CurrentTask { get; set; }
        public int? BoardId { get; set; }
        public TaskPage(Action<int?> goToBoard, User currentUser, BoardTask currentTask, int? boardId)
        {
            InitializeComponent();
            CurrentTask = currentTask;
            CurrentUser = currentUser;
            GoToBoard = goToBoard;
            BoardId = boardId;
            nameTextBox.Text = CurrentTask.Title;
            descriptionTextBox.Text = CurrentTask.Description;
            datePicker.SelectedDate = CurrentTask.Deadline;

            switch (currentTask.Status)
            {
                case "ToDo":
                    radioButton.IsChecked = true;
                    break;
                case "InReview":
                    radioButton1.IsChecked = true;
                    break;
                case "Done":
                    radioButton2.IsChecked = true;
                    break;
            }
            comboBox.ItemsSource = SingletonContext.Instance.GetUsers().Result;
            comboBox.SelectedValue = currentTask.AssignedUserId;
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton.IsChecked == true)
            {
                CurrentTask.Status = "ToDo";
            }
            if (radioButton1.IsChecked == true)
            {
                CurrentTask.Status = "InReview";
            }
            if (radioButton2.IsChecked == true)
            {
                CurrentTask.Status = "Done";
            }
            CurrentTask.Title = nameTextBox.Text;
            CurrentTask.Description = descriptionTextBox.Text;
            CurrentTask.Deadline = datePicker.SelectedDate.Value;
            var user = comboBox.SelectedItem as UserComboboxVM;
            CurrentTask.AssignedUserId = user.Id;
            await SingletonContext.Instance.SaveTask(CurrentTask);
            GoToBoard(BoardId);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            GoToBoard(BoardId);
        }
    }
}
