using AutoMapper;
using AvailableGroups.AutoMapping;
using AvailableGroups.Controllers;
using AvailableGroups.Helpers;
using AvailableGroups.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{

    public class HomeControllUnitTest
    {
        [Fact]
        public async void HomeControl_GroupList_Test()
        {
            // arrange

            // -arrange mockApiService
            string data =  "{\"list\":[{\"id\":\"981fec8e-10a0-4892-94e7-0029b22a41be\",\"name\":\"sdf\",\"logoUrl\":null},{\"id\":\"66c97701-920f-4b5e-aad0-01cf51cf79b7\",\"name\":\"newvinyl\",\"logoUrl\":null},{\"id\":\"3a7e2abe-eadc-413d-8669-02773649415f\",\"name\":\"Grand Child\",\"logoUrl\":null},{\"id\":\"c08ca336-4eab-4678-bc6d-035beed4fff1\",\"name\":\"of\",\"logoUrl\":null},{\"id\":\"66942e25-808b-4c2b-93eb-04ae67f328e5\",\"name\":\"ytmlgroup\",\"logoUrl\":null},{\"id\":\"5fa29fcb-48ae-4393-bedb-059ef04259d1\",\"name\":\"Continue group\",\"logoUrl\":null},{\"id\":\"b5cba4c1-0e02-4440-a86a-06a417e8d9b9\",\"name\":\"Online Fact Find\",\"logoUrl\":null},{\"id\":\"8bbf73d2-4340-4f2f-9177-06d24358cbcd\",\"name\":\"newfuture\",\"logoUrl\":null},{\"id\":\"eee031db-52eb-41ce-a2cf-06e648cfaef0\",\"name\":\"groupname111901\",\"logoUrl\":null},{\"id\":\"95c88c33-42d2-4673-8554-08d81249f3bd\",\"name\":\"Roar Software Three\",\"logoUrl\":null},{\"id\":\"f1791d0c-88eb-4793-25cb-08d8174a1a59\",\"name\":\"Mcdonald\",\"logoUrl\":null},{\"id\":\"41968687-4105-4567-2507-08d8180bcdf1\",\"name\":\"Jeffrey Group sub Sub\",\"logoUrl\":null},{\"id\":\"a1e17200-0c03-4a96-6a59-08d81896d721\",\"name\":\"Jeffrey Sub sub\",\"logoUrl\":null},{\"id\":\"f196a92d-a41c-4b0f-973a-08d81c57aaea\",\"name\":\"Hello World 1111\",\"logoUrl\":null},{\"id\":\"4c19f6ae-16d8-4909-7323-08d81d71858d\",\"name\":\"My General Info\",\"logoUrl\":null},{\"id\":\"f915aa0e-0ebc-4afc-1206-08d81e1f52e0\",\"name\":\"Business Name +12\",\"logoUrl\":null},{\"id\":\"8738352c-456d-4f47-1207-08d81e1f52e0\",\"name\":\"Business Name +13\",\"logoUrl\":null},{\"id\":\"28848731-4341-4d9b-188e-08d81e20526f\",\"name\":\"Business Name + 14\",\"logoUrl\":null},{\"id\":\"f8646efa-a179-4446-2fbb-08d81e219606\",\"name\":\"Business Name +15\",\"logoUrl\":null},{\"id\":\"db790b53-289c-4a9b-5dd0-08d81e2272c7\",\"name\":\"Business Name +16\",\"logoUrl\":null},{\"id\":\"52b55ded-3104-4e78-5dd1-08d81e2272c7\",\"name\":\"ROAR 11\",\"logoUrl\":null},{\"id\":\"f00cf160-5463-4200-93a9-08d82488e89b\",\"name\":\"tab group\",\"logoUrl\":null},{\"id\":\"ba377c13-7029-4bbd-9a76-08d82edf43ac\",\"name\":\"test for restore account\",\"logoUrl\":null},{\"id\":\"b37ef05b-b17d-4323-3c67-08d8329aa118\",\"name\":\"ROAR Interviews\",\"logoUrl\":null},{\"id\":\"84ceded3-5ab7-4e99-3c68-08d8329aa118\",\"name\":\"Candidate 1\",\"logoUrl\":null}],\"page\":1,\"pageSize\":25,\"total\":156,\"totalPages\":7}";          
            var mockApiService = new Mock<IApiService>(); 
       
            mockApiService.Setup(a => a.GetApiDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(Task.FromResult(data));

            // -arrange mockIlogger
            var mockIlogger = new Mock<ILogger<HomeController>>();

            // -arrange mapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            var mapper = mockMapper.CreateMapper();

            //Act
            var controller = new HomeController(mockIlogger.Object, mapper, mockApiService.Object);
            var result = await controller.GetGroupListAsync("1", "25", "fake_token");

            //Assert
            Assert.NotNull(result);
            GroupPageModel model = Assert.IsAssignableFrom<GroupPageModel>(result);
            Assert.Equal(25, model.List.Count());
        }

        [Fact]
        public void GroupModel_GetFileExtensionFromUrl_Test()
        {
            Assert.True(GroupModel.GetFileExtensionFromUrl("../jpg.png?x=jpg") == ".png");
            Assert.True(GroupModel.GetFileExtensionFromUrl("wtf.png") == ".png");
            Assert.True(GroupModel.GetFileExtensionFromUrl("http://www.com/wtf.png?wtf") == ".png");
            Assert.True(GroupModel.GetFileExtensionFromUrl("jepg") == "");
            Assert.True(GroupModel.GetFileExtensionFromUrl("") == "");
        }
    }
}

