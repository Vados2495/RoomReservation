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

namespace RoomReservation
{
    /// <summary>
    /// Логика взаимодействия для AdminAuth.xaml
    /// </summary>
    public partial class AdminAuth : Window
    {
        public AdminAuth()
        {
            InitializeComponent();
        }
        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = Account; Trusted_Connection = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM Admin WHERE Username='{UsernameTextBox.Text}' AND password='{passwordTextBox.Password}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        Content content = new Content();
                        content.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }
    }
}
