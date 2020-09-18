using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlLikeDishes.xaml
    /// </summary>
    public partial class UserControlLikeDishes : UserControl
    {
        public UserControlLikeDishes()
        {
            InitializeComponent();
        }
        ObservableCollection<Trip> _data;
        public Trip FinishTrips { get; set; } = null;
        public Trip NewTrip { get; set; } = null;
        List<int> save = new List<int>();

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};

            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}Finish.txt";
            var lines = File.ReadAllLines(database);
            int count = int.Parse(lines[0]);
            _data = new ObservableCollection<Trip>();
            for (int i = 0; i < count; i++)
            {
                var line1 = lines[i * 6 + 1];
                var line2 = lines[i * 6 + 2];
                var line3 = lines[i * 6 + 3];
                var line4 = lines[i * 6 + 4];
                var line5 = lines[i * 6 + 5];
                var line6 = lines[i * 6 + 6];

                var trips = new Trip()
                {
                    Name = line1,
                    Introduce = line2,
                    Picture = line3,
                    Member = line4,
                    address1 = line5,
                    Icon = line6
                };
                if (trips.Icon == "/Assets/list-finish.png")
                {
                    save.Add(i);
                    _data.Add(trips);
                }
            }

            this.Bot.Visibility = Visibility.Hidden;
            if (_data.Count > 3)
            {
                this.Bot.Visibility = Visibility.Visible;
            }

            info.CurrentPage = 1;
            info.RowsPerPage = 3;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);

            Thread thread = new Thread(delegate ()
            {
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    dataListView.ItemsSource = _data.Take(info.RowsPerPage);
                });
            });

            thread.Start();
        }


        PagingInfo info = new PagingInfo();
        class PagingInfo : INotifyPropertyChanged
        {
            public int TotalPages { get; set; }
            private int _currentPage = 1;
            public int CurrentPage
            {
                get => _currentPage;
                set
                {
                    _currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }

            public int Count { get; set; }
            public int RowsPerPage { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage <= info.TotalPages)
            {
                info.CurrentPage--;
                dataListView.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
                if (info.CurrentPage <= 1)
                {
                    info.CurrentPage = 1;
                }
            }
        }
        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            info.RowsPerPage = 3;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);
            dataListView.ItemsSource =
            _data
                .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                .Take(info.RowsPerPage);
            pagingInfoStackPanel.DataContext = info;
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage < info.TotalPages)
            {
                info.CurrentPage++;
                dataListView.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
            }
        }
        private void dataListview_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as Trip;
            if (item != null)
            {
                _escolha.Children.Clear();
                _escolha.Children.Add(new UserControlDetailFood(item));
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            //Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (SearchTexbox.Text.Length == 0)
            {
                dataListView.ItemsSource = _data.Take(3);
            }
            else
            {// Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
                ObservableCollection<Trip> searchTrips = new ObservableCollection<Trip>();
                for (int i = 0; i < _data.Count; i++)
                {
                    if (RemoveUnicode(_data[i].Name).ToLower().Contains(RemoveUnicode(SearchTexbox.Text).ToLower())
                    || RemoveUnicode(_data[i].Name).ToUpper().Contains(RemoveUnicode(SearchTexbox.Text).ToUpper())
                    || RemoveUnicode(_data[i].Member).ToUpper().Contains(RemoveUnicode(SearchTexbox.Text).ToUpper())
                    || RemoveUnicode(_data[i].Member).ToLower().Contains(RemoveUnicode(SearchTexbox.Text).ToLower())
                    || RemoveUnicode(_data[i].address1).ToUpper().Contains(RemoveUnicode(SearchTexbox.Text).ToUpper())
                    || RemoveUnicode(_data[i].address1).ToLower().Contains(RemoveUnicode(SearchTexbox.Text).ToLower())) // Nếu tìm thấy tên phù hợp
                    {
                        searchTrips.Add(_data[i]); // Thì thêm vào danh sách mới
                    }
                }
                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchTrips.Count > 0 && searchTrips.Count <= 3)
                {
                    this.Bot.Visibility = Visibility.Collapsed;
                    dataListView.ItemsSource = searchTrips.Take(info.RowsPerPage);
                }
                else if (searchTrips.Count > 3)
                {
                    this.Bot.Visibility = Visibility.Visible;
                    dataListView.ItemsSource = searchTrips.Take(info.RowsPerPage);
                }
                else
                {
                    MessageBox.Show("Not trip exists!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Làm trống ô tìm kiếm
                SearchTexbox.Text = "";
            }
        }

        private void flag_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListView.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            if (_data[index].Icon == "None")
            {
                Thread thread = new Thread(delegate ()
                {
                    Dispatcher.Invoke(() =>
                    {
                        dataListView.ItemsSource = _data.Take(info.RowsPerPage);
                    });

                });
                thread.Start();
                _data[index].Icon = "/Assets/list-finish.png";
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}Finish.txt";
                var lines = File.ReadAllLines(database);
                lines[save[index] * 6 + 6] = "/Assets/list-finish.png";
                File.WriteAllLines(database, lines);
                //Xu ly xoa
                if (index == -1)
                {

                }
                else
                {
                    var db = $"{folder}AllTrips2.txt";
                    List<string> resline = File.ReadAllLines(db).ToList();
                    var line = File.ReadAllLines(db);
                    int count = int.Parse(line[0]);
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (resline[i * 6 + 1] == _data[index].Name)
                            {
                                count--;
                                resline[0] = (count--).ToString();
                                resline.RemoveAt(i * 6 + 6);
                                resline.RemoveAt(i * 6 + 5);
                                resline.RemoveAt(i * 6 + 4);
                                resline.RemoveAt(i * 6 + 3);
                                resline.RemoveAt(i * 6 + 2);
                                resline.RemoveAt(i * 6 + 1);
                                File.WriteAllLines(db, resline.ToArray());
                            }
                        }
                    }
                }
            }
            else
            {
                Thread thread = new Thread(delegate ()
                {
                    // Cập nhật UI
                    Dispatcher.Invoke(() =>
                    {
                        dataListView.ItemsSource = _data.Take(info.RowsPerPage);
                    });
                });
                thread.Start();
                _data[index].Icon = "None";
                FinishTrips = _data[index];
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var db = $"{folder}Finish.txt";
                var lines = File.ReadAllLines(db);
                lines[save[index] * 6 + 6] = "None";
                File.WriteAllLines(db, lines);
                var database = $"{folder}AllTrips2.txt";
                var flines = File.ReadAllLines(database);
                int count = int.Parse(flines[0]);
                count++;
                flines[0] = (count++).ToString();
                File.WriteAllLines(database, flines);
                using (StreamWriter sw = File.AppendText(database))
                {
                    sw.WriteLine(FinishTrips.Name);
                    sw.WriteLine(FinishTrips.Introduce);
                    sw.WriteLine(FinishTrips.Picture);
                    sw.WriteLine(FinishTrips.Member);
                    sw.WriteLine(FinishTrips.address1);
                    sw.WriteLine(FinishTrips.Icon);
                }
            }
        }
    }
}
