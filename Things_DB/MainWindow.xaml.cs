using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Things_DB.Model;

namespace Things_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBthingsClient dbthingsClient;
        public MainWindow()
        {
            dbthingsClient = new DBthingsClient(new ConnectionThingsDB());
            InitializeComponent();
            updateFVList();
        }

        private void bdConnectionTest_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
            ConnectionThingsDB connectionThingsDB = new ConnectionThingsDB();
                using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
                {
                    MessageBox.Show("Connection is ok", "Connected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // обнавление листа
        private void updateFVList()
        {
            try
            {
                // обновление списка объектов (можно придумать рациональный способ)
                List<Things> frutveg = dbthingsClient.SelectAll();
                ThingsListBox.Items.Clear();
                frutveg.ForEach(frutveget => ThingsListBox.Items.Add(frutveget));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void allThings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                updateFVList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void allTypes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void allSupplyers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void maxCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minCount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minCost_Click(object sender, RoutedEventArgs e)
        {

        } 
        private void maxCost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
