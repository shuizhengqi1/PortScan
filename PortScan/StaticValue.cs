using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortScan
{
    
    class StaticValue
    {
        public static List<int> _portList = new List<int> { 80, 443, 8080, 2222 };


        public static void AddPort(int port)
        {
            _portList.Add(port);
        }
        public static List<int> getPort()
        {
            return _portList;
        }
        public static void DelPort(int port)
        {
            _portList.Remove(port);
        }
        public static int checkPort(int port)
        {
            return _portList.IndexOf(port);
        }
    }
}
