using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class GlobalEvent
    {
        private int _scaleValue;

        public int ScaleValue 
        { 
            get => _scaleValue;
            set { _scaleValue = value; OnScaleValueChanged(value); }
        }

        private event EventHandler<ScaleValueChangedEventArgs> _scaleValueChanged;
        public event EventHandler<ScaleValueChangedEventArgs> ScaleValueChanged
        {
            add
            {
                _scaleValueChanged += value;
            }
            remove
            {
                _scaleValueChanged -= value;
            }
        }

        void OnScaleValueChanged(int value)
        {
            _scaleValueChanged?.Invoke(this, new ScaleValueChangedEventArgs(value));
        }
    }

    public class ScaleValueChangedEventArgs
    {
        private int _scaleValue;

        public int ScaleValue { get => _scaleValue; set => _scaleValue = value; }

        public ScaleValueChangedEventArgs(int value)
        {
            _scaleValue = value;
        }
    }
}
