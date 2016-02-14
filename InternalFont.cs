using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Valentine
{
    public sealed class InternalFont : IDisposable
    {
        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public Font getFont(int index, float size, FontStyle style = FontStyle.Regular)
        {
            FontFamily[] families = privateFontCollection.Families;
            FontFamily family = index >= families.Length ? FontFamily.GenericSansSerif : families[index];
            return new Font(family, size, style, GraphicsUnit.Pixel);
        }

        public void Init(params byte[][] fontBytes)
        {
            foreach (byte[] bytes in fontBytes)
            {
                AddFontFromBytes(bytes);
            }
        }

        public void Dispose()
        {
            privateFontCollection.Dispose();
        }

        private void AddFontFromBytes(byte[] fontBytes)
        {
               var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontData);
        }
    }
}
