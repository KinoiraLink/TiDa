using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MdView.Templates;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkdownReader : ContentPage
    {
        private string text;
        private mdReadViewModel viewModel;
        private bool isNight = false;
        private bool isGreen = false;
        public MarkdownReader()
        {
            InitializeComponent();
            Slider.Minimum = 0;
            BindingContext = viewModel = new mdReadViewModel();
            

        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            top.IsVisible = !top.IsVisible;
            bottom.IsVisible = !bottom.IsVisible;
            
        }



        private async void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            Slider.Maximum = ScrollView.Content.Height;
            await ScrollView.ScrollToAsync(0, Slider.Value, true);
        }

        public async Task SpeakNowDefaultSettings()
        {
            text = viewModel.TaskContent;
            await TextToSpeech.SpeakAsync(text);

            // This method will block until utterance finishes.
        }



        private async void Btn_Speak(object sender, EventArgs e)
        {
            await SpeakNowDefaultSettings();
        }

        private void Btn_Noght(object sender, EventArgs e)
        {
            isNight = !isNight;
            if (isNight==true)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    BackGround.BackgroundColor = Color.DarkGray;
                }

                if (Device.RuntimePlatform == Device.UWP)
                {
                    BackGround.BackgroundColor = Color.Black;
                    BackGround.Opacity = 0.6;
                }

                
            }
            else
            {
                BackGround.BackgroundColor = Color.White;
                BackGround.Opacity = 1;
            }
        }

        private void Btn_Green(object sender, EventArgs e)
        {
            isGreen = !isGreen;
            if (isGreen)
            {
                BackGround.BackgroundColor = Color.Aquamarine;
            }

            else
            {
                BackGround.BackgroundColor = Color.White;
                BackGround.Opacity = 1;
            }
        }
    }
}