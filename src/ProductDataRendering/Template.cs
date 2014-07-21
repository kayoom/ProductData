using System.IO;

namespace ProductDataRendering
{
    public class Template
    {
        public string HTML { get; set; }

        public void LoadFile(string templateFileName)
        {
            using (var file = File.OpenText(templateFileName))
            {
                HTML = file.ReadToEnd();
            }
        }
    }
}
