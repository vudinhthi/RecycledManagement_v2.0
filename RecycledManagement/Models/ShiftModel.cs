using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    //calss mo ta cac truong cau bang shift
    public class ShiftModel
    {
        private int id;
        private string shiftName;
        private bool isActived;
        private DateTime createDate;
        private int createBy;//foreign Key noi cot ID bang Account

        public int Id { get => id; set => id = value; }
        public string ShiftName { get => shiftName; set => shiftName = value; }
        public bool IsActived { get => isActived; set => isActived = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int CreateBy { get => createBy; set => createBy = value; }
    }
}
