using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RoomReservation.View;

namespace RoomReservation
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        private string connectionString = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = Account; Trusted_Connection = True";
        public Regist()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string email = tbEmail.Text;
            string password = pbPassword.Password;
            string confirmPassword = pbConfirmPassword.Password;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO [User] (Username, Email, Password, ConfirmPassword) VALUES (@Username, @Email, @Password, @ConfirmPassword)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                command.ExecuteNonQuery();
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string email = tbEmail.Text;
            string password = pbPassword.Password;
            string confirmPassword = pbConfirmPassword.Password;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO User (Username, Email, Password, ConfirmPassword) VALUES (@Username, @Email, @Password, @ConfirmPassword)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                command.ExecuteNonQuery();
            }
            HotelBooking hotel = new HotelBooking();
            hotel.Show();
            this.Close();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
