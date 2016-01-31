using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace selenium_test_01
{
    class Program
    {

 
        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();           
            StringBuilder verificationErrors = new StringBuilder();
            
            driver.Navigate().GoToUrl("http://demo.opencart.com/");
            driver.FindElement(By.XPath("(//button[@type='button'])[11]")).Click();
            driver.FindElement(By.XPath("//form[@id='currency']/div/button")).Click();
            driver.FindElement(By.Name("GBP")).Click();
            driver.FindElement(By.Name("search")).Clear();
            driver.FindElement(By.Name("search")).SendKeys("ipod");
            driver.FindElement(By.XPath(".//*[@id='search']/span/button")).Click();

            int NumOfElem = 0;
            var result = driver.FindElements(By.XPath("//*[contains(@data-original-title, 'Compare this Product')] "));
            foreach (IWebElement element in result)
            {
                element.Click();
                NumOfElem += 1;
            }

            driver.FindElement(By.XPath(".//*[@id='compare-total']")).Click();

           

            for (int column = 1; column <= NumOfElem; column++)
            {

                var element = driver.FindElement(By.XPath(".//*[@id='content']/table/tbody[1]/tr[6]/td["+column+"]")).Text.ToString();
                if (element == "Out Of Stock") 
                {
                    driver.FindElement(By.XPath(".//*[@id='content']/table/tbody[2]/tr/td[" + column + "]/a")).Click();
                    NumOfElem = NumOfElem - 1;
                    column = column - 1;
                }
            }
            Random rnumber = new Random();
            int buyproduct = rnumber.Next(2, NumOfElem + 1);

          
            var price = driver.FindElement(By.XPath(".//*[@id='content']/table/tbody[1]/tr[3]/td["+buyproduct+"]")).Text.ToString();
            driver.FindElement(By.XPath(".//*[@id='content']/table/tbody[2]/tr/td[" + buyproduct + "]/input")).Click();

            driver.FindElement(By.XPath("html/body/div[2]/div[1]/a[2]")).Click();
            
            var totalprice = driver.FindElement(By.XPath(".//*[@id='content']/form/div/table/tbody/tr/td[6]")).Text.ToString();
        }
    }
}
