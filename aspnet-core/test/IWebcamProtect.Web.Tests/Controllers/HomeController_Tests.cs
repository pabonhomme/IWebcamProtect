using System.Threading.Tasks;
using IWebcamProtect.Models.TokenAuth;
using IWebcamProtect.Web.Controllers;
using Shouldly;
using Xunit;

namespace IWebcamProtect.Web.Tests.Controllers
{
    public class HomeController_Tests: IWebcamProtectWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}