using OpenQA.Selenium;
using SeleniumCore.Helpers.BaseClasses;
using SeleniumCore.Helpers;
using SeleniumCore.Pages;

namespace SeleniumCore.Steps
{
    public class LoginSteps : BaseSteps
    {
        private LoginPage _loginPage;
        public LoginSteps(LoginPage loginPage)
        {
            _loginPage = loginPage;
        }

        public void PerformLogin(string userEmail, string password)
        {
            _loginPage.NavigateTo(Constants.BASE_URL);
            _loginPage.PerformLogin(userEmail, password);
        }

        public string CheckThatUserNameIsDisplayed(string userName)
        {
            return _loginPage.CheckThatUserNameIsDisplayed(userName);
        }
    }
}
