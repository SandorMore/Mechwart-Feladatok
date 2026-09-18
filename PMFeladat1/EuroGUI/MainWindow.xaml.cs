using System.IO.Packaging;
using System.Text;
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
using System.Collections.ObjectModel;

namespace EuroGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Models.Model> _models = new();
        private const string CONNECTION_STRING = "Server=localhost;Port=3306;Database=eurovizio;Uid=root;Pwd=;";
        private MySqlConnection connection;
        private Models.Model selectedItem;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(CONNECTION_STRING);
                lb_conn_status.Content += " stable";
            }
            catch(MySqlException ex)
            {
                lb_conn_status.Content += " failed";
                MessageBox.Show($"Hiba: {ex.Message}");
            }
            load_data();
        }
        private async void load_data()
        {
            
            if(connection == null)
            {
                return;
            }
            _models.Clear();
            await connection.OpenAsync();

            using var cmd = new MySqlCommand("SELECT id, ev, eloado, cim, helyezes, pontszam FROM dal", connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                _models.Add(new Models.Model
                {
                    id = reader.GetInt32(reader.GetOrdinal("id")),
                    ev = reader.GetInt32(reader.GetOrdinal("ev")),
                    eloado = reader.GetString(reader.GetOrdinal("eloado")),
                    cim = reader.GetString(reader.GetOrdinal("cim")),
                    helyezes = reader.GetInt32(reader.GetOrdinal("helyezes")),
                    pontszam = reader.GetInt32(reader.GetOrdinal("pontszam"))
                });
            }
            dg.ItemsSource = _models;
            connection.Close();
        }
        
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg == null)
                return;

            selectedItem = dg.SelectedItem as Models.Model;
        }

        private async void btn_szervezo_Click(object sender, RoutedEventArgs e)
        {
            if(connection == null)
            {
                return;
            }

            if(selectedItem == null)
            {
                return;
            }

            try
            {
                await connection.OpenAsync();


                using var cmd = new MySqlCommand(
                    "SELECT v.orszag FROM verseny v JOIN dal d ON v.ev = d.ev WHERE d.id = @id",
                    connection);
                cmd.Parameters.AddWithValue("@id", selectedItem.id);

                var orszag = await cmd.ExecuteScalarAsync();
                lb_selected.Content = orszag?.ToString() ?? "Nincs találat";

            }
            finally
            {
                connection.Close();
            }
        }

        private async void btn_mv_Click(object sender, RoutedEventArgs e)
        {
            if (connection == null)
                return;
            try
            {
                await connection.OpenAsync();

                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM dal WHERE orszag = \"Magyarország\"", connection);
                var magyarok = await cmd.ExecuteScalarAsync();

                lb_mv.Content = magyarok?.ToString() ?? "Nincs találat!";

            }
            finally
            {
                connection.Close();
            }
        }
    }
}