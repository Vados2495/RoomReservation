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
    /// Логика взаимодействия для HotelBooking.xaml
    /// </summary>
    public partial class HotelBooking : Window
    {
        private List<Hotel> hotels;
        private List<Room> rooms;
        private string connectionString = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = RoomReserv; Trusted_Connection = True";
        private int selectedHotelId;
        private int selectedRoomId;
        public HotelBooking()
        {
            InitializeComponent();
            hotels = new List<Hotel>();
            rooms = new List<Room>();
            InitHotels();

            LoadHotels();
        }
        private void LoadHotels()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Rating FROM Hotels WHERE is_available = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = reader["Name"].ToString();
                        string rating = reader["Rating"].ToString();
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = name;
                        item.Content = rating;
                        item.Tag = id;
                        hotelComboBox.Items.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки отелей: " + ex.Message);
            }
        }

        private void LoadRooms(int hotelId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Number, Category, Price FROM Rooms WHERE HotelId = @HotelId AND is_available = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@HotelId", hotelId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        int number = (int)reader["Number"];
                        string category = reader["Category"].ToString();
                        decimal price = (decimal)reader["Price"];
                        Room room = new Room(id, number, category, price);
                        roomComboBox.Items.Add(room);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки номеров: " + ex.Message);
            }
        }
    }
}
