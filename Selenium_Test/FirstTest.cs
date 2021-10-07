using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium_Test
{
    class FirstTest
    {
        private IWebDriver driver;
        private string pageAddress = "https://www.vlaanderen.be/";

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void GoToPage()
        {
            GoToWebPage();            
        }
        [Test]
        public void FindLink()
        {
            string xPathNext = "//a[contains(text(), 'Volgende')]";
            bool linkFound = false;

            GoToWebPage();
            Thread.Sleep(2000); //site is wat traag waardoor de elementen niet altijd gevonden worden
            IWebElement elem = driver.FindElement(By.Id("vlw-search"));
            elem.SendKeys("Burgerprofiel");
            elem.Submit();            
            Thread.Sleep(1500);                       

            while (ElementExist(By.XPath(xPathNext)) && !linkFound)
            {                
                if (ElementExist(By.XPath("//a[contains(., 'COVID-certificaat - het vaccinatiecertificaat')]")))
                {
                    linkFound = true;
                    driver.FindElement(
                        By.XPath("//a[contains(., 'COVID-certificaat - het vaccinatiecertificaat')]"))
                        .Click();                                        
                }
                else
                {
                    driver.FindElement(By.XPath(xPathNext)).Click();
                    Thread.Sleep(1500);
                }
                
            }            
            Assert.IsTrue(ElementExist(By.XPath("//a[contains(@href, 'reopen.europa.eu')]")));
            
        }

        [TearDown]
        public void CloseBrowser()
        {            
            driver.Close();
        }
        private void GoToWebPage()
        {
            driver.Navigate().GoToUrl(pageAddress);
            driver.Manage().Window.Maximize();
        }
        private bool ElementExist(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;               
            }
        }
    }
}
