using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private const int InitCulumnCount = 640;

        public ObservableCollection<Detail> Items { get; private set; } = new ObservableCollection<Detail>();

        public MainWindow2()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            InitColumn(InitCulumnCount);

            InitItems(InitCulumnCount);

            listView.Visibility = Visibility.Visible;
        }

        private void InitColumn(int count)
        {
            var gridView = listView.View as GridView;
            if (gridView == null) return;
            gridView.Columns.Clear();

            var converter = new BooleanToVisibilityConverter();
            for (int columnIndex = 0; columnIndex < count; ++columnIndex)
            {
                var binding = new Binding($"Values[{columnIndex}].Value");
                binding.Converter = converter;

                var factory = new FrameworkElementFactory(typeof(Rectangle));
                factory.SetValue(Rectangle.HeightProperty, 10.0);
                factory.SetValue(Rectangle.WidthProperty, 10.0);
                factory.SetValue(Rectangle.FillProperty, Brushes.LightSkyBlue);
                factory.SetBinding(Rectangle.VisibilityProperty, binding);

                var dataTemplate = new DataTemplate();
                dataTemplate.VisualTree = factory;

                var column = new GridViewColumn();
                column.CellTemplate = dataTemplate;
                column.Width = 38;

                gridView.Columns.Add(column);
            }

            var itemContainerStyle = new Style(typeof(ListViewItem));
            var horizontalContentAlignmentSetter = new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            itemContainerStyle.Setters.Add(horizontalContentAlignmentSetter);

            listView.ItemContainerStyle = itemContainerStyle;
        }

        private void InitItems(int initCulumnCount)
        {
            listView.BeginInit();

            Items.Clear();

            for (int i = 0; i < initCulumnCount; i++)
            {
                Items.Add(new Detail(initCulumnCount));
            }

            listView.EndInit();
        }
    }
}