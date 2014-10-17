using System;

namespace ProductDataRendering
{
    public class DummyImageService : IImageService
    {
        public Uri GetURL(Uri baseUri, ImageFormat format)
        {
            return baseUri;
        }
    }
}