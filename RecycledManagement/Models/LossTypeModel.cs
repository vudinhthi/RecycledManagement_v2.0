using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class LossTypeModel
    {
        private int lossTypeId;
        private string lossTyoeName;
        private bool isActive;
        private bool lossTypeForm;
        private DateTime createDate;
        private int createBy;

        public int LossTypeId { get => lossTypeId; set => lossTypeId = value; }
        public string LossTyoeName { get => lossTyoeName; set => lossTyoeName = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool LossTypeForm { get => lossTypeForm; set => lossTypeForm = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int CreateBy { get => createBy; set => createBy = value; }
    }
}
