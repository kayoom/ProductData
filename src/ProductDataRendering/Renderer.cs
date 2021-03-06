using Mustache;

namespace ProductDataRendering
{
    public class Renderer
    {
        private readonly FormatCompiler _compiler;
        private readonly Generator _generator;
        private readonly IImageService _imageService;
        private readonly Template _template;

        public Renderer(Template template, IImageService imageService)
        {
            _template = template;
            _imageService = imageService;
            _compiler = new FormatCompiler();
            _compiler.RegisterTag(new ImgUrlTagDefinition(imageService), true);

            _generator = _compiler.Compile(_template.HTML);
        }

        public string Render(Product product)
        {
            return _generator.Render(product);
        }
    }
}