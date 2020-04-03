using OpenQA.Selenium;
namespace SeleniumCore.Drivers
{
    public class BaseTestDriver
    {
        protected IWebDriver WebDriver;

        public void Close()
        {
            if (WebDriver != null)
            {
                WebDriver.Close();
                WebDriver.Quit();
                WebDriver = null;
            }
        }
    }
}
