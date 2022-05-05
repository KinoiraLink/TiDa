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

        public event EventHandler SelectionChanged;

        public int PressedPos;

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
