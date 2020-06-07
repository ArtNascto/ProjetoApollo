using System.Threading.Tasks;
using ProjetoApollo.Models.TokenAuth;
using ProjetoApollo.Web.Controllers;
using Shouldly;
using Xunit;

namespace ProjetoApollo.Web.Tests.Controllers
{
    public class HomeController_Tests: ProjetoApolloWebTestBase
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