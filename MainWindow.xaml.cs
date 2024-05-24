using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace EuroGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string CONNECTION_STRING = "datasource=127.0.0.1;port=3306;database=eurovizio;username=root;password=;";

        ObservableCollection<Dal> dalok = new();

        public MainWindow()
        {
            InitializeComponent();
            AdatBetolt();
            dgDalok.ItemsSource = dalok;
            dgDalok.SelectedIndex = 0;
        }

        public void AdatBetolt() {
            MySqlConnection kapcsolat = new(CONNECTION_STRING);
            kapcsolat.Open();

            MySqlCommand parancs = new("SELECT dal.ev, helyezes, pontszam, eloado, cim, dal.orszag, datum, id FROM dal JOIN verseny ON dal.ev = verseny.ev;", kapcsolat);
            MySqlDataReader olvaso = parancs.ExecuteReader();

            while (olvaso.Read()) {
                dalok.Add(new Dal(
                    olvaso.GetInt32(0),
                    olvaso.GetInt32(1),
                    olvaso.GetInt32(2),
                    olvaso.GetString(3),
                    olvaso.GetString(4),
                    olvaso.GetString(5),
                    olvaso.GetDateTime(6).ToString(),
                    olvaso.GetInt32(7)
                ));
            }

            olvaso.Close();
            kapcsolat.Close();
        }

        private void btnFeladat4_Click(object sender, RoutedEventArgs e)
        {
            if (dgDalok.SelectedIndex != -1) {
                lblFeladat4.Content = (dgDalok.SelectedItem as Dal).Datum;
            }
        }

        private void btnFeladat5_Click(object sender, RoutedEventArgs e)
        {
            lblFeladat5.Content = dalok.Count(x => x.Orszag == "Magyarország");
        }

        private void btnFeladat7_Click(object sender, RoutedEventArgs e)
        {
            string orszag = tbFeladat7.Text;
            if (dalok.Count(x => x.Orszag == orszag) == 0) {
                MessageBox.Show("Nincs ilyen ország");
                return;
            }

            MessageBox.Show($"{orszag} versenyzőinek átlaga: {Math.Round(dalok.Where(x => x.Orszag == orszag).Average(x => x.Helyezes))}");
        }

        private void btnTorol_Click(object sender, RoutedEventArgs e)
        {
            if (dgDalok.SelectedIndex != -1) {
                int id = (dgDalok.SelectedItem as Dal).Id;

                MySqlConnection kapcsolat = new(CONNECTION_STRING);
                kapcsolat.Open();

                MySqlCommand parancs = new($"DELETE FROM dal WHERE id = {id};", kapcsolat);
                parancs.ExecuteNonQuery();

                kapcsolat.Close();
                dalok.Remove(dgDalok.SelectedItem as Dal);
            }
        }
    }
}
