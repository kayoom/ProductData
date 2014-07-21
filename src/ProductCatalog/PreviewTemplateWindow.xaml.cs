using System.Windows;
using ProductDataRendering;

namespace ProductCatalog
{
    /// <summary>
    /// Interaction logic for PreviewTemplateWindow.xaml
    /// </summary>
    public partial class PreviewTemplateWindow : Window
    {
        public Product Product { get; set; }
        public string TemplateFileName { get; set; }

        public PreviewTemplateWindow()
        {
            InitializeComponent();
            Loaded += (sender, args) => Refresh();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            var template = new Template();
            template.LoadFile(TemplateFileName);

            var renderer = new Renderer(template);
            var html = renderer.Render(Product);

            Browser.LoadHTML(html);
        }
    }
}
