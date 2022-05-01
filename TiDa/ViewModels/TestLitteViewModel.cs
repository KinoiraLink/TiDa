using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    internal class TestLitteViewModel : BaseViewModel
    {
        private string _testTitle;

        public string TestTitle
        {
            get => _testTitle;
            set => SetProperty(ref _testTitle, value);
        }


        public ObservableCollection<TestModel> Tests { get; }
        public Command NewitemCommand { get; }
        public TestLitteViewModel()
        {
            TestTitle = "父测试项";
            Tests = new ObservableCollection<TestModel>();
            Tests.Add(new TestModel());
            NewitemCommand = new Command(AddFunction);
        }


        public void AddFunction()
        {
            Tests.Add(new TestModel());
        }


    }
}
public class TestModel
{
    public string Title { get; set; }= "子测试项";
}
