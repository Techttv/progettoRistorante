using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Fonts;

namespace progettoRistorante.Classes
{
    class MyFontResolver : IFontResolver
    {
        public string DefaultFontName => throw new NotImplementedException();

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var fs = File.Open(faceName, FileMode.Open))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("Merchant Copy Doublesize", StringComparison.CurrentCultureIgnoreCase))
            {
                return new FontResolverInfo("Font/merchant.ttf");
            }
            if (familyName.Equals("Gabriola", StringComparison.CurrentCultureIgnoreCase))
            {
                return new FontResolverInfo("Font/Gabriola.ttf");
            }
            return null;
        }

    }
}
