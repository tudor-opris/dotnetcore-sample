using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumCore.Helpers;
using SeleniumCore.Helpers.BaseClasses;
using SeleniumCore.Steps;

namespace SeleniumCore.Tests
{
    [TestClass]
    public class UserCanLogIn : BaseTest
    {

        [TestMethod]
        public void UserCanLogInTest()
        {
            _loginSteps.PerformLogin(Constants.USER_EMAIL, Constants.USER_PASSWORD);
            _loginSteps.CheckThatUserNameIsDisplayed(Constants.USER_NAME).Should().Be(Constants.USER_NAME);
        }
    }
}
