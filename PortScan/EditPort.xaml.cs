using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PortScan
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class EditPort : Window
    {
        
        
        public EditPort()
        {
            InitializeComponent();
            EditPort_load();
        }

        public void EditPort_load()
        {
            this.relList.Items.Clear();
            for (int i = 0; i < StaticValue._portList.Count; i++)
            {
                this.relList.Items.Add(StaticValue._portList[i]);
            }

                for (int port = 0; port < relList.Items.Count; port++)
                {
                    Console.Write(relList.Items[port] + "\n");
                }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"\D");
            
            if(addPort.Text!=""){
                if (!regex.IsMatch(addPort.Text))
                {
                    int newport = int.Parse(addPort.Text);
                    if (StaticValue.checkPort(newport) > 0)
                    {
                        MessageBox.Show("对不起，该端口号已经存在，请重新输入");
                    }
                    else
                    {
                        StaticValue.AddPort(newport);
                        EditPort_load();
                    }

                }
                else
                {
                    MessageBox.Show("请输入正确的数字");
                }
            }
            else
            {
                MessageBox.Show("请输入正确的端口号，然后再添加");
            }
            
            
            
           
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (relList.SelectedIndex > 0)
            {
                int port = int.Parse(relList.SelectedValue.ToString());
                StaticValue.DelPort(port);
                EditPort_load();
            }
            else
            {
                MessageBox.Show("请选中要删除的内容之后在点击删除");
            }

        }

        private void confirmBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.flagOfNewDialog = true;
        }

        private void Window_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {

        }
    }
}
