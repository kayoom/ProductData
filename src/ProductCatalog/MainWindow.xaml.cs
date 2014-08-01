using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Awesomium.Core;
using Microsoft.Win32;
using ProductData;
using ProductData.DOM;
using Product = ProductDataRendering.Product;

namespace ProductCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Data _data;

        public MainWindow()
        {
            _data = new Data();
            DataContext = _data;
            WebCore.Initialize(WebConfig.Default);

            Loaded += OnLoaded;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
                return;

            _data.Load(args[1], null);

            if (args.Length < 3)
                return;

            ShowPreview(args[2], _data.Products.First());
        }

        private void LoadXMLClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "XML Files | *.xml"
            };

            var result = dialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            _data.Load(dialog.FileName, null);
        }

        private void PreviewTemplateClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var product = (Product) button.DataContext;

            var dialog = new OpenFileDialog
            {
                Filter = "Handlebars Template | *.hbs"
            };

            var result = dialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            ShowPreview(dialog.FileName, product);
        }

        private static void ShowPreview(string templateFileName, Product product)
        {
            var previewWindow = new PreviewTemplateWindow
            {
                TemplateFileName = templateFileName,
                Product = product
            };

            previewWindow.ShowDialog();
        }
    }

    public class Data
    {
        public Data()
        {
            Products = new ObservableCollection<Product>();
        }

        public ObservableCollection<Product> Products { get; private set; }

        public void Load(string fileName, string langCode)
        {
            var catalog = Catalog.Load(fileName);

            Products.Clear();
            foreach (var product in catalog.Products.OrderBy(p => p.Names.Get(langCode)))
            {
                Products.Add(new Product(product, catalog, langCode, "EUR"));
            }
        }
    }
}
