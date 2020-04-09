using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCore.Helpers.Interfaces;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SeleniumCore.Helpers.BaseClasses
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriverResolver webDriverResolver)
        {
            Driver = webDriverResolver.Driver;
        }

        public readonly By _datePickerContainer = By.CssSelector("div.calendar-light");
        private readonly By _monthYearDiv = By.CssSelector("div.ms-DatePicker-monthAndYear");
        private readonly By _yearDiv = By.CssSelector("div[class*=currentYear]");
        private readonly By _nextYearButton = By.CssSelector("button[class*=nextYear]");

        private readonly By _previousYearButton = By.CssSelector("button[class*=prevYear]");
        private readonly By _monthOptionContainer = By.CssSelector("div[class*=optionGrid]");

        private readonly By _dayTableContainer = By.CssSelector("table[class*=DatePicker]");
        private readonly By _fieldValueContainer = By.CssSelector("div[id*='view'] .field");
        private readonly By _fieldValueOnEditContainer = By.CssSelector(".field");
        private readonly By _radioButtonsListOfOptions = By.CssSelector("div.list-of-choices label");

        private readonly By _spinnerDiv = By.CssSelector(".loading-spinner");

        public void NavigateTo(string baseUrl)
        {
            Driver.Navigate().GoToUrl(baseUrl);
            WaitForDocumentReadyState();
        }

        protected void WaitUntilElementIsVisible(By element)
        {
            WaitForDocumentReadyState();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }


        public bool CheckIfAnElementDissapeared(By selector)
        {
            bool result;
            try
            {
                result = Driver.FindElement(selector).Displayed;
            }
            catch (NoSuchElementException)
            {
                result = false;
            }
            catch (StaleElementReferenceException)
            {
                result = false;
            }

            return !result;
        }


        protected void WaitUntilElementDissapeared(By element)
        {
            WaitForDocumentReadyState();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
        }

        protected void WaitForElementToExist(By element)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until(ExpectedConditions.ElementExists(element));
        }

        internal void RefreshPage()
        {
            Driver.Navigate().Refresh();
        }

        protected void WaitUntilElementIsVisible(By element, double seconds)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }

        public string GetPageTitle()
        {
            WaitForDocumentReadyState();
            WaitUntilElementHasText(By.CssSelector(".page-title"));
            return Driver.FindElement(By.CssSelector(".page-title")).Text;
        }


        protected void WaitUntilElementIsClickable(By element)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        protected void ClickOnElement(By selector)
        {
            WaitUntilElementIsClickable(selector);
            ScrollToElement(selector);
            Driver.FindElement(selector).Click();
            WaitForDocumentReadyState();
        }

        protected void SendKeysToInput(By selector, string values)
        {
            WaitUntilElementIsVisible(selector);
            ClearInputField(selector);

            Driver.FindElement(selector).FindElement(By.CssSelector("input")).SendKeys(values);
        }




        protected void ClearInputField(By selector)
        {
            WaitUntilElementIsVisible(selector);
            ClickOnElement(selector);

            IWebElement webelement = Driver.FindElement(selector).FindElement(By.CssSelector("input"));

            webelement.SendKeys(Keys.Control + "a");
            webelement.SendKeys(Keys.Delete);
        }

        protected void ClearTextareaField(By selector)
        {
            WaitUntilElementIsVisible(selector);
            ClickOnElement(selector);
            SendKeysToTextarea(selector, Keys.Control + "a");
            SendKeysToTextarea(selector, Keys.Delete);
        }


        protected void Submit(By element)
        {
            Driver.FindElement(element).Submit();
        }

        protected void WaitForDocumentReadyState()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until((d) =>
                ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete")
                && CheckIfAnElementDissapeared(_spinnerDiv));
        }


        protected void WaitUntilSpinnerDissapeared()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until((d) => (ExpectedConditions.ElementIsVisible(_spinnerDiv)));
            wait.Until((d) => (ExpectedConditions.InvisibilityOfElementLocated(_spinnerDiv)));
        }


        public void UploadFile(By inputFile, string filePath)
        {
            WaitForDocumentReadyState();
            WaitForElementToExist(inputFile);
            Driver.FindElement(inputFile).SendKeys(filePath);
        }

        protected void UploadMultipleFiles(By inputFile, string[] allFilesPath)
        {
            WaitForDocumentReadyState();
            WaitForElementToExist(inputFile);
            var allUploadedFilesPath = string.Join("\n", allFilesPath);

            Driver.FindElement(inputFile).SendKeys(allUploadedFilesPath);
        }


        protected void WaitUntilElementHasText(By element)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until((d) => Driver.FindElement(element).Text.Length > 0);
        }
        protected void WaitUntilWebElementHasText(IWebElement webElement)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until((d) => webElement.Text.Length > 0);
        }


        protected void WaitUntilListHasElements(By element)
        {
            WaitForDocumentReadyState();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Constants.WAIT_TIME_SECONDS));
            wait.Until((d) => Driver.FindElements(element).Count > 0);
        }


        protected string GetTextFromElement(By element)
        {
            WaitUntilElementHasText(element);
            return Driver.FindElement(element).Text;
        }

        protected void SelectOptionFromDropdownByValue(By selector, string statusValue)
        {
            WaitUntilElementIsVisible(selector);

            SelectElement dropdown = new SelectElement(Driver.FindElement(selector).FindElement(By.CssSelector("select")));
            dropdown.SelectByText(statusValue);
        }

        protected string SelectRandomValueFromDropdown(By selector)
        {
            WaitUntilElementIsVisible(selector);
            ScrollToElement(selector);

            IWebElement dropdownContainer = Driver.FindElement(selector).FindElement(By.CssSelector("select"));
            dropdownContainer.Click();

            IList<IWebElement> optionsList = dropdownContainer.FindElements(By.CssSelector("option"));
            var randomIndex = GetRandomNumber(optionsList.Count());
            var randomElement = optionsList[randomIndex];

            randomElement.Click();
            
            return randomElement.Text.Trim();

        }


        protected string SelectRandomValueFromDropdownForMandatoryField(By selector)
        {
            WaitUntilElementIsVisible(selector);
            ScrollToElement(selector);
            IWebElement dropdownContainer = Driver.FindElement(selector);
            IList<IWebElement> dropdownOptions = dropdownContainer.FindElements(By.CssSelector("select option[value]"));
            var numberOfOptions = dropdownOptions.Count();
            var randomIndex = GetRandomNumber(numberOfOptions);

            var randomOption = dropdownOptions[randomIndex];
            randomOption.Click();

            return randomOption.Text;
        }

        private int GetRandomNumber(int numberOfOptions)
        {
            Random rand = new Random();
            return rand.Next(0, numberOfOptions);
        }

        protected void SendKeysToTextarea(By descriptionTextarea, string description)
        {
            WaitForElementToExist(descriptionTextarea);
            Driver.FindElement(descriptionTextarea).FindElement(By.CssSelector("textarea")).SendKeys(description);
        }


        protected void SelectDate(By dateSelector, string date)
        {
            WaitForElementToExist(dateSelector);
            ScrollToElement(dateSelector);
            Driver.FindElement(dateSelector).FindElement(By.CssSelector("label")).Click();
            Driver.FindElement(dateSelector).FindElement(By.CssSelector("input")).Click();
            SelectDateFromDatePicker(date);
        }

        private void ScrollToElement(By dateSelector)
        {
            var element = Driver.FindElement(dateSelector);
            var x = element.Location.X - 400;
            var y = element.Location.Y - 100;

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(" + x + ", " + y + ")");
        }



        private void SelectDateFromDatePicker(string date)
        {
            var currentDate = Convert.ToDateTime(date);

            string day = currentDate.Day.ToString();
            string month = currentDate.ToString("MMM");
            string year = currentDate.Year.ToString();

            SelectYear(year);
            SelectMonth(month);
            SelectDay(day);
        }

        private void SelectDay(string day)
        {
            WaitUntilElementHasText(_dayTableContainer);
            IWebElement monthContainer = Driver.FindElement(_dayTableContainer);
            IList<IWebElement> daysList = monthContainer.FindElements(By.CssSelector("td[class*=dayIsFocused] button"));

            foreach (var item in daysList.Where(
                item => item.Text.ToLower().Contains(day.ToLower())))
            {
                item.Click();
                break;
            }

        }

        private void SelectMonth(string month)
        {
            WaitUntilElementHasText(_monthOptionContainer);
            IWebElement monthContainer = Driver.FindElement(_monthOptionContainer);
            IList<IWebElement> monthList = monthContainer.FindElements(By.CssSelector("button[class*=monthOption]"));

            foreach (var item in monthList.Where(
                item => item.Text.ToLower().Contains(month.ToLower())))
            {
                item.Click();
                break;
            }

        }

        private void SelectYear(string year)
        {
            Driver.FindElement(_monthYearDiv).Click();

            do
            {
                var yearText = GetTextFromElement(_yearDiv);
                var yearNumber = int.Parse(yearText);

                if (yearNumber < int.Parse(year))
                {
                    Driver.FindElement(_nextYearButton).Click();
                }
                else if (yearNumber > int.Parse(year))
                {
                    Driver.FindElement(_previousYearButton).Click();
                }

            } while (!GetTextFromElement(_yearDiv).Equals(year));
        }

        protected bool SelectValueFromSuggestionBox(By fieldContainer, string option)
        {
            WaitUntilElementIsVisible(fieldContainer);
            Driver.FindElement(fieldContainer).FindElement(By.CssSelector("input")).SendKeys(option);

            WaitUntilListHasElements(By.CssSelector("[id*='suggestion-list-id']"));
            IList<IWebElement> suggestionList = Driver.FindElements(By.CssSelector("[id*='suggestion-list-id']"));
            foreach (IWebElement element in suggestionList)
            {
                if (element.Text.ToLower().Contains(option.ToLower()))
                {
                    element.Click();
                    return true;
                }
            }

            return false;
        }

        protected void RemoveSelectedValueInSuggestionField(By fieldContainer)
        {
            WaitUntilElementIsVisible(fieldContainer);
            Driver.FindElement(fieldContainer).FindElement(By.CssSelector("button")).Click();
        }


        protected string SelectRandomOptionFromRadioButtonsList(By selector)
        {
            WaitUntilListHasElements(_radioButtonsListOfOptions);
            var container = Driver.FindElement(selector);
            IList<IWebElement> radioButtonsOptions = container.FindElements(_radioButtonsListOfOptions);

            var randomIndex = Randomize.GenerateNumberWithATopLimit(radioButtonsOptions.Count());
            radioButtonsOptions[randomIndex].Click();

            return radioButtonsOptions[randomIndex].Text;
        }

        protected string SelectRandomOptionFromTableOfRadioButtons(By selector)
        {
            WaitUntilListHasElements(selector);
            IList<IWebElement> optionsList = Driver.FindElements(selector);

            var randomIndex = Randomize.GenerateNumberWithATopLimit(optionsList.Count());
            optionsList[randomIndex].FindElement(By.CssSelector("input")).Click();

            return optionsList[randomIndex].FindElement(By.CssSelector(".table-cell-text")).Text;
        }

        internal bool CheckIfFileHasBeenDownloaded(string fileName)
        {
            var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var filePaths = Directory.GetFiles(path);

            foreach (var name in filePaths)
            {
                if (name.EndsWith(fileName))
                {
                    File.Delete(name);
                    return true;
                }
            }

            return false;
        }

    }
}
