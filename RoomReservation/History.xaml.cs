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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = dataTable.NewRow();
            dataTable.Rows.Add(newRow);
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataAdapter.Update(dataTable);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update data: {ex.Message}");
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItems != null && usersGrid.SelectedItems.Count > 0)
            {
                List<DataRowView> selectedRows = usersGrid.SelectedItems.Cast<DataRowView>().ToList();

                if (MessageBox.Show($"Are you sure you want to delete {selectedRows.Count} rows?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (DataRowView selectedRow in selectedRows)
                        {
                            DataRow row = selectedRow.Row;
                            row.Delete();
                        }

                        dataAdapter.Update(dataTable);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete data: {ex.Message}");
                    }
                }
            }
        }

        private void HotelsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Content content = new Content();
            content.Show();
        }

        private void RoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Rooms rooms = new Rooms();
            rooms.Show();
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Users users = new Users();
            users.Show();
        }
    }
}
