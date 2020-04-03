using OpenQA.Selenium;
using SeleniumCore.Helpers.BaseClasses;
using SeleniumCore.Helpers;

namespace SeleniumCore.Steps
{
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        public void PerformLogin(string userEmail, string password)
        {
            NavigateTo(Constants.BASE_URL);
            LoginPage.PerformLogin(userEmail, password);
        }

        public string CheckThatUserNameIsDisplayed(string userName)
        {
            return LoginPage.CheckThatUserNameIsDisplayed(userName);
        }
    }
}
