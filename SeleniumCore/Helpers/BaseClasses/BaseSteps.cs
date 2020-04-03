using FluentAssertions;
using OpenQA.Selenium;
using SeleniumCore.Pages;

namespace SeleniumCore.Helpers.BaseClasses
{
    public class BaseSteps
    {
        protected BasePage BasePage;
        protected LoginPage LoginPage;
        public IWebDriver Driver;

        public BaseSteps(IWebDriver driver)
        {
            Driver = driver;
            BasePage = new BasePage(driver);
            LoginPage = new LoginPage(driver);
        }

        public void NavigateTo(string baseUrl)
        {
            BasePage.NavigateTo(baseUrl);
        }

    }
}
