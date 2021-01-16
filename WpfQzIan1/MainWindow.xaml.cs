using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfQzIan1
{
   
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\travels.txt";
        List<Trip> tripList = new List<Trip>();
        
        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            // to read the file
            if (File.Exists(DataFileName))
            {
                string[] tripInfos = File.ReadAllLines(DataFileName);
                foreach (string tripLine in tripInfos)
                {
                    Trip trip = new Trip(tripLine);
                    tripList.Add(trip);
                }
            }

            lvGridTrip.ItemsSource = tripList;
        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            if (txtDestination.Text == "" && txtName.Text == "" && txtPassport.Text == ""
                && dpDeparture.Text == "" && dpWeturn.Text == "")
            {
                var result = MessageBox.Show("No fields can be left blank", "Warning",
                    MessageBoxButton.OK);

                if (result == MessageBoxResult.OK) { return; }
            }
            
            string destination = txtDestination.Text;
            string name = txtName.Text;
            string passport = txtPassport.Text;
            string departure = (string)dpDeparture.Text;
            string weturn = (string)dpWeturn.Text;

            Trip t = new Trip(destination, name, passport, departure, weturn);
            tripList.Add(t);

            ResetValue();
        }

        private void ResetValue()
        {
            // ...
            txtDestination.Text = "";
            txtName.Text = "";
            txtPassport.Text = "";
            dpDeparture.Text = "";
            dpWeturn.Text = "";
            lvGridTrip.Items.Refresh();
            lvGridTrip.SelectedIndex = -1;
            btnDeleteTrip.IsEnabled = false;
            btnUpdateTrip.IsEnabled = false;
        }

        private void btnDeleteTrip_Click(object sender, RoutedEventArgs e)
        {
            Trip tripToDelete = (Trip)lvGridTrip.SelectedItem;
            tripList.Remove(tripToDelete);

            ResetValue();
        }

        private void btnUpdateTrip_Click(object sender, RoutedEventArgs e)
        {
            string newDestination = txtDestination.Text;
            string newName = txtName.Text;
            string newPassport = txtPassport.Text;
            string newDeparture = dpDeparture.Text;
            string newWeturn = dpWeturn.Text;

            Trip itemUpdate = (Trip)lvGridTrip.SelectedItem;

            itemUpdate.Destination = newDestination;
            itemUpdate.Name = newName;
            itemUpdate.Passport = newPassport;
            itemUpdate.Departure = newDeparture;
            itemUpdate.Weturn = newWeturn;

            ResetValue();
        }

        // To recheck
        private void lvGridTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteTrip.IsEnabled = true;
            btnUpdateTrip.IsEnabled = true;

            var selectedTrip = lvGridTrip.SelectedItem;

            if (selectedTrip is Trip)
            {
                Trip trip = (Trip)lvGridTrip.SelectedItem;

                txtDestination.Text = trip.Destination;
                txtName.Text = trip.Name;
                txtPassport.Text = trip.Passport;
                dpDeparture.Text = trip.Departure;
                dpWeturn.Text = trip.Weturn;
            }
        }

        private void BtnSaveSelected_Click(object sender, RoutedEventArgs e)
        {
            // To recheck
            using (StreamWriter writer = new StreamWriter(DataFileName))
            {
                foreach (Trip trip in tripList)
                {
                    writer.WriteLine(trip.ToDataString());
                }
            }
        }
    }
}
