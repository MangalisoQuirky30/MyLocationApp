using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyLocationApp.Models
{
    
    public class MyLocation : INotifyPropertyChanged
    {

        public string Address { get; set; }
        public string Description { get; set; }

        Position _position;

        public Position Position
        {
            get => _position;
            set
            {
                if (!_position.Equals(value))
                {
                    _position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /*
        public MyLocation(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }
        */
    } 
}
