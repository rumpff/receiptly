using static receiptly.PrintCommands;
using System;

namespace receiptly
{
    // https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/commands.html
    // https://escpos.readthedocs.io/en/latest/index.html
    internal class PrintCommands
    {
        internal class Character
        {
            /// <summary>
            /// Sets the right-side character spacing to <paramref name="amount"/> × (horizontal or vertical motion unit). 
            /// </summary>
            /// <param name="amount">0 - 255</param>
            /// <returns></returns>
            public static string RightSideSpacing(int amount)
            {
                return "\x1b\x20" + (char)amount;
            }

            /// <summary>
            /// Turn underline mode on/off
            /// </summary>
            /// <param name="enabled">0 = off, 1 = on</param>
            /// <returns></returns>
            public static string UnderlineMode(int enabled)
            {
                return "\x1b\x2d" + (char)enabled;
            }

            /// <summary>
            /// Turn emphasized mode on/off
            /// </summary>
            /// <param name="enabled">0 = off, 1 = on</param>
            /// <returns></returns>
            public static string EmphasizedMode(int enabled)
            {
                return "\x1b\x45" + (char)enabled;
            }

            /// <summary>
            /// Turn double-strike mode on/off
            /// </summary>
            /// <param name="enabled">0 = off, 1 = on</param>
            /// <returns></returns>
            public static string DoubleStrikeMode(int enabled)
            {
                return "\x1b\x47" + (char)enabled;
            }

            /// <summary>
            /// Select character font
            /// </summary>
            /// <param name="font">0 - 4 = Font A - E, 97 = Special Font A, 98 = Special Font B</param>
            /// <returns></returns>
            public static string Font(int font)
            {
                return "\x1b\x4d" + (char)font;
            }
        }

        internal class Position
        {
            /// <summary>
            ///  Moves the print position to the next horizontal tab position.
            ///  <a href="https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/ht.html">More Info</a>
            /// </summary>
            public static string HorizontalTab()
            {
                return "asd";
            }
        }

        internal class Mechanism
        {
            /// <summary>
            ///  Moves the print head to the standby position.
            ///  <a href="https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/esc_less_than_sign.html">More Info</a>
            /// </summary>
            public static string ReturnHome()
            {
                return "\x1B\x3C";
            }

            /// <summary>
            ///  Cut paper
            ///  <a href="https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/gs_cv.html">More Info</a>
            /// </summary>
            /// <param name="shape">0 = full, 1 = partial</param>
            /// <returns></returns>
            public static string Cut(char shape = '0')
            {
                return "\x1d\x56" + shape;
            }
        }

        internal class Barcode
        {
            public static string Generate(string code)
            {
                return "\x1d\x6b\x08\x7b\x42" + code + "\x00";
            }

            /// <summary>
            ///  Select print position of HRI characters
            /// </summary>
            /// <param name="mode">0 = Disabled, 1 = Above, 2 = Below, 3 = Above and below</param>
            /// <returns></returns>
            public static string SetHRI(int mode)
            {
                return "\x1d\x48" + (char)mode;
            }
        }
    }
}
