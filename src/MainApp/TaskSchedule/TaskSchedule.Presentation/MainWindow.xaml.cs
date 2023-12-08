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
using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            frame.Navigate(new RegisterPage(LoginPageRedirect));
        }
        public void LoginPageRedirect()
        {
            log.Info("Login Page Redirect");
            frame.Navigate(new LoginPage(RegisterPageRedirect, BoardsPageRedirect));
        }
        public void RegisterPageRedirect()
        {
            log.Info("Register Page Redirect");
            frame.Navigate(new RegisterPage(LoginPageRedirect));
        }
        public void BoardsPageRedirect(User user)
        {
            log.Info("Boards Page Redirect");
            CurrentUser = user;
            frame.Navigate(new BoardsPage(NewBoardRedirect, BoardPageRedirect, user, MyTasksPageRedirect, BoardsPageRedirect));
        }
        public void MyTasksPageRedirect()
        {
            log.Info("My Tasks Page Redirect");
            frame.Navigate(new MyTasksPage(BoardsPageRedirect, CurrentUser, MyTasksPageRedirect, BoardsPageRedirect, TaskPageRedirect));
        }
        public void BoardsPageRedirect()
        {
            log.Info("Boards Page Redirect");
            frame.Navigate(new BoardsPage(NewBoardRedirect, BoardPageRedirect, CurrentUser, MyTasksPageRedirect, BoardsPageRedirect));
        }
        public void TaskPageRedirect(BoardTask currentTask, int? boardId, bool canUpdate, bool canDelete)
        {
            log.Info("Task Page Redirect");
            frame.Navigate(new TaskPage(BoardPageRedirect, CurrentUser, currentTask, boardId, canUpdate, canDelete, MyTasksPageRedirect, BoardsPageRedirect));
        }
        public void BoardPageRedirect(int? boardId)
        {
            log.Info("Board Page Redirect");
            frame.Navigate(new BoardPage(boardId, NewTaskRedirect, TaskPageRedirect, CurrentUser, MyTasksPageRedirect, BoardsPageRedirect));
        }
        public void NewBoardRedirect()
        {
            log.Info("New Board Redirect");
            frame.Navigate(new NewBoard(BoardsPageRedirect, CurrentUser, MyTasksPageRedirect, BoardsPageRedirect));
        }
        public void NewTaskRedirect(int? boardId)
        {
            log.Info("New Task Redirect");
            frame.Navigate(new NewTask(BoardPageRedirect, CurrentUser, boardId, MyTasksPageRedirect, BoardsPageRedirect));
        }
    }
}