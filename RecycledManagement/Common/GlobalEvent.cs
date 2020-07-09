using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class GlobalEvent
    {
        #region Tạo sự kiện cho cân
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
        #endregion

        #region Tạo sự kiện cho việc show MixingEditor
        private bool _showMixingEditor;
        public bool ShowMixingEditor { 
            get => _showMixingEditor;
            set { _showMixingEditor = value; OnShowMixingEditorChanged(value); } 
        }

        private event EventHandler<ScaleValueChangedEventArgs> _showMixingEditorChanged;
        public event EventHandler<ScaleValueChangedEventArgs> ShowMixingEditorChanged
        {
            add
            {
                _showMixingEditorChanged += value;
            }
            remove
            {
                _showMixingEditorChanged -= value;
            }
        }

        void OnShowMixingEditorChanged(bool value)
        {
            _showMixingEditorChanged?.Invoke(this, new ScaleValueChangedEventArgs(value));
        }
        #endregion
    }


    //Class define các properties trả về
    public class ScaleValueChangedEventArgs
    {
        private int _scaleValue;

        private bool _showMixingEditor;

        public int ScaleValue { get => _scaleValue; set => _scaleValue = value; }
        public bool ShowMixingEditor { get => _showMixingEditor; set => _showMixingEditor = value; }

        public ScaleValueChangedEventArgs(int value)
        {
            _scaleValue = value;
        }

        public ScaleValueChangedEventArgs(bool value)
        {
            _showMixingEditor = value;
        }
    }
}
