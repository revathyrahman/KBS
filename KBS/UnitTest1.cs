using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace MyLCIAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://mylcibeta.lionsclubs.org/");
            driver.FindElement(By.Id("PageContent_Login1_txtUsername")).SendKeys("4141201");
            driver.FindElement(By.Id("PageContent_Login1_txtPassword")).SendKeys("password0");
            driver.FindElement(By.Id("PageContent_Login1_btnSubmit")).Click();

        }
    }
}
