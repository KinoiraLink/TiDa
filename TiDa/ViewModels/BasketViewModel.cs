using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TiDa.Models;

namespace TiDa.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        public ObservableCollection<MarkDownTask> MarkDownTasks { get; }

        public ObservableCollection<CommonTask> CommonTasks { get; }

        //public ObservableCollection<>

    }
}
