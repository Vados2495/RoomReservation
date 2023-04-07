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
        private string connectionString = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = Reservation; Trusted_Connection = True";
        private int selectedHotelId;
        private int selectedRoomId;
        public HotelBooking()
        {
            InitializeComponent();
            hotels = new List<Hotel>();
            rooms = new List<Room>();
            InitHotels();
        }
        private void InitHotels()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Hotels WHERE is_available = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.Id = int.Parse(reader["Id"].ToString());
                        hotel.Name = reader["Name"].ToString();
                        hotel.Rating = int.Parse(reader["Rating"].ToString());
                        hotels.Add(hotel);
                    }
                    reader.Close();
                    connection.Close();
                }

                if (hotels.Count == 0)
                {
                    MessageBox.Show("There are no available hotels");
                    return;
                }

                hotelComboBox.ItemsSource = hotels;
                hotelComboBox.DisplayMemberPath = "Name";
                hotelComboBox.SelectedValuePath = "Id";
                hotelComboBox.SelectedIndex = 0;
                selectedHotelId = hotels[0].Id;

                InitRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitRooms()
        {
            rooms.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Rooms WHERE Hotel_id = {selectedHotelId} AND is_available = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Room room = new Room();
                        room.Id = int.Parse(reader["Id"].ToString());
                        room.HotelId = int.Parse(reader["Hotel_id"].ToString());
                        room.Category = reader["Category"].ToString();
                        room.Capacity = int.Parse(reader["capacity"].ToString());
                        rooms.Add(room);
                    }
                    reader.Close();
                    connection.Close();
                }

                if (rooms.Count == 0)
                {
                    MessageBox.Show("There are no available rooms");
                    return;
                }

                roomsComboBox.ItemsSource = rooms;
                roomsComboBox.DisplayMemberPath = "Category";
                roomsComboBox.SelectedValuePath = "Id";
                roomsComboBox.SelectedIndex = 0;
                selectedRoomId = rooms[0].Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HotelComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedHotelId = (int)hotelComboBox.SelectedValue;
            InitRooms();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = startDatePicker.SelectedDate ?? DateTime.Now;
                DateTime endDate = endDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);
                int guestCount = int.Parse(guestCountTextBox.Text);
                string guestName = guestNameTextBox.Text;
                if (guestCount < 1 || guestCount > 5)
                {
                    MessageBox.Show("Guest count should be between 1 and 5");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO Bookings(Room_id, Start_date, End_date, Guest_count, Guest_name, is_cancelled) " +
                        $"VALUES({selectedRoomId}, '{startDate.ToString("yyyy-MM-dd")}', '{endDate.ToString("yyyy-MM-dd")}', {guestCount}, '{guestName}', 0)";
                    SqlCommand command = new SqlCommand(query, connection);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        query = $"UPDATE Rooms SET is_available = 0 WHERE Id = {selectedRoomId}";
                        command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Booking successful");
                    }
                    else
                    {
                        MessageBox.Show("Booking failed");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Category { get; set; }
        public int Capacity { get; set; }
    }
}
