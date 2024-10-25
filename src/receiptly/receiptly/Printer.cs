using System.Text;


namespace receiptly
{
    internal class Printer
    {
        private string m_Name;
        private List<byte> m_printQueue;

        public Printer(string name) { 
            m_Name = name;
            m_printQueue = new List<byte>();
        }

        public bool Print(bool clearQueue = true)
        {
            byte[] byteQueue = m_printQueue.ToArray();
            bool result =  RawPrinterHelper.PrintBytes(m_Name, byteQueue, byteQueue.Length);

            if (clearQueue)
                ClearQueue();

            return result;
        }

        public void Queue(byte data)
        {
            m_printQueue.Add(data);
        }

        public void Queue(byte[] data)
        {
            m_printQueue.AddRange(data);
        }


        /// <summary>
        /// Add data to printer queue
        /// </summary>
        /// <param name="data"></param>
        public void Queue(string data)
        {
            Queue(Encoding.ASCII.GetBytes(data));
        }

        /// <summary>
        /// Add data with a line break to printer queue
        /// </summary>
        /// <param name="data"></param>
        public void QueueLine(string data)
        {
            Queue(data + "\n");
        }

        public void ClearQueue()
        {
            m_printQueue = new List<byte>();
        }

    }
}
