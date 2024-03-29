﻿using GalaSoft.MvvmLight;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using TiDa.Models;
using TiDa.Services;

namespace TiDa.ViewModels 
{
    public class CommonTasksViewModel : ViewModelBase
    {
        //******** 私有变量
        /// <summary>
        /// 一般Task存储
        /// </summary>
        private ICommonTaskStorage _commonTaskStorage;

        //Todo  导航存储

        //******** 构造函数
        public CommonTasksViewModel(ICommonTaskStorage commonTaskStorage)
        {
            _commonTaskStorage = commonTaskStorage;
            _commonTaskStorage.UpdateMode += _commonTaskStorage_UpdateMode;
            
        }

        private void _commonTaskStorage_UpdateMode(object sender, commonTaskStroageUpdateEventArgs e)
        {
            throw new NotImplementedException();
            //Todo 修改
            //Todo 删除 
            //Todo 完成
            //Todo 添加
        }

        //******** 绑定属性

        public ObservableRangeCollection<CommonTask> CommonTaskCollection { get; } =
            new ObservableRangeCollection<CommonTask>();

        ///// <summary>
        ///// 一般Task
        ///// </summary>
        //private CommonTask _commonTask;

        //public CommonTask CommonTask
        //{
        //    get => _commonTask;
        //    set => Set(nameof(CommonTask), ref _commonTask, value);
        //}

        //******** 绑定命令

        /// <summary>
        /// comment
        /// </summary>
        private RelayCommand _pageAppearingCommand;

        public RelayCommand PageAppearingCommand =>
            _pageAppearingCommand ?? (_pageAppearingCommand = new RelayCommand(async () => await PageAppearingCommandFunction()));

        public async Task PageAppearingCommandFunction()
        {
            var canRun = false;

            //二次加锁
            if (!_isLoaded)//A,B
            {
                lock (_isLoadedLock)//(A in,B blocked) or (A blocked,B in)
                {
                    if (!_isLoaded)
                    {
                        canRun = true;
                        _isLoaded = true;
                    }
                }
            }

            if (!canRun)
            {
                return;
            }

            CommonTaskCollection.Clear();
            //Todo 零时清页
            if (!_commonTaskStorage.IsInitialized())
            {
                await _commonTaskStorage.InitializeAsync();
            }
            CommonTaskCollection.AddRange(await _commonTaskStorage.GetCommonsTasksAsync());
        }


        //******** 私有变量
        /// <summary>
        ///页面是否已加载
        /// </summary>
        private volatile bool _isLoaded;//volatile禁用Cpu编译器的优化


        /// <summary>
        /// 页面是否已加载锁
        /// </summary>
        private readonly object _isLoadedLock = new object();
    }
}
