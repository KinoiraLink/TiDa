using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TomatoNewView : ContentPage
    {
        TomatoNewViewModel viewModel;
        int _lastCheck = -1;
        private bool isAdd = false;
        private int oneSecond = 0;
        private int oneMin = 0;
        private int oneHour = 0;
        /// <summary>
        /// 指针形状
        /// </summary>
        struct HandParams
        {
            public HandParams(double width, double height, double offset) : this()
            {
                Width = width;
                Height = height;
                Offset = offset;
            }

            public double Width { private set; get; }
            public double Height { private set; get; }
            public double Offset { private set; get; }  // 旋转中心
        }

        static readonly HandParams secondParams = new HandParams(0.02, 1.1, 0.85);
        static readonly HandParams minuteParams = new HandParams(0.05, 0.8, 0.9);
        static readonly HandParams hourParams = new HandParams(0.125, 0.65, 0.9);

        /// <summary>
        /// 表盘
        /// </summary>
        BoxView[] tickMarks = new BoxView[60];
        public TomatoNewView()
        {
            InitializeComponent();
            BindingContext = viewModel = new TomatoNewViewModel();
            for (int i = 0; i < tickMarks.Length; i++)
            {
                tickMarks[i] = new BoxView { Color = Color.Gray };
                absoluteLayout.Children.Add(tickMarks[i]);
            }

            
        }

        void OnAbsoluteLayoutSizeChanged(object sender, EventArgs args)
        {
            /*
             * 绘制表盘
             */
            // Get the center and radius of the AbsoluteLayout.
            Point center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            double radius = 0.45 * Math.Min(absoluteLayout.Width, absoluteLayout.Height);
            // Position, size, and rotate the 60 tick marks.
            for (int index = 0; index < tickMarks.Length; index++)
            {
                double size = radius / (index % 5 == 0 ? 15 : 30);
                double radians = index * 2 * Math.PI / tickMarks.Length;
                double x = center.X + radius * Math.Sin(radians) - size / 2;
                double y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(tickMarks[index], new Rectangle(x, y, size, size));
                tickMarks[index].Rotation = 180 * radians / Math.PI;
            }


            LayoutHand(secondHand, secondParams, center, radius);
            LayoutHand(minuteHand, minuteParams, center, radius);
            LayoutHand(hourHand, hourParams, center, radius);
        }
        /// <summary>
        /// 指针位置
        /// </summary>
        /// <param name="boxView"></param>
        /// <param name="handParams"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        void LayoutHand(BoxView boxView, HandParams handParams, Point center, double radius)
        {
            double width = handParams.Width * radius;
            double height = handParams.Height * radius;
            double offset = handParams.Offset;

            AbsoluteLayout.SetLayoutBounds(boxView,
                new Rectangle(center.X - 0.5 * width,
                    center.Y - offset * height,
                    width, height));

            // Set the AnchorY property for rotations.
            boxView.AnchorY = handParams.Offset;
        }

    }
}