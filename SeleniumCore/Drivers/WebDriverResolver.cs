using Autofac;
using OpenQA.Selenium;
using System.Reflection;

namespace SeleniumCore.Helpers.Interfaces
{
    public class WebDriverResolver : IWebDriverResolver
    {
        private readonly IComponentContext _context;

        public WebDriverResolver(IComponentContext context)
        {
            _context = context;
        }

        public IWebDriver Driver
        {
            get
            {
                var testWebDriver = _context.ResolveNamed<IWebDriverResolver>((Constants.BROWSER + "TestDriver").ToUpperInvariant());

                return testWebDriver?.Driver;
            }
        }

    }
}
