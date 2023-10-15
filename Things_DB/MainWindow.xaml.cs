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
        private int qweryItem;
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
            catch (Exception ex)
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
            try
            {
                List<NameThigs> types = dbthingsClient.TypeAll();
                ThingsListBox.Items.Clear();
                types.ForEach(type => ThingsListBox.Items.Add(type));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void allSupplyers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<NameThigs> types = dbthingsClient.SupplierAll();
                ThingsListBox.Items.Clear();
                types.ForEach(supplier => ThingsListBox.Items.Add(supplier));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void maxCount_Click(object sender, RoutedEventArgs e)
        {
            List<Things> things = dbthingsClient.maxCount();
            ThingsListBox.Items.Clear();
            things.ForEach(thing => ThingsListBox.Items.Add(thing));
        }

        private void minCount_Click(object sender, RoutedEventArgs e)
        {
            List<Things> things = dbthingsClient.minCount();
            ThingsListBox.Items.Clear();
            things.ForEach(thing => ThingsListBox.Items.Add(thing));
        }

        private void minCost_Click(object sender, RoutedEventArgs e)
        {
            List<Things> things = dbthingsClient.minCost();
            ThingsListBox.Items.Clear();
            things.ForEach(thing => ThingsListBox.Items.Add(thing));
        }
        private void maxCost_Click(object sender, RoutedEventArgs e)
        {
            List<Things> things = dbthingsClient.maxCost();
            ThingsListBox.Items.Clear();
            things.ForEach(thing => ThingsListBox.Items.Add(thing));
        }


        private void Result_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (qweryItem == 0)
                {
                    List<Things> things = dbthingsClient.ChoisType(Parametr.Text);
                    ThingsListBox.Items.Clear();
                    things.ForEach(thing => ThingsListBox.Items.Add(thing));
                }
                if (qweryItem == 1)
                {
                    List<Things> things = dbthingsClient.ChoisSupplier(Parametr.Text);
                    ThingsListBox.Items.Clear();
                    things.ForEach(thing => ThingsListBox.Items.Add(thing));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during select object list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void paramChois_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            qweryItem = paramChois.SelectedIndex;
        }
    }
}
