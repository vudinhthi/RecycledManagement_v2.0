using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class Reasons
    {
        private int reasonId;
        private string reasonName;
        private int reasonType;
        private int isActived;
        private DateTime createdDate;
        private int createdBy;

        public int ReasonId { get => reasonId; set => reasonId = value; }
        public string ReasonName { get => reasonName; set => reasonName = value; }
        public int ReasonType { get => reasonType; set => reasonType = value; }
        public int IsActived { get => isActived; set => isActived = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
        public int CreatedBy { get => createdBy; set => createdBy = value; }
    }
}
