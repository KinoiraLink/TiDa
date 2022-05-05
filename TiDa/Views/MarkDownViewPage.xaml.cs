using TiDa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.Helpers;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkDownViewPage : ContentPage
    {
        public int PointterPos;
        public string SelText;
        MarkDownDetailViewModel viewModel;
        public MarkDownViewPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MarkDownDetailViewModel();
            EditorScroll.IsVisible = true;
            MdScroll.IsVisible = false;
            myEditor.Text = viewModel.TaskContent;
            if (viewModel.TaskContent == null)
            {
                myEditor.Text = "";
            }

        }


        private void Editor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MdView.Markdown = myEditor.Text;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            EditorScroll.IsVisible = !EditorScroll.IsVisible;
            MdScroll.IsVisible = !MdScroll.IsVisible;
        }

        private void Btn_Heading1(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "# ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "# ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "# ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "# ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Heading2(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "## ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "## ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "## ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "## ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Heading3(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Heading4(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "#### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "#### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "#### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "#### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Heading5(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "##### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "##### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "##### ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "##### ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Italic(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "*");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "*");
                insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "*");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "*");
                insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Bold(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "**");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 2, "**");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "**");
                insert = insert.Insert(PointterPos + 2, "**");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "**");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 2, "**");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "**");
                insert = insert.Insert(PointterPos + 2, "**");
                myEditor.Text = insert;
                return;

            }
        }


        private void Btn_ItalicBold(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "***");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 3, "***");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "***");
                insert = insert.Insert(PointterPos + 3, "***");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "***");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 3, "***");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "***");
                insert = insert.Insert(PointterPos + 3, "***");
                myEditor.Text = insert;
                return;

            }
        }


        private void Btn_Quote(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "> ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "> ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "> ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "> ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_List(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "- ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "- ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "- ");
                //insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 1, "*");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "- ");
                //insert = insert.Insert(PointterPos + 1, "*");
                myEditor.Text = insert;
                return;

            }
        }

        private void Btn_Code(object sender, EventArgs e)
        {
            string insert = null;
            insert = myEditor.Text;

            //myEditor.Text = insert;
            //insert = null;
            //myEditor.Text.Insert(PointterPos, "A");
            //string forstring = null;
            //int endPosition = 0;
            string obestring = null;
            if (myEditor.PressedPos - myEditor.SelectionText.Length < 0)
            {
                //一定是从前往后选中

                insert = insert.Insert(PointterPos, "```\n");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 4, "\n```");
                myEditor.Text = insert;
                return;
                //myEditor.Text = insert;
            }
            if (myEditor.SelectionText.Length + myEditor.PressedPos > myEditor.Text.Length)
            {
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "```\n");
                insert = insert.Insert(PointterPos + 4, "\n```");
                myEditor.Text = insert;
                return;

            }
            obestring = insert.Substring(myEditor.PressedPos, myEditor.SelectionText.Length);
            if (obestring == myEditor.SelectionText)
            {
                //从前往后
                insert = insert.Insert(PointterPos, "```\n");
                insert = insert.Insert(PointterPos + myEditor.SelectionText.Length + 4, "\n```");
                myEditor.Text = insert;
                return;
            }
            else
            {

                //从后往前
                //一定是从后往前选中
                insert = insert.Insert(PointterPos - myEditor.SelectionText.Length, "```\n");
                insert = insert.Insert(PointterPos + 4, "\n```");
                myEditor.Text = insert;
                return;

            }
        }

        private void MyEditor_OnSelectionChanged(object sender, EventArgs e)
        {
            PointterPos = (sender as MyEditor).PressedPos;
            SelText = (sender as MyEditor).SelectionText;
        }

        private void MyEditor_OnUnfocused(object sender, FocusEventArgs e)
        {
            MdView.Markdown = myEditor.Text;
        }
    }
}