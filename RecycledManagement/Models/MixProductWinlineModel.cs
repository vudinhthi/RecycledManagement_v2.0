using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class MixProductWinlineModel
    {
        #region tao ham dung cho model
        public MixProductWinlineModel()
        {

        }
        public MixProductWinlineModel(DataRow row,string orderAmount)
        {
            this.materialCode = row[2].ToString();
            this.materialName = row[3].ToString();
            this.quantity = row[4].ToString();
            this.actualUsage = "0";

            this.total = (Convert.ToDouble(orderAmount) * Convert.ToDouble(this.quantity)).ToString();
        }
        #endregion

        private string materialCode;
        private string materialName;
        private string quantity;
        private string total;
        private string actualUsage;

        public string MaterialCode { get => materialCode; set => materialCode = value; }
        public string MaterialName { get => materialName; set => materialName = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public string Total { get => total; set => total = value; }
        public string ActualUsage { get => actualUsage; set => actualUsage = value; }
    }
}
