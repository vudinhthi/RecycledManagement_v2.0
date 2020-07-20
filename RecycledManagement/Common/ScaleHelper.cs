using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public delegate void DataRecieveHandler(object sender, object param);
    public class ScaleHelper
    {
        public string IPServer { get; set; }
        public int PortServer { get; set; }
        private Stream _stream;
        private TcpClient _client;
        private Decimal _value;

        public bool isAllowScale { get; set; }
        public event DataRecieveHandler DataChanged;

        public static  bool isFailed;
        public decimal _Value
        {
            get { return _value; }
            set
            {
                _value = value;
                onDataRecieved(value);
            }
        }

        private void onDataRecieved(Decimal Value)
        {
            DataChanged?.Invoke(this, Value);
        }
        public ScaleHelper()
        {
            if (_client == null)
            {
                _client = new TcpClient();
            }
            //Task.Factory.StartNew(() => ReadData());
        }
        public void ReadData()
        {
            try
            {
                _client.Connect(IPServer, PortServer);
                _stream = _client.GetStream();

                var reader = new StreamReader(_stream);
                //StreamReader reader ;
                Regex digits;
                Match mx;
                string str = "";
                while (true)
                {
                    if (isAllowScale)
                    {
                        reader = new StreamReader(_stream);
                        str = reader.ReadLine();
                        //Debug.WriteLine(str.Length);
                        digits = new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*");
                        mx = digits.Match(str);

                        _Value = mx.Success ? Convert.ToDecimal(mx.Groups[1].Value) : 0;
                        //_Value = Convert.ToDecimal(mx.Groups[1].Value);
                    }

                }
            }
            catch (Exception ex)
            {

                isFailed = true;
            }
            //_client.Close();

        }

        public void CloseConnection()
        {
            _stream?.Close();
            _client?.Close();
        }


    }
}
