using Mustache;

namespace ProductDataRendering
{
    public class Renderer
    {
        private readonly Template _template;
        private readonly FormatCompiler _compiler;
        private readonly Generator _generator;

        public Renderer(Template template)
        {
            _template = template;
            _compiler = new FormatCompiler();
            _generator = _compiler.Compile(_template.HTML);
        }

        public string Render(Product product)
        {
            return _generator.Render(product);
        }
    }
}