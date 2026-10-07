using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfDavITFutures
{
    public static class Extensions
    {
        public static float x(this float f)
        {
            return PageSize.A4.Width - f;
        }

        public static float y(this float f)
        {
            return PageSize.A4.Height - f;
        }
    }
}
