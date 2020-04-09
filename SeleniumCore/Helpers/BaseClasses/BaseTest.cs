using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Autofac;
using System;
using System.Drawing;
using System.Reflection;
using SeleniumCore.Steps;
using SeleniumCore.Helpers.Interfaces;
using System.IO;

namespace SeleniumCore.Helpers.BaseClasses
{
    public class BaseTest
    {

        private IWebDriverResolver _webDriverResolver;
        protected IWebDriver Driver => _webDriverResolver?.Driver;

        private IContainer _container;

        protected DateTime CurrentDate = DateTime.Now.Date;

        protected string DateFormat = "d MMM yyyy";

        public string _guid;

        public string Browser;

        #region define steps
        protected LoginSteps _loginSteps;
        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            BuildContainer();
            InitializeDriver();
            ResolveSteps();
            _guid = Guid.NewGuid().ToString().Substring(0, 25);
        }

        private void BuildContainer()
        {

            if (_container == null)
            {
                var builder = new ContainerBuilder();
                var assembly = Assembly.GetExecutingAssembly();

                // Repositories
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("TestDriver", StringComparison.OrdinalIgnoreCase))
                    .Named<IWebDriverResolver>(t => t.Name.ToUpperInvariant())
                    .SingleInstance();

                builder.RegisterType<WebDriverResolver>().As<IWebDriverResolver>();

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Page", StringComparison.OrdinalIgnoreCase))
                    .SingleInstance();

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Steps", StringComparison.OrdinalIgnoreCase))
                    .SingleInstance();

                _container = builder.Build();
            }

        }
        public void InitializeDriver()
        {
            _webDriverResolver = _container.Resolve<IWebDriverResolver>();

            //configure driver properties
            //Driver.Manage().Window.Size = new Size(1024, 768);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Constants.LOAD_TIME_SECONDS);
        }

        private void ResolveSteps()
        {
            _loginSteps = _container.Resolve<LoginSteps>();
        }

        
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var testClassName = testContextInstance.FullyQualifiedTestClassName;
            if (testContextInstance.CurrentTestOutcome.ToString().Equals("Failed"))
            {
                var guid = Guid.NewGuid().ToString();
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                string path = Directory.GetCurrentDirectory() + testClassName + DateTime.Now.ToFileTime() + ".png";
                ss.SaveAsFile(path);
                TestContext.AddResultFile(path);
            }

            Driver.Quit();
        }

    }

}