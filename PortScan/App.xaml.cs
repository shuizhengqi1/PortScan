using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PortScan
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public List<String> _portList = new List<string> { "80", "443", "8080", "2222" };


        public void AddPort(String port)
        {
            this._portList.Add(port);
        }
    }
}
