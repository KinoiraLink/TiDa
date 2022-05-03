using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using MdView.Templates;

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
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

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
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
                ImagePath = PhotoPath;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
    }
}
