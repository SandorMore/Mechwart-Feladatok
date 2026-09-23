using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using SQLitePCL;
using System.Collections.ObjectModel;
using System.IO;
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

namespace PMFeladat2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int id = 0;
        private ObservableCollection<VedettAllat> _collection = new();
        private SqliteConnection connection;
        private List<VedettAllat> vedettAllatok = new List<VedettAllat>();
        private const string CONNECTION_STRING = "Data Source = vedett.db";
        public MainWindow()
        {
            InitializeComponent();
            Batteries.Init();
            establish_connection();
        }
        
        private void establish_connection()
        {
            try 
            {
                connection = new SqliteConnection(CONNECTION_STRING);
                
            }
            catch (Exception ex)
            {
                lb_connection_status.Content = "Status: Failed to connect";
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            lb_connection_status.Content = "Status: Stable Connection";

        }
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void read_file()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a file to read from";
            
            
            if (ofd.ShowDialog() == null)
                return;

            string selectedFile = ofd.FileName;
            
            if (selectedFile == null || selectedFile.Length <= 0)
                return;
            
            load_data(selectedFile);
        }
        private void load_data(string selectedFile)
        {
            vedettAllatok = File.ReadAllLines(selectedFile).Select(_ => new VedettAllat(_)).ToList();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            read_file();
            await load_data_into_grid();

        }

        private async Task<int> get_besorolas(string _fajta)
        {
            using var sqliteCommand = new SqliteCommand("SELECT id FROM besorolas WHERE nev = @fajta", connection);
            sqliteCommand.Parameters.AddWithValue("@fajta", _fajta);
            var result = await sqliteCommand.ExecuteScalarAsync();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        private async Task load_data_into_grid()
        {
            if (connection == null)
                return;

            _collection.Clear();

            try
            {
                await connection.OpenAsync();

                foreach (var item in vedettAllatok)
                {
                    int besorolasId = await get_besorolas(item.fajta);

                    using var sqliteCommand = new SqliteCommand(
                        "INSERT INTO vedett_allat (id, nev, ertek, ev, besorolas_id) VALUES (@id, @nev, @ertek, @ev, @besorolas_id);", connection
                    );
                    item.id = id++;
                    sqliteCommand.Parameters.AddWithValue("@id", item.id);
                    sqliteCommand.Parameters.AddWithValue("@nev", item.nev);
                    sqliteCommand.Parameters.AddWithValue("@ertek", item.ertek);
                    sqliteCommand.Parameters.AddWithValue("@ev", item.since);
                    sqliteCommand.Parameters.AddWithValue("@besorolas_id", besorolasId);

                    await sqliteCommand.ExecuteNonQueryAsync();

                    _collection.Add(item); // <-- this is what actually fills the grid
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}