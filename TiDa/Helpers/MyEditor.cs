using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TiDa.Helpers
{
    public class MyEditor : Editor
    {
        public static readonly BindableProperty SelectionChangedProperty =
            BindableProperty.Create("SelectionChanged", typeof(EventHandler), typeof(MyEditor), null);
        //当光标移动时触发该事件
        public event EventHandler SelectionChanged;
        //光标当前位置
        public int PressedPos;
        //光标选中的文字
        public string SelectionText;

        public void SelectionChange()
        {
            EventHandler eventHandler = this.SelectionChanged;
            SelectionEventArgs selectionEventArgs = new SelectionEventArgs() { };
            eventHandler?.Invoke((object)this, selectionEventArgs);
        }

        public class SelectionEventArgs : EventArgs
        {
            public int lastPos { get; set; }
            public int curPos { get; set; }
        }
    }
}
