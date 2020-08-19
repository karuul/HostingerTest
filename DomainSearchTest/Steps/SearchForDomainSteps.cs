using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UnitTestProject1.Steps
{
    [Binding]
    public class SearchForDomainfeatureSteps
    {
        private const int timeout = 15; // seconds
        private ChromeDriver browser;

        public SearchForDomainfeatureSteps() => browser = new ChromeDriver();

        [Given(@"I have navigated to Domain-checker page")]
        public void GivenIHaveNavigatedToDomain_CheckerPage()
        {
            browser.Navigate().GoToUrl(Constants.checkerDomain);

            Assert.IsTrue(browser.Title.Contains("Domain Checker"));
        }

        [Given(@"I have entered (.*) in Domain Checker")]
        public void GivenIHaveEnteredValueToDomainChecker(String domainName)
        {
            var searchForm = browser.FindElementByXPath("//*[@id='cart_domain_search_domain']");

            searchForm.SendKeys(domainName);
        }

        [When(@"I try to select domain suffix (.*)")]
        public void WhenITryToSelectDomainSuffix(String suffix)
        {
            browser.FindElementByXPath($"//*[@class='ng-binding'][contains(text(),'{suffix}')]").Click();

            var submitButton = browser.FindElementByXPath("//input[@type='submit']");
            submitButton.Click();
        }

        [When(@"I try to add domain to cart")]
        public void WhenITryToAddDomainToCart()
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='/cart'][contains(text(),'Continue')]")));

            var buttonsList = browser.FindElements(By.XPath("//a[@href='/cart'][contains(text(),'Continue')]"));
            buttonsList[0].Click();
        }

        [When(@"I try to checkout")]
        public void WhenITryToCheckout()
        {
            var checkoutButton = browser.FindElementByXPath("//*[@id='cart-checkout-btn']");
            checkoutButton.Click();
        }

        [When(@"fill in test users credentials")]
        public void WhenFillInCredentials()
        {
            var nameField = browser.FindElementByXPath("//*[@class='form-group']//input[@type='text']");
            var emailField = browser.FindElementByXPath("//*[@class='form-group']//input[@type='email']");
            var passwordField = browser.FindElementByXPath("//*[@class='form-group']//input[@type='password']");

            nameField.SendKeys(Constants.username);
            emailField.SendKeys(Constants.email);
            passwordField.SendKeys(Constants.password);

            var signUpButton = browser.FindElementByXPath("//*[@id='hgr-cart-sign_up_checkout_button']");
            signUpButton.Click();
        }

        [Then(@"I check if domain is available")]
        public void ThenICheckIfDomainIsAvailable()
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".domain-checker-new-results-box__button")));

            var addToBasketButton = browser.FindElementByCssSelector(".domain-checker-new-results-box__button");
            addToBasketButton.Click();

            new WebDriverWait(browser, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".w-100.ng-scope.available .domain-checker-new-results-box__button")));
        }

        [Then(@"I check if domain (.*) with suffix (.*) is added to cart")]
        public void ThenICheckIfDomainIsAddedToCart(String domain, String suffix)
        {
            Assert.IsTrue(browser.Title.Contains("Shopping Cart"));

            browser.FindElementByXPath($"//*[@class='value ng-binding' and contains(text(),'{domain.ToLower()}{suffix.ToLower()}')]");
        }

        [Then(@"I should be at Payment Method page")]
        public void ThenIShouldBeAtPaymentMethodPage()
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("//*[@id='order-summary-block-right']")));

            Assert.IsTrue(browser.Title.Contains("Receipt Payment"));
        }

        public void Dispose()
        {
            if (browser != null)
            {
                browser.Dispose();
                browser = null;
            }
        }
    }
}
