﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using MdView.Templates;
using Newtonsoft.Json;
using TiDa.Views;
using Xamarin.Forms.Internals;
using System.Drawing.Imaging;
using System.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace TiDa.ViewModels
{
    public class SimpelWrPoEditViewModel : BaseViewModel
    {


        public string PhotoPath = null;
        private string _simpleTitle;

        
        private bool _isEdit = true;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        private bool _isPreview = false;

        public bool IsPreview
        {
            get => _isPreview;
            set => SetProperty(ref _isPreview, value);
        }

        public string SimpleTitle
        {
            get => _simpleTitle;
            set => SetProperty(ref _simpleTitle, value);
        }

        private string _content;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }




        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_simpleTitle);
        }

        public Command SaveCommand { get; }

        public Command CancelCommand { get; }

        public Command UploadImageCommand { get; }

        public Command ChangeVIewCommand { get; }

        public Command GoListCommand { get; }

        public SimpelWrPoEditViewModel()
        {
            if (SimpleWrPoDataStore.IsInitialized())
            {
                SimpleWrPoDataStore.InitializeAsync();
            }
            SaveCommand = new Command(SaveFunc, ValidateSave);
            CancelCommand = new Command(CancelFunc);
            UploadImageCommand = new Command(TakePhotoAsync);
            ChangeVIewCommand = new Command(ChangeFunc);
            GoListCommand = new Command(GoListFunc);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

        private async void GoListFunc()
        =>
            await Shell.Current.GoToAsync(nameof(SimpleWrPosView));
        

        private void ChangeFunc()
        {
            IsEdit = !IsEdit;
            IsPreview = !IsPreview;
        }

        private async void SaveFunc()
        {
            var simpleWrPo = new SimpleWrPo();
            simpleWrPo.Title = SimpleTitle;
            simpleWrPo.Content = Content;
            simpleWrPo.ImagePath = ImagePath;
            simpleWrPo.Timestamp = DateTime.Now.Ticks;
            await SimpleWrPoDataStore.InserItemAsync(simpleWrPo);
            await Shell.Current.GoToAsync("..");

        }

        private async void CancelFunc()
        {
            await Shell.Current.GoToAsync("..");
        }



        async void TakePhotoAsync()
        {
            string imgName = await TakeImageAction("123");
            if (imgName == null)
            {
                return;
            }

            var imagePath = JsonConvert.DeserializeObject<ImagePath>(imgName);
            ImagePath = $"http://121.37.91.77:3000/images/{imagePath.filePath}";
        }




        async Task<string> TakeImageAction(string s)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("提示", "取消", "确认", "拍照", "从相册选择");
            MediaFile pickFile = null;
            switch (action)
            {
                case "拍照":
                    //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("错误提示", "未找到可用摄像头", "确认");
                    //}
                    var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        CompressionQuality = 40,
                        CustomPhotoSize = 35,
                        PhotoSize = PhotoSize.Small,
                    }).ConfigureAwait(false);
                    //var photo = await MediaPicker.CapturePhotoAsync();
                    //if (photo == null)
                    //    return null;
                    //pickFile = photo;
                    pickFile = photo;
                    break;
                case "从相册选择":
                    //if (!CrossMedia.Current.IsPickPhotoSupported)
                    //{
                    //    await Application.Current.MainPage.DisplayAlert("错误提示", "未获得权限读取相册", "确认");
                    //}
                    //var fileb = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                    //{
                    //    CompressionQuality = 40,
                    //    CustomPhotoSize = 35,
                    //    PhotoSize = PhotoSize.Small,
                    //}).ConfigureAwait(true);
                    var fileb = await CrossMedia.Current.PickPhotoAsync();
                    pickFile = fileb;
                    //var fileb = await MediaPicker.PickPhotoAsync();

                    //if (fileb == null)
                    //    return null;
                    //pickFile = fileb;
                    break;
            }
            if (pickFile != null)
            {
                
                //上传图片到服务器
                HttpClient client = new HttpClient();
                #region
                MultipartFormDataContent form = new MultipartFormDataContent();
                StreamContent fileContent = new StreamContent(pickFile.GetStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                fileContent.Headers.ContentDisposition.FileName = System.DateTime.Now.ToString("yyyMMddHHmmss") + ".jpg";
                //fileContent.Headers.ContentDisposition.FileName = "ig";
                fileContent.Headers.ContentDisposition.Name = "img";
                form.Add(fileContent);
                #endregion
                HttpResponseMessage res = await client.PostAsync("http://121.37.91.77:3000/upload/image", form);
                var uploadModel = await res.Content.ReadAsStringAsync();
                if (uploadModel == null)
                {
                    await Application.Current.MainPage.DisplayAlert("错误提示", "未上传图片", "确认");
                }

                return uploadModel;
            }
            else
            {
                return "";
            }
        }



      
    }
}
