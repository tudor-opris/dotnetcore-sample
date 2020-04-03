using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumCore.Helpers.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SeleniumCore.Drivers
{
    public class FirefoxTestDriver : BaseTestDriver, ITestWebDriver
    {
        public string DateFormat => "dd MMMM yyyy";

        public IWebDriver Driver
        {
            get
            {
                if (WebDriver == null)
                {
                    var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);


                    FirefoxDriverService geckoService = FirefoxDriverService.CreateDefaultService(path);
                    geckoService.Host = "::1";
                    var options = new FirefoxOptions();
                    options.AcceptInsecureCertificates = true;

                    WebDriver = new FirefoxDriver(geckoService, options);

                }
                return WebDriver;
            }
        }
    }
}
