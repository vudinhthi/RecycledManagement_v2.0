using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    class GlobalEvent
    {
        private int scaleValue;

        public int ScaleValue
        {
            get => scaleValue;
            set
            {
                scaleValue = value;
                OnScaleValueChanged(value);//gọi sự kiện
            }
        }

        #region tạo event cho sự kiện tag scaleValue
        private event EventHandler<ScaleValueChangedEventArgs> scaleValueChanged;
        public event EventHandler<ScaleValueChangedEventArgs> ScaleValueChanged
        {
            add
            {
                scaleValueChanged += value;
            }
            remove
            {
                scaleValueChanged -= value;
            }
        }

        //tạo method moi khi muốn gọi sự kiện scaleValueChanged thì gọi method này
        void OnScaleValueChanged(int value)
        {
            //if (scaleValueChanged != null)
            {
                scaleValueChanged?.Invoke(this, new ScaleValueChangedEventArgs(value));
            }
        }
        #endregion


    }

    //private void event_ScaleValueChanged(object sender, NameChangedEventArgs e)Ơ }
    //tao lass chua cac du lieu tra ve owr doi so EventArgs
    class ScaleValueChangedEventArgs : EventArgs
    {
        private int scaleValue;

        public int ScaleValue { get => scaleValue; set => scaleValue = value; }

        public ScaleValueChangedEventArgs(int value)
        {
            scaleValue = value;
        }
    }
}
