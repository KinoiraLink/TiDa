using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestTiDaProject.Helpers;
using TiDa.Models;
using TiDa.Services;

namespace TestTiDaProject.Services
{
    public class TestCommonTaskStorage
    {
        //[SetUp, TearDown]
        //public static void RemoveDatebaseFile() =>
        //    CommonTaskStorageHelper.RemoveDatabaseFile();

        [Test]
        public async Task TaskInitialize()
        {
            var mock = new Mock<IPreferenceStorage>();
            mock.Setup(p => p.Get(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.DefultVersion)).Returns(CommonTaskStorageConstants.Version-1);
            var mockPreferenceStorage = mock.Object;
            var commonTaskStorage = new CommonTaskStorage(mockPreferenceStorage);
            Assert.IsFalse(commonTaskStorage.IsInitialized());
            mock.Setup(p => p.Get(CommonTaskStorageConstants.VersionKey, CommonTaskStorageConstants.DefultVersion)).Returns(CommonTaskStorageConstants.Version);
            Assert.IsTrue(commonTaskStorage.IsInitialized());

            Assert.IsFalse(File.Exists(CommonTaskStorage.CommonTaskDbPath));
            await commonTaskStorage.InitializeAsync();
            Assert.IsTrue(File.Exists(CommonTaskStorage.CommonTaskDbPath));
            await commonTaskStorage.CloseAsync();
        }

        [Test]
        public async Task TestCrud()
        {
            var commontaskStorage = await CommonTaskStorageHelper.GetInitilizedCommonTaskStorageAsync();


            var commonTasks = new List<CommonTask>()
            {
                new CommonTask
                {
                    Done = true, TaskDate = "2022.01.03", TaskDescribe = "一个测试", TaskTime = "17:00",
                    TaskTitle = "ceshi1", Id = 1
                },
                new CommonTask
                {
                    Done = false, TaskDate = "2022.01.04", TaskDescribe = "另一个测试", TaskTime = "17:30",
                    TaskTitle = "ceshi2", Id = 2
                },
            };

            await commontaskStorage.SaveCommonTaskAsync(commonTasks[0]);
            await commontaskStorage.SaveCommonTaskAsync(commonTasks[1]);

            var commonTaskAsync = await commontaskStorage.GetCommonTaskAsync(commonTasks[0]);
            Assert.AreEqual(commonTasks[0].Done,commonTaskAsync.Done);
            

            var commonsTasksAsync = await commontaskStorage.GetCommonsTasksAsync();
            Assert.AreEqual(commonTasks.Count, commonsTasksAsync.Count);

            await commontaskStorage.DeleteCommonTaskAsync(commonTasks[0]);
            commonsTasksAsync = await commontaskStorage.GetCommonsTasksAsync();
            Assert.AreEqual(commonTasks.Count-1, commonsTasksAsync.Count);


            await commontaskStorage.CloseAsync();
        }
    }
}
