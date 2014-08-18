using System;
using System.Collections.Generic;
using System.IO;
using Mustache;

namespace ProductDataRendering
{
    public class ImgUrlTagDefinition : InlineTagDefinition
    {
        private readonly IImageService _imageService;

        public ImgUrlTagDefinition(IImageService imageService) : base("imgurl")
        {
            _imageService = imageService;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[]
            {
                new TagParameter("baseUrl"),
                new TagParameter("width") {DefaultValue = -1},
                new TagParameter("height") {DefaultValue = -1},
                new TagParameter("format") {DefaultValue = "jpg"}
            };
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            int width;
            if (arguments["width"] is decimal)
                width = Convert.ToInt32((decimal) arguments["width"]);
            else
                width = (int) arguments["width"];

            int height;
            if (arguments["height"] is decimal)
                height = Convert.ToInt32((decimal) arguments["height"]);
            else
                height = (int) arguments["height"];

            var imageFormat = new ImageFormat
            {
                Width = width,
                Height = height,
                Type = (string) arguments["format"]
            };

            var baseUrl = (string) arguments["baseUrl"];
            var url = _imageService.GetURL(new Uri(baseUrl), imageFormat);
            writer.Write(url.ToString());
        }
    }
}