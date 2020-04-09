using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCore.Helpers.Interfaces
{
    public interface IWebDriverResolver
    {
        IWebDriver Driver { get; }
    }

}
