using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlDetailFood.xaml
    /// </summary>
    public partial class UserControlDetailFood : UserControl
    {
        public Trip _data;
        MainWindow a = new MainWindow();
        BindingList<Trip> _list;
        string testname;
        int doRong;
        long sum = 0;
        long CountMember = 0;
        public UserControlDetailFood(Trip t)
        {
            InitializeComponent();
            _data = t;
            testname = _data.Name;
            doRong = _data.rong;

        }

        //public SeriesCollection Data => new SeriesCollection();

        Trip tranfer = new Trip()
        {
            Description = "",
            Khoanchi = new ObservableCollection<string>(),
            member = new ObservableCollection<string>(),
            image = new BindingList<string>(),
            Name = "",
            Cotmoc = "",
            Picture = "",
        };

        public SeriesCollection Data { get; set; }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;
            foreach (PieSeries pie in chart.Series)
            {
                pie.PushOut = 0;
            }
            var neo = chartPoint.SeriesView as PieSeries;
            neo.PushOut = 30;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(_data.Icon== "/Assets/list-finish.png")
            {
                _update.Visibility = Visibility.Hidden;
            }
            else
            {
                _update.Visibility = Visibility.Visible;
            }
            this.DataContext = this;

            _list = new BindingList<Trip>();
            Itemscontrol.ItemsSource = _list;

            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            appStartPath = appStartPath + $"\\ListTrips\\{testname}\\";
            var g = new Trip()
            {
                leader = "",
                Description = "",
                Khoanchi = new ObservableCollection<string>(),
                member = new ObservableCollection<string>(),
                Money = new ObservableCollection<string>(),
                Remark = new ObservableCollection<string>(),
                image = new BindingList<string>(),
                Cotmoc = "",
                Data = new SeriesCollection(),
                Name = "",
                Picture = "",
            };
            // tranfer
            g.Name = testname;
            tranfer.Name = testname;
            //image
            string fileImageTxt = appStartPath + $"{testname}image.txt";
            var readImageTest = File.ReadAllLines(fileImageTxt);
            g.Picture = appStartPath + readImageTest[0];
            tranfer.Picture = readImageTest[0];
            for (int i = 1; i < readImageTest.Length; i++)
            {
                g.image.Add(appStartPath + readImageTest[i]);
                tranfer.image.Add(readImageTest[i]);
            }

            //cotmoc
            string fileCotmoctxt = appStartPath + $"{testname}cotmoc.txt";
            var readcotmoctest = File.ReadAllLines(fileCotmoctxt);
            g.Description = readcotmoctest[0];
            g.Cotmoc = readcotmoctest[1].Replace("__", "\r\n");
            //tranfer
            tranfer.Description = readcotmoctest[0];
            tranfer.Cotmoc = readcotmoctest[1].Replace("__", "\r\n");

            //leader
            string fileleadertxt = appStartPath + $"{testname}thanhvien.txt";
            var readleadertest = File.ReadAllLines(fileleadertxt);
            g.leader = readleadertest[0].ToString();
            var nameleader = g.leader;


            //member
            string filememberctxt = appStartPath + $"{testname}thanhvien.txt";
            var readmembertest = File.ReadAllLines(filememberctxt);
            g.member.Add(readmembertest[0] + " (leader)");
            for (int i = 2; i < readmembertest.Length; i = i + 2)
            {
                g.member.Add(readmembertest[i]);
            }

            // truyền data, thêm tiền ứng, tính tổng tiền
            for (int i = 0; i < readmembertest.Length; i++)
            {
                if (i % 2 != 0)
                {
                    g.Money.Add(readmembertest[i]);
                    CountMember++;
                }
                tranfer.member.Add(readmembertest[i]);
            }

            //khoan chi
            string fileKCtxt = appStartPath + $"{testname}khoanchi.txt";
            var readKCtest = File.ReadAllLines(fileKCtxt);
            for (int i = 0; i < readKCtest.Length; i = i + 2)
            {
                string ad = readKCtest[i] + " : " + readKCtest[i + 1] + " /1 Người";
                g.Khoanchi.Add(ad);
                PieSeries Pie = new PieSeries()
                {
                    Values = new ChartValues<float> { float.Parse(readKCtest[i + 1]) },
                    Title = $"{readKCtest[i]}"
                };
                g.Data.Add(Pie);
            }
            for (int i = 0; i < readKCtest.Length; i++)
            {
                if (i % 2 != 0)
                    sum += long.Parse(readKCtest[i]) * CountMember;
                tranfer.Khoanchi.Add(readKCtest[i]);
            }

            // remark
            sum = sum / CountMember;
            for (int i = 1; i < readmembertest.Length; i += 2)
            {
                // tiền ứng
                long a = long.Parse(readmembertest[i]);
                if (a < sum)
                    g.Remark.Add($"Cần trả thêm {Math.Abs(sum - a)} cho {nameleader}");
                else if (a > sum)
                    g.Remark.Add($"Được {nameleader} trả lại {a - sum}");
                else
                    g.Remark.Add($"Đã trả đủ");
            }
            _list.Add(g);
        }

        private void _btnUpdate_Click(object sender, MouseButtonEventArgs e)
        {
            _detail.Children.Clear();
            _detail.Children.Add(new UserControlUpDate(tranfer));
        }

        private void Cancel_Click(object sender, MouseButtonEventArgs e)
        {
            if (_data.Icon == "/Assets/list-finish.png")
            {
                _detail.Children.Clear();
                _detail.Children.Add(new UserControlLikeDishes());
            }
            else
            {
                _detail.Children.Clear();
                _detail.Children.Add(new UserControlEscolha());
            }
        }
    }
}
