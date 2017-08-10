using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa.Scaner2D
{
    public delegate void CallBackScaner(string data);

    public class Scaner
    {
        private SerialPort portConnection;
        private string portName;

        public Scaner(string portName)
        {
            this.portName = portName;
        }
      
        public void InitDevice()
        {
            this.portConnection = new System.IO.Ports.SerialPort(this.portName, 9600, Parity.None, 8, StopBits.One);
        }

        public bool TestDevice()
        {
            if (this.portConnection != null)
            {
                this.portConnection.Open();
                byte a = 0x16;
                byte b = 0x07;
                byte c = 0x0D;
                this.portConnection.Write(new byte[]{a, b, c}, 0, 3);

                this.portConnection.Close();
                return true;
            }
            return false;
        }

        public void Open()
        {
            this.portConnection.Open();
        }

        public void Close()
        {
            this.portConnection.Close();
        }

        public string ReadData()
        {
            string result = "";
            if (this.portConnection.IsOpen)
            {
            var sizeMessage = this.portConnection.BytesToRead;
            var stream = this.portConnection.BaseStream;
            byte[] data = new byte[sizeMessage];
            stream.Read(data, 0, sizeMessage);
            result = Encoding.UTF8.GetString(data, 0, sizeMessage);
            }
            return result;
       }
    }

}
