using System;
using System.Linq;
using System.Net;
using ProductDataRendering;
using RestSharp;

namespace ProductCatalog
{
    public class KayoomImageService : IImageService
    {
        private readonly string _apiKey;
        private readonly string _host;

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