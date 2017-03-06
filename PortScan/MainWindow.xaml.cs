using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;
using System.Data;
using System.Net;
using System.Net.Sockets;
namespace PortScan
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        IPAddress ip = null;
        Boolean flagOfdomain = false;
        Boolean domainError = false;
        /// <summary>
        /// 编辑窗口打开的标志
        /// </summary>
        public static Boolean flagOfNewDialog = true;
        Thread ConnetTest = null;
        EditPort ed = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 文本框检测
        /// <summary>
        /// by杨恒星
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkIP();
        }
        #endregion


        #region 开始连接
        /// <summary>
        /// by杨恒星
        /// </summary>
        private void ConnectTest(Object port1)
        {
            IPEndPoint endpoint = null;
            int port = (int)port1;
            if (flagOfdomain)
            {
                try
                {
                    String domain = IPInput.Text.Replace("http://", "");
                    IPHostEntry hostEntry = Dns.GetHostEntry(domain);
                    endpoint = new IPEndPoint(hostEntry.AddressList[0], port);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("无法解析域名，请确认网络连接正常");
                    domainError = true;
                    
                }
            }else{
                endpoint = new IPEndPoint(this.ip,port);
            }
            
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(endpoint);
                SetResult(port, 1);
            }
            catch (Exception ex)
            {
                SetResult(port, 2);

            }

        }
        #endregion
        #region 点击连接按钮
        /// <summary>
        /// by杨恒星
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            domainError = false;
            ResultList.Items.Refresh();
            Console.Write("==    开始连接测试     ==");
            if (IPInput.Text != ""&&checkIP())
            {
                for (int i = 0; i < StaticValue._portList.Count; i++)
                {
                    
                    int port = int.Parse(StaticValue._portList[i].ToString());
                    if (flagOfdomain)
                    {
                        
                    }
                    else
                    {
                        this.ip = IPAddress.Parse(IPInput.Text.ToString());
                    }
                    
                  //  ConnetTest = new Thread(new ParameterizedThreadStart(ConnectTest));

                   // ConnetTest.Start(port);
                    if (!domainError)
                    {
                        ConnectTest(port);
                    }
                         
                }
            }
            else
            {
                MessageBox.Show("请输入正确的ip地址之后再进行测试");
            }
            
           //
        }
        #endregion
        private void SetResult(int port, int flag)
        {
            String date = DateTime.Now.ToString();
            switch(flag){
                case 1:
                   
                    
                    ResultList.Items.Add(date+"   "+port + "的测试结果为   打开");
                    Console.Write("-------------------");
                    Console.Write(port + "的测试结果为   打开 \n");
                    break;
                case 2:
                    ResultList.Items.Add(date+"   "+port + "的测试结果为   关闭");
                     Console.Write("-------------------");
                    Console.Write(port + "的测试结果为   关闭 \n");
                    break;

            }

        }

        #region 打开编辑端口窗口   
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

            if (flagOfNewDialog)
            {
                ed = new EditPort();
                ed.Show();
                flagOfNewDialog = false;
            }
            else
            {
                ed.Focus();
            }
            
           
        }
        #endregion
        #region 判断ip格式是否正确
        /// <summary>
        /// by杨恒星
        /// </summary>
        /// <returns></returns>
        private Boolean checkIP()
        {
            Boolean check = false;
            String ipinput = IPInput.Text;
            if (ipinput.Contains("."))
            {
                String[] lines = new String[4];
                string s = ".";
                lines = ipinput.Split(s.ToCharArray(), 4);
                if (lines.Count().Equals(4))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Regex regex = new Regex(@"^\d+$");
                        if (regex.IsMatch(lines[i]))
                        {
                            if (Convert.ToInt32(lines[i]) >= 255)
                            {
                                ConfirmBtn.IsEnabled = true;
                                IPCheckLabel.Content = "输入ip地址合法";
                                flagOfdomain = true;
                                IPCheckLabel.Background = new SolidColorBrush(Colors.Green);
                                check = true;
                            }
                            else
                            {

                                ConfirmBtn.IsEnabled = true;
                                IPCheckLabel.Content = "输入ip地址合法";
                                IPCheckLabel.Background = new SolidColorBrush(Colors.Green);
                                check = true;
                                flagOfdomain = false;
                            }
                        }
                        else
                        {
                            Regex regex1 = new Regex(@"^[a-zA-Z0-9]");
                            if (regex1.IsMatch(lines[i]))
                            {
                                ConfirmBtn.IsEnabled = true;
                                IPCheckLabel.Content = "输入ip地址合法";
                                flagOfdomain = true;
                                IPCheckLabel.Background = new SolidColorBrush(Colors.Green);
                                check = true;
                            }
                            else
                            {
                                ConfirmBtn.IsEnabled = false;
                                IPCheckLabel.Content = "输入ip地址含有非法字符，请检查";
                                flagOfdomain = false;
                                IPCheckLabel.Background = new SolidColorBrush(Colors.Yellow);
                                check = false;
                            }
                            
                        }
                    }
                }
                else
                {
                    Regex regex = new Regex(@"^[a-zA-z0-9]*$");
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (regex.IsMatch(lines[i]))
                        {
                            ConfirmBtn.IsEnabled = true;
                            IPCheckLabel.Content = "输入ip地址合法";
                            flagOfdomain = true;
                            IPCheckLabel.Background = new SolidColorBrush(Colors.Green);
                            check = true;
                        }
                        else
                        {
                            ConfirmBtn.IsEnabled = false;
                            IPCheckLabel.Content = "输入ip地址不合法，请重新输入";
                            IPCheckLabel.Background = new SolidColorBrush(Colors.Red);
                            check = false;
                        }
                    }
                }
            }
            return check;
        }
        #endregion
        #region 关闭主窗口时检查子窗口是否关闭
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (!flagOfNewDialog)
            {
                ed.Close();
            }
            this.Close();
        }
        #endregion
    }
        
}
