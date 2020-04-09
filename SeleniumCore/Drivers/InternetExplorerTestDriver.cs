using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using SeleniumCore.Helpers.Interfaces;
using SeleniumCore.Helpers;
using System;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Reflection;

namespace SeleniumCore.Drivers
{

    public class InternetExplorerTestDriver : BaseTestDriver, IWebDriverResolver
    {
        public string DateFormat => null;

        public IWebDriver Driver
        {
            get
            {
                if (WebDriver == null)
                {
                    var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

                    var options = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        //RequireWindowFocus = true,
                        EnsureCleanSession = true,
                        IgnoreZoomLevel = true,
                    };

                    WebDriver = new InternetExplorerDriver(path, options);

                }

                return WebDriver;
            }
        }
    }
}
