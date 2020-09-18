using LiveCharts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
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
using Path = System.IO.Path;

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlUpDate.xaml
    /// </summary>
    public partial class UserControlUpDate : UserControl
    {
        Trip _data;

        public UserControlUpDate(Trip _name)
        {
            _data = _name;
            InitializeComponent();
        }

        BindingList<Trip> _list;
        ObservableCollection<string> members = new ObservableCollection<string>();
        ObservableCollection<string> pays = new ObservableCollection<string>();
        ObservableCollection<string> images = new ObservableCollection<string>();
        List<string> arr = new List<string>() {
            "thanhvien",
            "khoanchi",
            "cotmoc",
            "image",
            "leader"
        };

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            this.DataContext = this;
            _list = new BindingList<Trip>();
            // push lên UI
            Listitems.ItemsSource = _list;
            ListKhoanchi.ItemsSource = _list;
            ListImages.ItemsSource = _list;

            // path file
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            appStartPath = appStartPath + $"\\ListTrips\\{_data.Name}\\";

            // Picture
            ImageSource imgsource = new BitmapImage(new Uri(appStartPath + _data.Picture.ToString()));
            ImageDescriptionOfRecipe.ImageSource = imgsource;

            var g = new Trip()
            {
                Name = "",
                Description = "",
                leader="",
                Cotmoc = "",
                Khoanchi = new ObservableCollection<string>(),
                cotmoc = new ObservableCollection<string>(),
                member = new ObservableCollection<string>(),
                image = new BindingList<string>()
            };

            // thiết lập tên chuyến đi, review,
            g.Name = _data.Name;
            g.Description = _data.Description;
            reviewTrip.Text = g.Description;
            Title.Text = g.Name;
            Cotmoc.Text = _data.Cotmoc;

            //image
            foreach (string itemm in _data.image)
            {
                images.Add(appStartPath + itemm);
                g.image.Add(appStartPath + itemm);
            }

            //member

            g.leader = _data.member[0].ToString() + " (leader):  " + _data.member[1].ToString() + "đ";
            g.member.Add(g.leader);
            for (int i = 2; i < _data.member.Count; i = i + 2)
            {
                g.member.Add(_data.member[i] + ": " + _data.member[i + 1] + "đ");
            }

            for (int i = 0; i < _data.member.Count; i++)
            {
                members.Add(_data.member[i]);

            }

            //khoan chi
            for (int i = 0; i < _data.Khoanchi.Count; i += 2)
            {
                g.Khoanchi.Add(_data.Khoanchi[i] + " : " + _data.Khoanchi[i + 1] + "đ/ 1 người");
            }

            for (int i = 0; i < _data.Khoanchi.Count; i++)
            {
                pays.Add(_data.Khoanchi[i]);
            }
            _list.Add(g);
        }


        private void monetOfMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyOfMember.Text = Regex.Replace(moneyOfMember.Text, "[^0-9]+", "");
        }

        private void ChooseImages(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
                ImageDescriptionOfRecipe.ImageSource = imgsource;
            }
        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {
            price.Text = Regex.Replace(price.Text, "[^0-9]+", "");
        }

        private void moneyOfMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyOfMember.Text = Regex.Replace(moneyOfMember.Text, "[^0-9]+", "");
        }

        //  thêm member
        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            if (member.Text.Trim() != "")
            {
                string mem = member.Text;
                // advance payment
                string pricee = moneyOfMember.Text;
                ulong pr;
                if (pricee == "")
                {
                    pr = 0;
                }
                else
                    pr = Convert.ToUInt64(pricee);
                string koko = mem + ": " + pr.ToString() + "đ";
                //_list[0].leader.Add(koko1);
                _list[0].member.Add(koko);
                members.Add(mem.Trim());
                members.Add(pr.ToString());
                member.Text = "";
                moneyOfMember.Text = "";
            }
            else
                MessageBox.Show("Chưa nhập tên thành viên!!", "", MessageBoxButton.OK);
        }

        // thêm khoản chi
        private void addPayment_Click(object sender, RoutedEventArgs e)
        {
            if (description.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên khoảng chi!!", "", MessageBoxButton.OK);
            }
            else
            {
                string des = description.Text;
                string pricee = price.Text;
                ulong pr;
                if (price.Text == "")
                {
                    pr = 0;
                }
                else
                    pr = Convert.ToUInt64(pricee);

                string ko = des + " : " + pr.ToString() + "đ/ 1 người";
                _list[0].Khoanchi.Add(ko);
                pays.Add(des);
                pays.Add(pr.ToString());
                description.Text = "";
                price.Text = "";
            }
        }

        // button xóa member
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _list[0].member.IndexOf(item.ToString());
            if(index==0)
            {
                MessageBox.Show("Đây là leader, không thể xoá!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                members.RemoveAt(index * 2);
                members.RemoveAt(index * 2);
                _list[0].member.RemoveAt(index);
            }
        }

        // button xóa khoản chi
        private void Button_ClickPay(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _list[0].Khoanchi.IndexOf(item.ToString());
            pays.RemoveAt(index * 2);
            pays.RemoveAt(index * 2);
            _list[0].Khoanchi.RemoveAt(index);
        }

        // button xóa image
        private void DeleteImg(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = _list[0].image.IndexOf(item.ToString());
            images.RemoveAt(index);
            _list[0].image.RemoveAt(index);
        }

        // thêm ảnh
        private void AddImages_Click(object sender, RoutedEventArgs e)
        {
            if (_list[0].image.Count > 10)
            {
                MessageBox.Show("Ảnh không được quá 10");
            }
            else
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = true;
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    if (_list[0].image.Count + open.FileNames.Length <= 10)
                    {
                        foreach (string item in open.FileNames)
                        {
                            _list[0].image.Add(item);
                            images.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ảnh không được quá 10");
                    }
                }
            }
        }

        // lưu vào folder
        private void SaveToDB(object sender, RoutedEventArgs e)
        {
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string ImageBig = appStartPath;
            appStartPath = appStartPath + "\\ListTrips";
            string path2 = System.IO.Path.Combine(appStartPath, _data.Name);

            string mem = "";
            int temp = -1;

            // lấy tên và đường dẫn ảnh đại diện
            string imgdes = System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());
            var imgdes2 = ((BitmapImage)ImageDescriptionOfRecipe.ImageSource).UriSource.ToString().Remove(0, 8);


            for (int i = 0; i < 4; i++)
            {
                string pathDetail = path2 + $"\\{Title.Text}{arr[i]}.txt";
                if (File.Exists(pathDetail))
                {
                    File.Delete(pathDetail);
                }
                switch (i + 1)
                {
                    case 1:
                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                        {
                            foreach (string content in members)
                            {
                                sw1.WriteLine(content);
                                if (temp == -1)
                                {
                                    temp = 0;
                                    mem = content;
                                }
                                else
                                    mem = mem + " " + content;
                            }
                        }
                        break;
                    case 2:
                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                        {
                            foreach (string content in pays)
                            {
                                sw1.WriteLine(content);
                            }
                        }
                        break;
                    case 3:
                        using (StreamWriter sw1 = File.CreateText(pathDetail))
                        {
                            sw1.WriteLine(reviewTrip.Text);
                            sw1.WriteLine(Cotmoc.Text.Replace("\r\n", "__"));
                        }
                        break;
                    case 4:
                        using (StreamWriter sw = File.CreateText(pathDetail))
                        {
                            // lưu file ảnh đai jdiện
                            sw.WriteLine(imgdes);
                            String appStartPath1 = String.Format(path2 + "\\" + imgdes);
                            if (!File.Exists(appStartPath1))
                            {
                                File.Copy(imgdes2, appStartPath1, true);
                            }

                            // lưu vào file nhỏ
                            foreach (string content in images)
                            {
                                string nameCheck = System.IO.Path.GetFileName(content);
                                string lp = path2 + "\\" + nameCheck;
                                if (!File.Exists(path2 + "\\" + nameCheck))
                                {
                                    string name = System.IO.Path.GetFileName(content);
                                    string kop = content.Replace($"\\{name}", "");
                                    sw.WriteLine(name);
                                    appStartPath = String.Format(path2 + "\\" + name);
                                    File.Copy(content, appStartPath, true);
                                }
                                else
                                    sw.WriteLine(System.IO.Path.GetFileName(content));
                            }
                        }
                        break;
                }
            }

            // sửa file alltrip
            String appStartPath2 = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string ALLFile = System.IO.Path.Combine(appStartPath2, "AllTrips2.txt");
            var op = File.ReadAllLines(ALLFile);
            for (int i = 1; i < op.Length; i += 6)
            {
                if (op[i] == _data.Name)
                {
                    op[i + 1] = reviewTrip.Text;
                    op[i + 2] = imgdes;
                    op[i + 3] = mem;
                    op[i + 4] = Cotmoc.Text.Replace("\r\n", "__");
                }
            }
            File.WriteAllLines(ALLFile, op);
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
