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
    /// Логика взаимодействия для BookingUpdate.xaml
    /// </summary>
    public partial class BookingUpdate : Window
    {
        private string connectionString = "Data Source = DESKTOP-UBC2MQ9; Initial Catalog = Reservation; Trusted_Connection = True";
        private int bookingId;
        public BookingUpdate()
        {
            InitializeComponent();
            LoadBookingDetails();
        }
        private void LoadBookingDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM Bookings WHERE Id = {bookingId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        startDatePicker.SelectedDate = DateTime.Parse(reader["Start_date"].ToString());
                        endDatePicker.SelectedDate = DateTime.Parse(reader["End_date"].ToString());
                        guestCountTextBox.Text = reader["Guest_count"].ToString();
                        guestNameTextBox.Text = reader["Guest_name"].ToString();
                        isCancelledCheckBox.IsChecked = (bool)reader["is_cancelled"];
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = startDatePicker.SelectedDate ?? DateTime.Now;
                DateTime endDate = endDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);
                int guestCount = int.Parse(guestCountTextBox.Text);
                string guestName = guestNameTextBox.Text;
                bool isCancelled = isCancelledCheckBox.IsChecked ?? false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE Bookings SET Start_date = '{startDate.ToString("yyyy-MM-dd")}', End_date = '{endDate.ToString("yyyy-MM-dd")}', Guest_count = {guestCount}, Guest_name = '{guestName}', is_cancelled = {(isCancelled ? 1 : 0)} WHERE Id = {bookingId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MessageBox.Show("Booking updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Booking update failed");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
