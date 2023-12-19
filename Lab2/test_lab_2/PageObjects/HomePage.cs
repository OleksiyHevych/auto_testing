using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_lab_2.PageObjects
{
    internal class HomePage
    {
        private IWebDriver driver;
        private By customerLoginBtn = By.XPath("//button[contains(text(),'Customer Login')]");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickCustomerLogin()
        {
            driver.FindElement(customerLoginBtn).Click();
        }
    }
}
