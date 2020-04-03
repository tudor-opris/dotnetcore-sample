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

        private ITestWebDriver _testWebDriver;
        protected IWebDriver Driver => _testWebDriver?.Driver;

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
            InitializeDriver();
            _guid = Guid.NewGuid().ToString().Substring(0, 25);
            _loginSteps = new LoginSteps(Driver);
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
        protected IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    var builder = new ContainerBuilder();
                    var assembly = Assembly.GetExecutingAssembly();

                    // Repositories
                    builder.RegisterAssemblyTypes(assembly)
                        .Where(t => t.Name.EndsWith("TestDriver", StringComparison.OrdinalIgnoreCase))
                        .Named<ITestWebDriver>(t => t.Name.ToUpperInvariant())
                        .InstancePerDependency();

                    _container = builder.Build();
                }

                return _container;
            }
        }



        public void InitializeDriver()
        {
            _testWebDriver = Container.ResolveNamed<ITestWebDriver>((Constants.BROWSER + "TestDriver").ToUpperInvariant());

            //configure driver properties
            //Driver.Manage().Window.Size = new Size(1024, 768);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Constants.LOAD_TIME_SECONDS);
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

            _testWebDriver.Driver.Quit();
        }

    }

}