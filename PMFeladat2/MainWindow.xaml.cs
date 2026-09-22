using Microsoft.Data.Sqlite;
using Microsoft.Win32;
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
        private SqliteConnection connection;
        private List<VedettAllat> vedettAllatok = new List<VedettAllat>();
        private const string CONNECTION_STRING = "Data Source = vedett.db";
        public MainWindow()
        {
            InitializeComponent();
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
            load_data( selectedFile );
        }
        private void load_data(string selectedFile)
        {
            vedettAllatok = File.ReadAllLines(selectedFile).Select(_ => new VedettAllat(_)).ToList();
        }
    }
}