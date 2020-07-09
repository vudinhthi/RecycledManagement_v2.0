using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class OtherSourceModel
    {
        private int id;
        private string sourceName;
        private bool isActived;
        private DateTime createdDate;
        private int createdBy;

        public int Id { get => id; set => id = value; }
        public string SourceName { get => sourceName; set => sourceName = value; }
        public bool IsActived { get => isActived; set => isActived = value; }
        public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
        public int CreatedBy { get => createdBy; set => createdBy = value; }
    }
}
