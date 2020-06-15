using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebAplication3.Test.Pruebas_Selenium
{
    [TestFixture]
    class Category
    {
        [Test]
        public void testCreateCategory() {
            IWebDriver chrome = new InternetExplorerDriver();
            chrome.Url = "https://localhost:44392/";

            Thread.Sleep(6000);
            var ButtonArticulo = chrome.FindElement(By.CssSelector("#btnGroupDrop4"));
            ButtonArticulo.Click();

            var ButtonArticulos = chrome.FindElement(By.CssSelector("#indexcategory"));
            ButtonArticulos.Click();
           // chrome.Close();
        }
    }
}
