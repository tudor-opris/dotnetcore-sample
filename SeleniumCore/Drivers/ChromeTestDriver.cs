using Lucene.Net.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCore.Helpers.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SeleniumCore.Drivers
{
    public class ChromeTestDriver : BaseTestDriver, ITestWebDriver
    {
        public string DateFormat => "dd MMMM yyyy";

        public IWebDriver Driver
        {
            get
            {
                if (WebDriver == null)
                {
                    var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

                    HashMap<String, Object> chromePrefs = new HashMap<String, Object>();
                    chromePrefs.Add("profile.default_content_settings.popups", 0);
                    chromePrefs.Add("download.default_directory", path);

                    var options = new ChromeOptions();
                    options.AddArgument("no-sandbox");
                    options.AddArgument("--disable-infobars"); // disabling infobars
                    options.AddArgument("--disable-extensions"); // disabling extensions
                                                                 //   options.AddArgument("--headless"); // runs chrome headless
                    options.AddArgument("--disable-gpu"); // applicable to windows os only
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--ignore-certificate-errors");
                    options.AddUserProfilePreference("download.default_directory", path);

                    WebDriver = new ChromeDriver(path, options);
                       
                }
                return WebDriver;
            }
        }
    }
}
