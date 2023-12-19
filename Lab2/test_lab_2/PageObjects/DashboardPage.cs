using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_lab_2.PageObjects
{
    internal class DashboardPage
    {
        private IWebDriver driver;
        private By customersMenuItem = By.XPath("//button[contains(text(),'Customers')]");

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickOnCustomers()
        {
            driver.FindElement(customersMenuItem).Click();
        }
    }
}
