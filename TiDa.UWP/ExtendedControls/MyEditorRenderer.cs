using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using TiDa.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyEditor), typeof(TiDa.UWP.ExtendedControls.MyEditorRenderer))]
namespace TiDa.UWP.ExtendedControls
{
    public class MyEditorRenderer : Xamarin.Forms.Platform.UWP.EditorRenderer, MyEditText.EditTextSelectChange
    {
        public MyEditorRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            MyEditText myEditText = new MyEditText();
            myEditText.Text = Element.Text;
            myEditText.setEditTextSelectChange(this);
            myEditText.TextWrapping = TextWrapping.Wrap;
            myEditText.AcceptsReturn = true;
            myEditText.VerticalContentAlignment = VerticalAlignment.Top;
            SetNativeControl(myEditText);
        }

        private void Control_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void change(int lastPos, int curPos)
        {
            //Console.WriteLine("lastPos ===" + lastPos + "curPos ====" + curPos);
            ((MyEditor)Element).SelectionChange();
        }

        public void Pressed(int firstPos)
        {
            ((MyEditor)Element).PressedPos = firstPos;
            ((MyEditor)Element).SelectionChange();
        }

        public void Released(string selectionText)
        {
            ((MyEditor)Element).SelectionText = selectionText;

            ((MyEditor)Element).SelectionChange();

        }

        public void KeyPressed(int firstPos, string text, string selectionText)
        {
            ((MyEditor)Element).PressedPos = firstPos;
            ((MyEditor)Element).SelectionText = selectionText;
            ((MyEditor)Element).Text = text;
            ((MyEditor)Element).SelectionChange();
        }
    }
}
