using System;
using System.Collections.Generic;
using System.Text;
using TiDa.Models;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class HtmlEditorViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
                LoadMarkdown(value.ToString());
            }
        }
        private string _content;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private HtmlWebViewSource _editorSource;

        public HtmlWebViewSource EditorSource
        {
            get => _editorSource;
            set => SetProperty(ref _editorSource, value);
        }

        public HtmlEditorViewModel()
        {
            EditorSource = new HtmlWebViewSource();
            EditorSource.Html = $@"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <link href=""https://cdn.quilljs.com/1.3.6/quill.snow.css"" rel=""stylesheet"">
    <title>Document</title>
</head>

<body>
    <div id=""editor"">
        请获取文本
    </div>
    <script src=""https://cdn.quilljs.com/1.3.6/quill.js""></script>

    <!-- Initialize Quill editor -->
    <script>
        var toolbarOptions = [
            ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
            ['blockquote', 'code-block'],

            [{{ 'header': 1 }}, {{ 'header': 2 }}],               // custom button values
            [{{ 'list': 'ordered' }}, {{ 'list': 'bullet' }}],
            [{{ 'script': 'sub' }}, {{ 'script': 'super' }}],      // superscript/subscript
            [{{ 'indent': '-1' }}, {{ 'indent': '+1' }}],          // outdent/indent
            [{{ 'direction': 'rtl' }}],                         // text direction

            [{{ 'size': ['small', false, 'large', 'huge'] }}],  // custom dropdown
            [{{ 'header': [1, 2, 3, 4, 5, 6, false] }}],

            [{{ 'color': [] }}, {{ 'background': [] }}],          // dropdown with defaults from theme
            [{{ 'font': [] }}],
            [{{ 'align': [] }}],

            ['clean']                                         // remove formatting button
        ];
        var quill = new Quill('#editor', {{
            modules: {{
                toolbar: toolbarOptions
            }},
            theme: 'snow'
        }});
        
function contents(mark)
{{
    quill.setContents(mark);
    return mark;
}}
function factorial() {{
        let content = quill.container.firstChild.innerHTML;
        return content;
}}
    </script>
</body>

</html>";
        }


        public async void LoadMarkdown(string Id)
        {
            try
            {
                var markDownTask = await MarkDownDataStore.GetItemAsync(Id);
                Content = markDownTask.TaskContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public string GetContent()
        {
            return Content;
        }

        public async void SetContent(string s)
        {
            await MarkDownDataStore.InsertorReplace(new MarkDownTask
            {
                Id = Id,
                TaskContent = s,
                Timestamp = DateTime.Now.Ticks
            });
        }

    }
    


}

