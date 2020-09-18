using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pizzaria1
{
    public class Trip
    {

        public string address1 { get; set; }
        public string Name { get; set; }
        public string Cotmoc { get; set; }
        public string Introduce { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public BindingList<string> Images { get; set; }
        public string Icon { get; set; }
        public string leader { get; set; }
        public ObservableCollection<string> member { get; set; }
        public ObservableCollection<string> cotmoc { get; set; }
        public ObservableCollection<string> Khoanchi { get; set; }
        public ObservableCollection<string> Money { get; set; }
        public ObservableCollection<string> Remark { get; set; }
        public BindingList<string> image { get; set; }
        public int rong { get; set; }
        public SeriesCollection Data { get; set; }
        public string Member { get; set; }
        public BindingList<string> Imagess { get; set; }
    }
}
