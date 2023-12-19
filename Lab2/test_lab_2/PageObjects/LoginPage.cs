using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_lab_2.PageObjects
{
    internal class LoginPage
    {
        private IWebDriver driver;
        private By managerLoginBtn = By.XPath("//button[contains(text(),'Bank Manager Login')]");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void DoLoginByName(string name)
        {
            var selectElement = new SelectElement(driver.FindElement(By.Id("userSelect")));

            // Вибираємо опцію за видимим текстом
            selectElement.SelectByText(name); Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/button")).Click();
        }
    }
}
