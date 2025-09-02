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

namespace Practise_task_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<COper> obj_results = new List<COper>();
        public MainWindow()
        {
            InitializeComponent();
            DataBaseSQL.initializeDB();
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(num_a.Text, out int a))
            {
                MessageBox.Show("Błędna pierwsza liczba");
                return;
            }

            if (!int.TryParse(num_b.Text, out int b))
            {
                MessageBox.Show("Błędna druga liczba");
                return;
            }

            if (cmb_oper.SelectedItem == null)
            {
                MessageBox.Show("Wybierz działanie!");
                return;
            }


            string operation = cmb_oper.Text;
            var service = new CalculateService();

            int result = await service.calculateResult(a, b, operation);

            COper obj_result = new COper(a, b, operation, result);
            obj_results.Add(obj_result);
            addObjectToGrid(result_grid, obj_result, obj_results.Count);
        }

        private void addObjectToGrid(Grid grid, COper obj_result, int i)
        {
            
            
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
         
            
           
            var op = obj_result;

            TextBlock txtA = new TextBlock { Text = op.A.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(txtA, i);
            Grid.SetColumn(txtA, 0);
            grid.Children.Add(txtA);

            TextBlock txtB = new TextBlock { Text = op.B.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(txtB, i );
            Grid.SetColumn(txtB, 1);
            grid.Children.Add(txtB);

            TextBlock txtOp = new TextBlock { Text = op.Operation,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(txtOp, i);
            Grid.SetColumn(txtOp, 2);
            grid.Children.Add(txtOp);

            TextBlock txtResult = new TextBlock { Text = op.Result.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(txtResult, i);
            Grid.SetColumn(txtResult, 3);
            grid.Children.Add(txtResult);
            
        }

        private void create_first_grid(List<COper> obj_results)
        {
            var rows_to_remove = result_grid.Children
            .OfType<UIElement>()
            .Where(e => Grid.GetRow(e) != 0)
            .ToList();

            foreach (var row_to_remove in rows_to_remove)
            {
                result_grid.Children.Remove(row_to_remove);
            }
            while (result_grid.RowDefinitions.Count > 1)
            {
                result_grid.RowDefinitions.RemoveAt(result_grid.RowDefinitions.Count - 1);
            }
            for (int i = 0; i < obj_results.Count; i++)
            {
                addObjectToGrid(result_grid, obj_results[i], i+1);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string file_path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "results.xml");
            XMLConvertion.serialize_file(obj_results, file_path);
            DataBaseSQL.saveXMLFile(file_path);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            obj_results = XMLConvertion.deserialize_file();
            if (obj_results.Count > 0)
            {
                create_first_grid(obj_results);
            }
        }
    }
}