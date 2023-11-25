using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public  class RegisterViewModel : BaseViewModel
    {
        private User _registerUser;

        public User RegisterUser
        {
            get => _registerUser;
            set => SetProperty(ref _registerUser, value);
        }
        private string _acount;

        public string Account
        {
            get => _acount;
            set => SetProperty(ref _acount, value);
        }

        private string _pwd;

        public string Pwd
        {
            get => _pwd;
            set => SetProperty(ref _pwd, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_acount)
                   && !String.IsNullOrWhiteSpace(_pwd);
        }

        private string _nickName;

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        public Command RegisterCommand { get; set; }

        public Command UploadImageCommand { get; set; }
        public Command ReutrnCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(RegisterFunc,ValidateSave);

            UploadImageCommand = new Command(TakePhotoAsync);

            ReutrnCommand = new Command(ReturnFunc);
            this.PropertyChanged +=
                (_, __) => RegisterCommand.ChangeCanExecute();
        }

        private async void RegisterFunc()
        {
            var user = new User();
            user.account = Account;
            user.pwd = Pwd;
            RegisterUser = await LoginWebService.RegisterAsync(user);
            if (RegisterUser.msg.Equals("注册成功"))
            {
                var userInfo = new UserInfo();
                userInfo.UserCookie = RegisterUser.token;
                userInfo.NickName = NickName;
                userInfo.Email = Email;
                userInfo.ImagePath = ImagePath;
                await RegisterWebService.RegisterAsync(userInfo);
            }

            if (RegisterUser.msg.Equals("已有此用户"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", RegisterUser.msg, "OK");
            }
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
        private async void ReturnFunc()
        {
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        async Task<string> TakeImageAction(string s)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("提示", "取消", "确认", "拍照", "从相册选择");
            MediaFile pickFile = null;
            switch (action)
            {
                case "拍照":
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

                    var fileb = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                    {
                        CompressionQuality = 40,
                        CustomPhotoSize = 35,
                        PhotoSize = PhotoSize.Small,
                    }).ConfigureAwait(true);
                    pickFile = fileb;
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
                return uploadModel;
            }
            else
            {
                return "";
            }
        }
    }
}
