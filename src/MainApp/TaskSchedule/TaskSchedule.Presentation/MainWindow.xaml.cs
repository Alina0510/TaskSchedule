using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskSchedule.BLL;
using TaskSchedule.BLL.Singletons;
using TaskSchedule.DAL;
using TaskSchedule.Presentation.Pages;
using TaskSchedule.BLL.Services;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User? CurrentUser { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new RegisterPage(LoginPageRedirect));
        }
        public void LoginPageRedirect()
        {
            frame.Navigate(new LoginPage(RegisterPageRedirect, BoardsPageRedirect));
        }
        public void RegisterPageRedirect()
        {
            frame.Navigate(new RegisterPage(LoginPageRedirect));
        }
        public void BoardsPageRedirect(User user)
        {
            CurrentUser = user;
            frame.Navigate(new BoardsPage(NewBoardRedirect, BoardPageRedirect, user));
        }
        public void BoardsPageRedirect()
        {
            frame.Navigate(new BoardsPage(NewBoardRedirect, BoardPageRedirect, CurrentUser));
        }
        public void TaskPageRedirect(BoardTask currentTask, int? boardId)
        {
            frame.Navigate(new TaskPage(BoardPageRedirect, CurrentUser, currentTask, boardId));
        }
        public void BoardPageRedirect(int? boardId)
        {
            frame.Navigate(new BoardPage(boardId, NewTaskRedirect, TaskPageRedirect));
        }
        public void NewBoardRedirect()
        {
            frame.Navigate(new NewBoard(BoardsPageRedirect, CurrentUser));
        }
        public void NewTaskRedirect(int? boardId)
        {
            frame.Navigate(new NewTask(BoardPageRedirect, CurrentUser, boardId));
        }
    }
}