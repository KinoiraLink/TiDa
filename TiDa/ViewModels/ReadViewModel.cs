using System;
using System.Collections.Generic;
using System.Text;

namespace TiDa.ViewModels
{
    internal class ReadViewModel : BaseViewModel
    {
        private string _markDownText;

        public string MarkDownText
        {
            get => _markDownText;
            set => SetProperty(ref _markDownText, value);
        }

        
    }
}
