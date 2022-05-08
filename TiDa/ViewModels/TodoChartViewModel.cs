using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using TiDa.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TiDa.ViewModels
{
    public  class TodoChartViewModel : BaseViewModel
    {
        //私有变量
        private long firstdaytick = DateTime.Now.Ticks - 6048000000000;
        private int firstdaycount = 0;
        private long seconddaytick = DateTime.Now.Ticks - 5184000000000;
        private int secondcount = 0;
        private long thirddaytick = DateTime.Now.Ticks - 4320000000000;
        private int thirddaycount = 0;
        private long fourthdaytick = DateTime.Now.Ticks - 3456000000000;
        private int fourthdaycount = 0;
        private long fifthdaytick = DateTime.Now.Ticks - 2592000000000;
        private int fifthdaycount = 0;
        private long sixthdaytick = DateTime.Now.Ticks - 1728000000000;
        private int sixthdaycount = 0;
        private long sevethdaytick = DateTime.Now.Ticks - 864000000000;
        private int sevethdaycount = 0;
        //公有变量
        public ObservableCollection<CommonTask> CommonTasks { get; set; }

        public Command LoadCommand { get; }

        public DonutChart ComDonutChart { get; }

        public BarChart WeekComBarChart { get; }

        public LineChart WeekMarLineChart { get; }

        public PointChart WeekTomPointChart { get; }

        public ObservableCollection<CommonTask> ComDeletes { get; set; }

        public ObservableCollection<CommonTask> ComDones { get; set; }



        public TodoChartViewModel()
        {
            CommonTasks = new ObservableCollection<CommonTask>();
            LoadCommand = new Command(async () => await LoadFunc());
            ComDeletes = new ObservableCollection<CommonTask>();
            ComDones = new ObservableCollection<CommonTask>();
            ComDonutChart = new DonutChart();
            WeekComBarChart = new BarChart();
            WeekMarLineChart = new LineChart();
            WeekTomPointChart = new PointChart();
            LoadFunc();
        }

        private async Task LoadFunc()
        {
            IsBusy = true;
            try
            {
                #region CommonRegion

                var commonTasks = await CommonDataStore.GetAllItemsAsync();
                foreach (CommonTask commonTask in commonTasks)
                {
                    if (commonTask.Done == true)
                    {
                        ComDones.Add(commonTask);
                    }
                    else if (commonTask.IsDeleted == true)
                    {
                        ComDeletes.Add(commonTask);
                    }
                    else
                    {
                        CommonTasks.Add(commonTask);
                    }
                    //第一天
                    if (firstdaytick < commonTask.Timestamp && commonTask.Timestamp <= seconddaytick)
                    {
                        firstdaycount += 1;
                    }
                    //第二天
                    if (seconddaytick < commonTask.Timestamp && commonTask.Timestamp <= thirddaytick)
                    {
                        secondcount += 1;
                    }
                    //第三天
                    if (thirddaytick < commonTask.Timestamp && commonTask.Timestamp <= fourthdaytick)
                    {
                        thirddaycount += 1;
                    }
                    //第四天
                    if (fourthdaytick < commonTask.Timestamp && commonTask.Timestamp <= fifthdaytick)
                    {
                        fourthdaycount += 1;
                    }
                    //第五天
                    if (fifthdaytick < commonTask.Timestamp && commonTask.Timestamp <= sixthdaytick)
                    {
                        fifthdaycount += 1;
                    }
                    //第六天
                    if (sixthdaytick < commonTask.Timestamp && commonTask.Timestamp <= sevethdaytick)
                    {
                        sixthdaycount += 1;
                    }
                    //第七天
                    if (sevethdaytick < commonTask.Timestamp && commonTask.Timestamp <= DateTime.Now.Ticks)
                    {
                        sevethdaycount += 1;
                    }

                }

                var comentries = new[]
                {
                    new ChartEntry(ComDones.Count)
                    {
                        Label = "ToDone",
                        ValueLabel = ComDones.Count.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(ComDeletes.Count)
                    {
                        Label = "ToDelete",
                        ValueLabel = ComDeletes.Count.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(CommonTasks.Count)
                    {
                        Label = "Toding",
                        ValueLabel = CommonTasks.Count.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    }
                };
                ComDonutChart.Entries = comentries;
                var weekcomentries = new[]
                {
                    new ChartEntry(firstdaycount)
                    {
                        Label = "FirstDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(secondcount)
                    {
                        Label = "SecondDay",
                        ValueLabel = secondcount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(thirddaycount)
                    {
                        Label = "ThirdDay",
                        ValueLabel = thirddaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },

                    new ChartEntry(fourthdaycount)
                    {
                        Label = "FourthDay",
                        ValueLabel = fourthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(fifthdaycount)
                    {
                        Label = "FifthDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(sixthdaycount)
                    {
                        Label = "SixthDay",
                        ValueLabel = sixthdaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },
                    new ChartEntry(sevethdaycount)
                    {
                        Label = "SeventhDay",
                        ValueLabel = sevethdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    }

                };
                WeekComBarChart.Entries = weekcomentries;
                firstdaycount = 0;
                secondcount = 0;
                thirddaycount = 0;
                fourthdaycount = 0;
                fifthdaycount = 0;
                sixthdaycount = 0;
                sevethdaycount = 0;
                #endregion

                #region MarkdownRegion

                var markDownTasks = await MarkDownDataStore.GetAllItemsAsync();
                foreach (var markDownTask in markDownTasks)
                {
                    //第一天
                    if (firstdaytick < markDownTask.Timestamp && markDownTask.Timestamp <= seconddaytick)
                    {
                        firstdaycount += 1;
                    }
                    //第二天
                    if (seconddaytick < markDownTask.Timestamp && markDownTask.Timestamp <= thirddaytick)
                    {
                        secondcount += 1;
                    }
                    //第三天
                    if (thirddaytick < markDownTask.Timestamp && markDownTask.Timestamp <= fourthdaytick)
                    {
                        thirddaycount += 1;
                    }
                    //第四天
                    if (fourthdaytick < markDownTask.Timestamp && markDownTask.Timestamp <= fifthdaytick)
                    {
                        fourthdaycount += 1;
                    }
                    //第五天
                    if (fifthdaytick < markDownTask.Timestamp && markDownTask.Timestamp <= sixthdaytick)
                    {
                        fifthdaycount += 1;
                    }
                    //第六天
                    if (sixthdaytick < markDownTask.Timestamp && markDownTask.Timestamp <= sevethdaytick)
                    {
                        sixthdaycount += 1;
                    }
                    //第七天
                    if (sevethdaytick < markDownTask.Timestamp && markDownTask.Timestamp <= DateTime.Now.Ticks)
                    {
                        sevethdaycount += 1;
                    }
                }
                var weekmarentries = new[]
                {
                    new ChartEntry(firstdaycount)
                    {
                        Label = "FirstDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(secondcount)
                    {
                        Label = "SecondDay",
                        ValueLabel = secondcount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(thirddaycount)
                    {
                        Label = "ThirdDay",
                        ValueLabel = thirddaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },

                    new ChartEntry(fourthdaycount)
                    {
                        Label = "FourthDay",
                        ValueLabel = fourthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(fifthdaycount)
                    {
                        Label = "FifthDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(sixthdaycount)
                    {
                        Label = "SixthDay",
                        ValueLabel = sixthdaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },
                    new ChartEntry(sevethdaycount)
                    {
                        Label = "SeventhDay",
                        ValueLabel = sevethdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    }

                };
                WeekMarLineChart.Entries = weekmarentries;
                firstdaycount = 0;
                secondcount = 0;
                thirddaycount = 0;
                fourthdaycount = 0;
                fifthdaycount = 0;
                sixthdaycount = 0;
                sevethdaycount = 0;
                #endregion

                #region TomatoRegion

                var tomatoTasks = await TomatoDataStore.GetAllItemsAsync();
                foreach (TomatoTask tomatoTask in tomatoTasks)
                {
                    //第一天
                    if (firstdaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= seconddaytick)
                    {
                        firstdaycount += 1;
                    }
                    //第二天
                    if (seconddaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= thirddaytick)
                    {
                        secondcount += 1;
                    }
                    //第三天
                    if (thirddaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= fourthdaytick)
                    {
                        thirddaycount += 1;
                    }
                    //第四天
                    if (fourthdaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= fifthdaytick)
                    {
                        fourthdaycount += 1;
                    }
                    //第五天
                    if (fifthdaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= sixthdaytick)
                    {
                        fifthdaycount += 1;
                    }
                    //第六天
                    if (sixthdaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= sevethdaytick)
                    {
                        sixthdaycount += 1;
                    }
                    //第七天
                    if (sevethdaytick < tomatoTask.Timestamp && tomatoTask.Timestamp <= DateTime.Now.Ticks)
                    {
                        sevethdaycount += 1;
                    }
                }
                var weektomentries = new[]
                {
                    new ChartEntry(firstdaycount)
                    {
                        Label = "FirstDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(secondcount)
                    {
                        Label = "SecondDay",
                        ValueLabel = secondcount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(thirddaycount)
                    {
                        Label = "ThirdDay",
                        ValueLabel = thirddaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },

                    new ChartEntry(fourthdaycount)
                    {
                        Label = "FourthDay",
                        ValueLabel = fourthdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(fifthdaycount)
                    {
                        Label = "FifthDay",
                        ValueLabel = fifthdaycount.ToString(),
                        Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(sixthdaycount)
                    {
                        Label = "SixthDay",
                        ValueLabel = sixthdaycount.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    },
                    new ChartEntry(sevethdaycount)
                    {
                        Label = "SeventhDay",
                        ValueLabel = sevethdaycount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    }

                };
                WeekTomPointChart.Entries = weektomentries;

                #endregion



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
