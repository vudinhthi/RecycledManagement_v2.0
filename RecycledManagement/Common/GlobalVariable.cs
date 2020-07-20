using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public static class GlobalVariable
    {
        //các thông tin được truyền vào từ trang Login
        public static int role = 0;
        public static int userId;
        public static DateTime loginDate;

        #region tao các biến để set trang thái enable trong các editForm
        public static bool newOrUpdateOrderBook = false, enableFlagOrderBook = false;//true--New-->hiện nút save trên editForm; false--update-->ẩn nút save trên editForm
                                                                                     //mỗi lần click vào row trên gridView thì sẽ xét xem đó là dòng mới hay dòng cũ, để gán true or false cho biến này để hide/show nút Save trên EditForm
        public static bool newOrUpdateCrushed = false, enableFlagCrushed = false;//true--New-->hiện nút save trên editForm; false--update-->ẩn nút save trên editForm
        public static bool newOrUpdateIncoming = false, enableFlagIncoming = false;//true--New-->hiện nút save trên editForm; false--update-->ẩn nút save trên editForm
        public static bool newOrUpdateMix = false, enableFlagMix = false;//true--New-->hiện nút save trên editForm; false--update-->ẩn nút save trên editForm
        #endregion

        public static int idSelect;//dùng để lưu lại rowSelect khi chọn trên GridView để khi bấm printLables in cho đúng record
        public static string selectLabel = "";//biến dùng để chọn loại tem cần in
        public static int orderId;//dung cho Station Mixing
        public static string mixId;
        public static string selectScale;//biến để chọn cân. "ScaleColor"--> cân màu; "ScalePlastic"--> cân nhựa

        //biến check quyền cho user
        public static bool importOrder = false, print = false, scales = false;
        public static bool importMixing = false;
        public static bool importIncoming = false;
        public static bool importCrush = false;


        //Biến lưu các giá trị trừ bì
        public static double boxWeightIncoming = 2.1966,boxWeightCrushing = 1.14, boxWeightMixingMaterial = 0.16, boxWeightMixingRecycle = 1.14;

        public static GlobalEvent myEvent = new GlobalEvent();//tạo đối tượng để nhận event tag scaleValueChanged
        public static bool isMixcode = false;
    }
}
