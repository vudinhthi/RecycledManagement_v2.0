using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class MixRecycleScaledModel
    {
        public MixRecycleScaledModel() { }
        public MixRecycleScaledModel(DataRow row)
        {
            this.RecycleScaledId = row[0].ToString();
            this.weightReScaled = row[1].ToString();
            this.mixId = row[2].ToString();
            this.createdDate = row[3].ToString();
            this.createdBy = row[4].ToString();
            this.recycleCode = row[5].ToString();
            this.recycleName = row[6].ToString();
        }

        private string recycleScaledId;
        private string weightReScaled;
        private string mixId;
        private string createdDate;
        private string createdBy;
        private string recycleCode;
        private string recycleName;

        public string RecycleScaledId { get => recycleScaledId; set => recycleScaledId = value; }
        public string WeightReScaled { get => weightReScaled; set => weightReScaled = value; }
        public string MixId { get => mixId; set => mixId = value; }
        public string CreatedDate { get => createdDate; set => createdDate = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public string RecycleCode { get => recycleCode; set => recycleCode = value; }
        public string RecycleName { get => recycleName; set => recycleName = value; }
    }
}
