using System;
using System.Runtime.InteropServices;

namespace receiptly
{
    public class RawPrinterHelper
    {
        // Structure and API declarations:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName = "";
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile = "";
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType = "";
        }

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        public static bool PrintBytes(string szPrinterName, byte[] pBytes, Int32 dwCount)
        {
            Int32 dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            di.pDocName = "RAW Document";
            di.pDataType = "RAW";
            bool bSuccess = false;
            IntPtr pUnmanagedBytes = IntPtr.Zero;

            try
            {
                // Open the printer
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page
                        if (StartPagePrinter(hPrinter))
                        {
                            // Allocate unmanaged memory for the data
                            pUnmanagedBytes = Marshal.AllocCoTaskMem(dwCount);
                            Marshal.Copy(pBytes, 0, pUnmanagedBytes, dwCount);

                            // Write your data to the printer
                            bSuccess = WritePrinter(hPrinter, pUnmanagedBytes, dwCount, out dwWritten);

                            // End the page
                            EndPagePrinter(hPrinter);
                        }
                        // End the document
                        EndDocPrinter(hPrinter);
                    }
                }
            }
            finally
            {
                // Free the unmanaged memory
                if (pUnmanagedBytes != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pUnmanagedBytes);
                }
                // Close the printer
                if (hPrinter != IntPtr.Zero)
                {
                    ClosePrinter(hPrinter);
                }
            }
            return bSuccess;
        }
    }
}