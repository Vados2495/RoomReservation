using RoomReservation.Model;
using RoomReservation.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomReservation.ViewModel
{
    public class HotelBookingViewModel : INotifyPropertyChanged
    {
        private List<Hotel> hotels;
        private List<Room> rooms;
        private BookingModel bookingModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Hotel> Hotels
        {
            get { return hotels; }
            set
            {
                hotels = value;
                OnPropertyChanged(nameof(Hotels));
            }
        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public BookingModel BookingModel
        {
            get { return bookingModel; }
            set
            {
                bookingModel = value;
                OnPropertyChanged(nameof(BookingModel));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
