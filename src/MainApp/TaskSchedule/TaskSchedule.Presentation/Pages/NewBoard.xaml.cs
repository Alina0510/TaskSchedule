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
        public NewBoard(Action goToBoards, User user)
        {
            GoToBoards = goToBoards;
            CurrentUser = user;
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            await SingletonContext.Instance.CreateBoard(nameTextBox.Text, descriptionTextBox.Text, CurrentUser.Id);
            GoToBoards();
        }
    }
}
