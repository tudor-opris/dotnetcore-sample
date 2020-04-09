using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using SeleniumCore.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SeleniumCore.Drivers
{
    public class EdgeTestDriver : BaseTestDriver, IWebDriverResolver
    {
        public string DateFormat => "dd MMMM yyyy";

        public IWebDriver Driver
        {
            get
            {
                if (WebDriver == null)
                {
                    var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

                    var service = EdgeDriverService.CreateDefaultService(path, @"msedgedriver.exe");
                    service.UseVerboseLogging = true;
                    service.UseSpecCompliantProtocol = true;

                    service.Start();

                    var caps = new DesiredCapabilities(new Dictionary<string, object>()
{
    { "ms:edgeOptions", new Dictionary<string, object>() {
        {  "binary", @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe" }
    }}
});

                    WebDriver = new RemoteWebDriver(service.ServiceUrl, caps);

                }
                return WebDriver;
            }
        }
    }
}
