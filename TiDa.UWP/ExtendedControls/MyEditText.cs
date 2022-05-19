using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Xamarin.Forms.Platform.UWP;

namespace TiDa.UWP.ExtendedControls
{
    public class MyEditText : FormsTextBox
    {
        private int mLastPos = 0;
        private int mCurPos = 0;
        private int mFirstPos = 0;
        private string mSelectionText;
        private string mText;

        private EditTextSelectChange editTextSelectChange;

        public MyEditText()
        {

        }
        
        public void setEditTextSelectChange(EditTextSelectChange editTextSelectChange)
        {
            this.editTextSelectChange = editTextSelectChange;
        }

        //当鼠标移动时检测光标在文本中的位置
        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            base.OnPointerMoved(e);
            if (this.editTextSelectChange != null)
            {
                mCurPos = SelectionStart + SelectionLength;
                editTextSelectChange.change(mLastPos, mCurPos);
                mLastPos = mCurPos;
            }
        }
        //当鼠标按下时检测光标在文本中的位置
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            mFirstPos = SelectionStart;
            editTextSelectChange.Pressed(mFirstPos);
        }
        //当鼠标抬起时检测光标在文本中的位置
        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            mFirstPos = SelectionStart;
            mSelectionText = SelectedText;
            editTextSelectChange.Released(mSelectionText);
        }
        //当按键弹起之前检测光标在文本中的位置
        protected override void OnPreviewKeyUp(KeyRoutedEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            mSelectionText = SelectedText;
            mFirstPos = SelectionStart;
            mText = Text;
            editTextSelectChange.KeyPressed(mFirstPos, mText, mSelectionText);

        }
        //定义一个文字选择改变的接口
        public interface EditTextSelectChange
        {
            void change(int lastPos, int curPos);
            void Pressed(int firstPos);
            void Released(string selectionText);

            void KeyPressed(int firstPos, string text, string selectionTex);
        }
    }
}
