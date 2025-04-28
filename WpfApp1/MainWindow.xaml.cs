using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = halaszg; uid = root; password = ''");
        public MainWindow()
        {
            InitializeComponent();
            listboxfelvisz();
        }

        private void listboxfelvisz()
        {
            //listbox feltöltése
            kapcs.Open();
            MySqlCommand parancs = new MySqlCommand("SELECT * FROM halaszg_termek", kapcs);
            MySqlDataReader lekerdezes = parancs.ExecuteReader();
            while (lekerdezes.Read())
            {
                lbTermekek.Items.Add(lekerdezes["id"] + " " + lekerdezes["cikkszam"] + " " + lekerdezes["megnevezes"]);
            }
            kapcs.Close();
            lekerdezes.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cikkszam = txtCikkszam.Text;
            var megnevezes = txtMegnevezes.Text;
            kapcs.Open();
            MySqlCommand parancs = new MySqlCommand($"INSERT INTO halaszg_termek (cikkszam, megnevezes) VALUES ('{cikkszam}', '{megnevezes}')", kapcs);
            parancs.ExecuteNonQuery();
            kapcs.Close();
            lbTermekek.Items.Clear();
            listboxfelvisz();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ujCikkszam = kivCikkszam.Text;
            var ujMegnevezes = kivMegnevezes.Text;
            kapcs.Open();
            MySqlCommand parancs = new MySqlCommand($"UPDATE halaszg_termek SET cikkszam = '{ujCikkszam}', megnevezes = '{ujMegnevezes}' WHERE cikkszam = '{ujCikkszam}'", kapcs);
            parancs.ExecuteNonQuery();
            kapcs.Close();
            lbTermekek.Items.Clear();
            listboxfelvisz();
            kivCikkszam.Text = "";
            kivMegnevezes.Text = "";
        }

        private void lbTermekek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbTermekek.SelectedItem != null)
                {
                    var kivCikk = lbTermekek.SelectedItem.ToString().Split(' ')[1];
                    var kivMeg = lbTermekek.SelectedItem.ToString().Split(' ')[2];
                    kivCikkszam.Text = kivCikk;
                    kivMegnevezes.Text = kivMeg;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

    }
}
