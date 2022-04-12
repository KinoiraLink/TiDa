using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using TiDa.Services;

namespace TiDa.ViewModels
{
    public class ViewModelLocator
    {
        //包装属性，注入实例
        /// <summary>
        /// 一般清单页ViewModel
        /// </summary>
        public CommonTasksViewModel CommonTasksViewModel => SimpleIoc.Default.GetInstance<CommonTasksViewModel>();


        /// <summary>
        /// 注册service和viewModel
        /// </summary>
        public ViewModelLocator()
        {
            
            SimpleIoc.Default.Register<IPreferenceStorage,PreferenceStoragecs>();
            SimpleIoc.Default.Register<ICommonTaskStorage,CommonTaskStorage>();


            SimpleIoc.Default.Register<CommonTasksViewModel>();
        }

    }
}
