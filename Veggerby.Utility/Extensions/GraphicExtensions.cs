namespace Veggerby.Utility.Extensions
{
    public static class GraphicExtensions
    {
        public static string FontColorForBackground(this string color, string darkFontColor = "#333", string lightFontColor = "#fff")
        {
            // source http://stackoverflow.com/questions/946544/good-text-foreground-color-for-a-given-background-color
            var c = System.Drawing.ColorTranslator.FromHtml(color);

            var gray = c.R * 0.299 + c.G * 0.587 + c.B * 0.114;

            if (gray >= 186)
            {
                return darkFontColor;
            }

            return lightFontColor;
        }

    }
}
