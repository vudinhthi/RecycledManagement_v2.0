using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class UserConfigModel
    {
        public string ToMailAddress { get; set; }
        public string CCMailAddress { get; set; }
        public string Mixing_Material_BoxWeight { get; set; }
        public string Mixing_Recycle_BoxWeight { get; set; }
        public string Incoming_BoxWeight { get; set; }
        public string Crushing_BoxWeight{ get; set; }

    }
}
