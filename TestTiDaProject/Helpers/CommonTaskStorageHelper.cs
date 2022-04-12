using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TiDa.Services;

namespace TestTiDaProject.Helpers
{
    public class CommonTaskStorageHelper
    {
        public static async Task<CommonTaskStorage> GetInitilizedCommonTaskStorageAsync()
        {
            var commonTaskStorage = new CommonTaskStorage(new Mock<IPreferenceStorage>().Object);
            await commonTaskStorage.InitializeAsync();
            return commonTaskStorage;
        }

        /// <summary>
        /// 删除收藏数据文件
        /// </summary>
        public static void RemoveDatabaseFile()
        {
            File.Delete(CommonTaskStorage.CommonTaskDbPath);
        }
    }
}
