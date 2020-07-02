using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class BookingOrderSizeModel
    {
        private string sizeName;
        private int qtyPrs;
        private double qtyKg;

        public string SizeName { get => sizeName; set => sizeName = value; }
        public int QtyPrs { get => qtyPrs; set => qtyPrs = value; }
        public double QtyKg { get => qtyKg; set => qtyKg = value; }
    }
}
