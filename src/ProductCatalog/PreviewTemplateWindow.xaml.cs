using System.IO;
using System.Threading;
using System.Windows;
using ProductDataRendering;

namespace ProductCatalog
{
    /// <summary>
    /// Interaction logic for PreviewTemplateWindow.xaml
    /// </summary>
    public partial class PreviewTemplateWindow : Window
    {
        public PreviewTemplateWindow()
        {
            InitializeComponent();
            Loaded += (sender, args) => Refresh();
            Loaded += InitWatcher;
        }

        public Product Product { get; set; }
        public string TemplateFileName { get; set; }
        public FileSystemWatcher Watcher { get; set; }
        public bool DoSaveHTML { get; set; }

        private void InitWatcher(object sender, RoutedEventArgs e)
        {
            Watcher = new FileSystemWatcher(Path.GetDirectoryName(TemplateFileName));
            Watcher.Changed += WatcherOnChanged;
            Watcher.Created += WatcherOnChanged;
            Watcher.Renamed += WatcherOnChanged;
            Watcher.EnableRaisingEvents = LivePreview.IsChecked.HasValue && LivePreview.IsChecked.Value;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (fileSystemEventArgs.FullPath.ToLower() != Path.GetFullPath(TemplateFileName).ToLower())
                return;
            if (fileSystemEventArgs.ChangeType != WatcherChangeTypes.Deleted)
                Refresh();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            var template = LoadTemplate();

            var renderer = new Renderer(template,
                new KayoomImageService("http://kis.kayoom.com", "r8VhcAgCGjQWH7R20SBukZYEqnOfodz9"));
            var html = renderer.Render(Product);

            if(DoSaveHTML)
                File.WriteAllText(@"c:\tmp\example.html", html);
            
            Dispatcher.Invoke(() =>
            {
                Browser.WebSession.ClearCache();
                Browser.LoadHTML(html);
            });
        }

        private Template LoadTemplate()
        {
            var template = new Template();
            var fileLoaded = false;
            while (!fileLoaded)
            {
                try
                {
                    template.LoadFile(TemplateFileName);
                    fileLoaded = true;
                }
                catch (IOException)
                {
                    Thread.Sleep(100);
                }
            }
            return template;
        }

        private void LivePreviewChecked(object sender, RoutedEventArgs e)
        {
            if (Watcher != null)
                Watcher.EnableRaisingEvents = (LivePreview.IsChecked.HasValue && LivePreview.IsChecked.Value);
        }
    }
}