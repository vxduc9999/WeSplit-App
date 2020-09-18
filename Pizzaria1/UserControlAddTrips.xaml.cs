using Microsoft.Win32;
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
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlAddFood.xaml
    /// </summary>
    public partial class UserControlAddFood : UserControl
    {
        public UserControlAddFood()
        {
            InitializeComponent();
        }

        ObservableCollection<string> members = new ObservableCollection<string>();
        ObservableCollection<string> pays = new ObservableCollection<string>();
        List<string> arr = new List<string>() {
            "thanhvien",
            "khoanchi",
            "cotmoc",
            "image"
        };

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

                // nameOfMember
                TextBlock o = new TextBlock();
                TextBlock o1 = new TextBlock();
                TextBlock o2 = new TextBlock();
                string koko = mem + " : " + pr.ToString() + "đ";
                string koko1 = mem;
                string koko2 = pr.ToString() + "đ";
                o.Text = koko;
                o1.Text = koko1;
                o2.Text = koko2;
                listMembers1.Children.Add(o1);
                listMembers2.Children.Add(o2);
                members.Add(mem.Trim());
                members.Add(pr.ToString());
                member.Text = "";
                moneyOfMember.Text = "";
            }
            else
                MessageBox.Show("Chưa nhập tên thành viên!!", "", MessageBoxButton.OK);
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
                string ko1 = des;
                string ko2 = pr.ToString() + "đ/ 1 người";
                TextBlock o = new TextBlock();
                TextBlock o1 = new TextBlock();
                TextBlock o2 = new TextBlock();
                o.Text = ko.ToString();
                o1.Text = ko1;
                o2.Text = ko2;
                //listExpenditures.Children.Add(o);
                listExpenditures1.Children.Add(o1);
                listExpenditures2.Children.Add(o2);
                pays.Add(des);
                pays.Add(pr.ToString());
                description.Text = "";
                price.Text = "";
            }
        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {
            price.Text = Regex.Replace(price.Text, "[^0-9]+", "");
        }

        private void monetOfMember_TextChanged(object sender, TextChangedEventArgs e)
        {
            moneyOfMember.Text = Regex.Replace(moneyOfMember.Text, "[^0-9]+", "");
        }


        private void SaveToDB(object sender, MouseButtonEventArgs e)
        {
            if (Title.Text.Trim() != "" && ImageDescriptionOfRecipe.ImageSource != null && reviewTrip.Text.Trim() != "")
            {
                String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                string ImageBig = appStartPath;
                string ALLFile = System.IO.Path.Combine(appStartPath, "AllTrips2.txt");
                var op = File.ReadAllLines(ALLFile);
                op[0] = (Int64.Parse(op[0]) + 1).ToString();
                File.WriteAllLines(ALLFile, op);

                int temp = -1;
                string mem = "";

                appStartPath = appStartPath + "\\ListTrips";
                string path2 = System.IO.Path.Combine(appStartPath, Title.Text);

                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                    string path3 = path2 + $"\\{Title.Text}.txt";
                    if (File.Exists(path3))
                    {
                        File.Delete(path3);
                    }
                    using (StreamWriter sw = File.CreateText(path3))
                    {
                        // Lưu mô tả và hình ảnh đại diện cho món ăn
                        string imgdes = System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());
                        var imgdes2 = ((BitmapImage)ImageDescriptionOfRecipe.ImageSource).UriSource.ToString().Remove(0, 8);
                        appStartPath = String.Format(ImageBig + "\\Images\\" + imgdes);
                        File.Copy(imgdes2, appStartPath, true);


                        for (int i = 0; i < 4; i++)
                        {
                            string pathDetail = path2 + $"\\{Title.Text}{arr[i]}.txt";
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
                                                mem = content;
                                                temp = 0;
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
                                        sw1.WriteLine("");
                                    }
                                    break;
                                case 4:
                                    using (StreamWriter sw1 = File.CreateText(pathDetail))
                                    {
                                        sw1.WriteLine(imgdes);
                                        String appStartPath1 = String.Format(path2 + "\\" + imgdes);
                                        File.Copy(imgdes2, appStartPath1, true);
                                    }
                                    break;
                            }
                        }
                    }
                }

                using (StreamWriter sw = File.AppendText(ALLFile))
                {
                    sw.WriteLine(Title.Text);
                    sw.WriteLine(reviewTrip.Text);
                    sw.WriteLine(System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString()));
                    sw.WriteLine(mem);
                    sw.WriteLine("");
                    sw.WriteLine("None");

                }
                fooda.Children.Clear();
                fooda.Children.Add(new UserControlAddFood());
            }
            else
                MessageBox.Show("You did not enter the title, description or avatar image!!!");
            
        }

        private void Cancel_Click(object sender, MouseButtonEventArgs e)
        {
            UserControlEscolha _home = new UserControlEscolha();
            fooda.Children.Clear();
            fooda.Children.Add(_home);
        }
    }
}
