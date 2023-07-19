using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace multi_thread.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SynchronizationContext _synchronizationContext;
        public MainWindow()
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Work();//耗时操作，界面无响应
            //new Thread(Work).Start();//InvalidOperationException 无法访问tb对象，因为另一个线程拥有该对象
            //new Thread(UpdateMessage).Start();
            new Thread(()=>UpdateMessage("Work")).Start();
        }

        void Work()
        { 
            Thread.Sleep(5000);
            tb.Text = "Work";
        }

        void UpdateMessage()
        {
            Thread.Sleep(5000);
            Action action = () =>
            {
                tb.Text = "Work";
            };
            Dispatcher.BeginInvoke(action);
        }

        void UpdateMessage(string message)
        {
            Thread.Sleep(5000);
            //把委托Mashal 给UI线程
            //调用Post相当于调用Dispatch或Control上的BeginInvoke方法
            _synchronizationContext.Post(_=>tb.Text = message,null);
        }

    }
}
