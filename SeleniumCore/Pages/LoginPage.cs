using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using SeleniumCore.Helpers;
using SeleniumCore.Helpers.BaseClasses;
using SeleniumCore.Helpers.Interfaces;

namespace SeleniumCore.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriverResolver webDriverResolver) : base(webDriverResolver)
        {
        }

        #region Selectors

        private readonly By _userNameInput = By.CssSelector("#logonIdentifier");
        private readonly By _passwordInput = By.CssSelector("#password");
        private readonly By _loginButton = By.CssSelector("#next");
        private readonly By _rootDiv = By.CssSelector(".navbar-light");
        private readonly By _userNameButton = By.CssSelector("button.navbar-header-profile-light");

        #endregion

        public void PerformLogin(string username, string password)
        {
            WaitUntilElementIsVisible(_loginButton);
            EnterUserName(username);
            EnterPassword(password);
            Submit(_loginButton);
            WaitUntilElementIsVisible(_rootDiv, Constants.LOAD_TIME_SECONDS);
        }

        public void EnterUserName(string username)
        {
            Driver.FindElement(_userNameInput).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Driver.FindElement(_passwordInput).SendKeys(password);
        }

        public string CheckThatUserNameIsDisplayed(string userName)
        {
            return GetTextFromElement(_userNameButton);
        }

    }
}
