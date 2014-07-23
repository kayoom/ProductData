namespace ProductDataRendering
{
    public class ImageFormat
    {
        public ImageFormat()
        {
            Width = -1;
            Height = -1;
            Type = "jpg";
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Type { get; set; }
    }
}