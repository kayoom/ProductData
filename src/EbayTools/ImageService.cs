using System;
using System.Collections.Generic;
using eBay.Service.Core.Sdk;
using ProductDataRendering;

namespace EbayTools
{
    public class ImageService : IImageService
    {
        private readonly ApiContext _apiContext;
        private readonly Dictionary<Uri, Uri> _cache;
        private UploadSiteHostedPicturesCall _call;

        public ImageService(ApiContext apiContext)
        {
            _apiContext = apiContext;
            _cache = new Dictionary<Uri, Uri>();
        }

        public Uri GetURL(Uri baseUri, ImageFormat format)
        {
            if (_cache.ContainsKey(baseUri))
                return _cache[baseUri];

            return _cache[baseUri] = UploadToEPS(baseUri);
        }

        private Uri UploadToEPS(Uri baseUri)
        {
            _call = new UploadSiteHostedPicturesCall(_apiContext);

            return new Uri(_call.Execute(baseUri.ToString()));
        }
    }
}