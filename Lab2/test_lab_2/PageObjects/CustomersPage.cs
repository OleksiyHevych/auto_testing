using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_lab_2.PageObjects
{
    internal class CustomersPage
    {
        private decimal _balanceBeforeWithdrawal;
        private IWebDriver driver;
        private By lastNameColumnHeader = By.XPath("/html/body/div/div/div[2]/div/div[2]/div/div/table/thead/tr/td[2]/a");

        public CustomersPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickTheWithdrawButton()
        { 
            driver.FindElement(By.XPath("//button[contains(text(),'Withdrawl')]")).Click();
        }
        
        public void RememberTheBalance()
        {
            _balanceBeforeWithdrawal = Convert.ToInt32(driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[2]/strong[2]")).Text);
        }
        public void SendANumberThatIsBiggerThanMyBalance()
        {
            driver.FindElement(By.XPath("//input[@placeholder='amount']")).SendKeys((_balanceBeforeWithdrawal + 1).ToString());
            driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[4]/div/form/button")).Click();
        }
        
        public void CheckErrorMassage()
        {
            Assert.IsNotNull(driver.FindElement(By.CssSelector("span.error")));
        }
        public void InputANumberThatIsSuitableForMyBalance()
        {
            IWebElement input = driver.FindElement(By.XPath("//input[@placeholder='amount']"));
            input.Clear();
            input.SendKeys((_balanceBeforeWithdrawal - 1).ToString());
            driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[4]/div/form/button")).Click();
        }
        
        public void CheckBalance()
        {
            Assert.That(1, Is.EqualTo( Convert.ToInt32(driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/div[2]/strong[2]")).Text)));
        }

    }
}
