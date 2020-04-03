using OpenQA.Selenium;

namespace SeleniumCore.Helpers.Interfaces
{

    public interface ITestWebDriver
    {
        string DateFormat { get; }

        IWebDriver Driver { get; }

        void Close();
    }
}

