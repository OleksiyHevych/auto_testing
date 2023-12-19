using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using test_lab_2.PageObjects;
using OpenQA.Selenium.DevTools;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OpenQA.Selenium.DevTools.V116.Database;
using System.Runtime.Intrinsics.X86;

namespace test_lab_2.Steps
{
    [Binding]
    public class BankingSteps
    {
        IWebDriver driver = new ChromeDriver();
        HomePage homePage;
        LoginPage loginPage;
        DashboardPage dashboardPage;
        CustomersPage customersPage;

        [Given(@"I am on the Home page")]
        public void GivenIAmOnTheHomePage()
        {
            driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/");
            homePage = new HomePage(driver);
        }

        [When(@"I click on the Customer Login button")]
        public void WhenIClickOnTheCustomerLoginButton()
        {
            Thread.Sleep(1000);
            homePage.ClickCustomerLogin();
            loginPage = new LoginPage(driver);
        }

        [Then(@"I should see the Customer Login page")]
        public void ThenIShouldSeeTheCustomerLoginPage()
        {
            Thread.Sleep(1000);
            Assert.IsNotNull(loginPage);
        }

        [When(@"I log in with the name ""(.*)""")]
        public void WhenIDoLogInWithTheName(string customer_name)
        {
            Thread.Sleep(1000);
            loginPage.DoLoginByName(customer_name);
            customersPage = new CustomersPage(driver);
        }

        [Then(@"I should see the Customer Dashboard")]
        public void ThenIShouldSeeTheCustomerDashboard()
        {
            Thread.Sleep(1000);
            Assert.IsNotNull(customersPage);
        }

        [Then(@"I should see customer balance")]
        public void IShouldSeeCustomerBalance()
        {
            Thread.Sleep(1000);
            customersPage.RememberTheBalance();
        }

        [When(@"I click the Withdraw button")]
        public void WhenIClickTheWithdrawButton()
        {
            Thread.Sleep(1000);
            customersPage.ClickTheWithdrawButton();
        }

        [When(@"I send a number that is bigger than my balance")]
        public void WhenISendANumberThatIsBiggerThanMyBalance()
        {
            Thread.Sleep(1000);
            customersPage.SendANumberThatIsBiggerThanMyBalance();
        }

        [Then(@"I should see the error message")]
        public void WhenIShouldSeeTheErrorMessage()
        {
            Thread.Sleep(1000);
            customersPage.CheckErrorMassage();
        }

        [When(@"I input a number that is suitable for my balance")]
        public void WhenIInputANumberThatIsSuitableForMyBalance()
        {
            Thread.Sleep(1000);
            customersPage.InputANumberThatIsSuitableForMyBalance();
        }
        [Then(@"I should see that my balance has been reduced by the entered amount")]
        public void WhenIShouldSeeThatMyBalanceHasBeenReducedByTheEnteredAmount()
        {
            Thread.Sleep(1000);
            customersPage.CheckBalance();
        }
        [After]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
