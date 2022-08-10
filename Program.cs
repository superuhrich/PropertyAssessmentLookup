// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PropertyAssessmentLookup.Models;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.Collections.Immutable;
using System.Globalization;
using OpenQA.Selenium.DevTools.V102.Runtime;
using OpenQA.Selenium.Support.UI;


//Get the data from the CSV
var list = PropertyAssessmentLookup.CsvUtils.GetAssessmentData();

var completedList = new List<PropertyAssessmentLookup.Models.CsvModels.AssessedPropertyValues>();

new DriverManager().SetUpDriver(new ChromeConfig());
var options = new ChromeOptions();
var driver = new ChromeDriver(options);


foreach (var property in list) {

    driver.Navigate().GoToUrl("https://honestdoor.com");
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

    var address = $"{property.Address}, Calgary AB";

    driver.FindElement(By.ClassName("css-fjw8c7")).SendKeys(address);
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);

    driver.FindElement(By.ClassName("css-1kse7ka")).Click();

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

    // Wait until the Assessments details are there
    wait.Until(e => e.FindElements(By.XPath("*//*[contains(text(), 'City Assessments')]")));

    var dataMine = driver.FindElements(By.ClassName("css-23jksx"));

    var honestDoorValue = dataMine[0].Text;
    var cityAssessedValue = dataMine[1].Text;

    var assessedProperty = new CsvModels.AssessedPropertyValues(property.Address, property.Bd, property.FB, property.HB,
        property.SqFt, property.Sold_Price, property.Sold_Date, honestDoorValue, cityAssessedValue);
    
    completedList.Add(assessedProperty);


}

PropertyAssessmentLookup.CsvUtils.WriteAssessmentData(completedList);

Console.WriteLine("Complete");










Console.WriteLine("Hello, World!");