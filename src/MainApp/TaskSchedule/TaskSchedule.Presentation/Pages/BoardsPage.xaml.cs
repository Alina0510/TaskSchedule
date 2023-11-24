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
    /// Interaction logic for BoardsPage.xaml
    /// </summary>
    public partial class BoardsPage : Page
    {
        public Action GoToNewBoard {  get; set; }
        public Action<int?> GoToBoard { get; set; }
        public User CurrentUser { get; set; }
        public Action GoToMyTasks { get; set; }
        public Action GoToBoardsNav { get; set; }
        public List<MyBoardsListVM> BoardsList {  get; set; }
        public BoardsPage(Action goToNewBoard, Action<int?> goToBoard, User user, Action goToMyTasks, Action goToBoardsNav)
        {
            InitializeComponent();
            GoToNewBoard = goToNewBoard;
            GoToBoard = goToBoard;
            CurrentUser = user;
            BoardsList = SingletonContext.Instance.GetBoardForUser(CurrentUser.Id).Result;
            MyListView.ItemsSource = BoardsList;
            GoToMyTasks = goToMyTasks;
            GoToBoardsNav = goToBoardsNav;
        }

        private void createBoardButton_Click(object sender, RoutedEventArgs e)
        {
            GoToNewBoard();
        }
        private void MyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is MyBoardsListVM selectedItem)
            {
                GoToBoard(selectedItem.Id);
                    
            }
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
