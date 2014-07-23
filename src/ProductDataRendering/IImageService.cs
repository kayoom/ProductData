using System;

namespace ProductDataRendering
{
    public interface IImageService
    {
        Uri GetURL(Uri baseUri, ImageFormat format);
    }
}