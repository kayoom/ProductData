using System;
using System.Linq;
using System.Net;
using System.Windows;
using ProductDataRendering;
using RestSharp;

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

            var renderer = new Renderer(template, new KayoomImageService("http://kis.kayoom.com", "r8VhcAgCGjQWH7R20SBukZYEqnOfodz9"));
            var html = renderer.Render(Product);

            Browser.LoadHTML(html);
        }
    }
    
    public class KayoomImageService : IImageService
    {
        private readonly string _host;
        private readonly string _apiKey;

        public KayoomImageService(string host, string apiKey)
        {
            _host = host;
            _apiKey = apiKey;
        }

        public Uri GetURL(Uri baseUri, ImageFormat format)
        {
            var client = new RestClient(_host);
            var request = new RestRequest("/");

            request.AddParameter("url", baseUri.ToString());
            request.AddParameter("width", format.Width);
            request.AddParameter("height", format.Height);
            request.AddParameter("format", format.Type);

            request.AddHeader("X-ApiKey", _apiKey);

            client.FollowRedirects = false;
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.MovedPermanently)
                return baseUri;

            return new Uri((string) response.Headers.First(h => h.Name == "Location").Value);
        }
    }
}
