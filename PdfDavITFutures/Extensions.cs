using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfDavITFutures
{
    /// <summary>
    /// Extension methods for coordinate calculation and unit conversion in PDF document generation.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Calculates the horizontal offset from the right boundary of a standard A4 page.
        /// </summary>
        /// <param name="f">Offset from the right margin in points.</param>
        /// <returns>The absolute X coordinate.</returns>
        public static float x(this float f)
        {
            return PageSize.A4.Width - f;
        }

        /// <summary>
        /// Calculates the vertical offset from the top boundary of a standard A4 page.
        /// </summary>
        /// <param name="f">Offset from the top margin in points.</param>
        /// <returns>The absolute Y coordinate.</returns>
        public static float y(this float f)
        {
            return PageSize.A4.Height - f;
        }

        /// <summary>
        /// Converts a millimeter value to PDF typographic points (1 mm ≈ 2.83465 pt).
        /// </summary>
        /// <param name="mm">Measurement in millimeters.</param>
        /// <returns>Value converted to PDF points.</returns>
        public static float ToPdfPoints(this float mm)
        {
            return mm * (72f / 25.4f);
        }
    }
}
