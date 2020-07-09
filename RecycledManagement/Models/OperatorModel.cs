using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class OperatorModel
    {
        private int operatorId;
        private string operatorName;
        private int department;
        private bool isActived;
        private DateTime createdDate;
        private int createdBy;

        public int OperatorId { get => operatorId; set => operatorId = value; }
        public string OperatorName { get => operatorName; set => operatorName = value; }
        public int Department { get => department; set => department = value; }
        public bool IsActived { get => isActived; set => isActived = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
        public int CreatedBy { get => createdBy; set => createdBy = value; }
    }
}
