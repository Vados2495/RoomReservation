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
using System.Data;

namespace RoomReservation
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        private const string CONNECTION_STRING = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = Reservation; Trusted_Connection = True";
        private const string SELECT_QUERY = "SELECT * FROM Bookings";
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        public History()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    dataAdapter = new SqlDataAdapter(SELECT_QUERY, connection);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    usersGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}");
            }
        }

        private void HotelsBtn_Click(object sender, RoutedEventArgs e)
        {
            Content content = new Content();
            content.Show();
            this.Close();
        }

        private void RoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            Rooms rooms = new Rooms();
            rooms.Show();
            this.Close();
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Close();
        }
    }
}
