// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PropertyAssessmentLookup.Models;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.Collections.Immutable;
using System.Globalization;
using CsvHelper;
using OpenQA.Selenium.DevTools.V102.Runtime;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;


//Get the data from the CSV
var list = PropertyAssessmentLookup.CsvUtils.GetAssessmentData();

var completedList = new List<PropertyAssessmentLookup.Models.CsvModels.AssessedPropertyValues>();


// Selenium Drivers
new DriverManager().SetUpDriver(new EdgeConfig());
var options = new EdgeOptions();
var driver = new EdgeDriver(options);

// CSV Writer Drivers
//var writer = new StreamWriter(@"C:\Repos\CompletedAssessmentValues.csv");
//var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

//csv.WriteHeader<CsvModels.AssessedPropertyValues>();
//csv.NextRecord();


for(var i = 0; i<38; i++) {

    driver.Navigate().GoToUrl("https://honestdoor.com");
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

    var address = $"{list[i].Address}, Calgary AB";

    driver.FindElement(By.ClassName("css-fjw8c7")).SendKeys(address);
    //driver.FindElement(By.ClassName("css-fjw8c7")).SendKeys("7715 36 Avenue NW, Calgary AB");
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);

    driver.FindElement(By.ClassName("css-1kse7ka")).Click();

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

    try {
        var isModal = driver.FindElement(By.ClassName("chakra-modal__header"));

        if (isModal != null) {
            driver.FindElement(By.ClassName("css-1dxrdkq")).Click();
            driver.FindElement(By.ClassName("css-1nyjeo3")).Click();
        }

    }
    catch {
        
    }

    

    // Wait until the Assessments details are there
    wait.Until(e => e.FindElements(By.XPath("*//*[contains(text(), 'City Assessments')]")));

    var dataMine = driver.FindElements(By.ClassName("css-23jksx"));

    var honestDoorValue = dataMine[0].Text;
    var cityAssessedValue = dataMine[1].Text;

    var assessedProperty = new CsvModels.AssessedPropertyValues(list[i].Address, list[i].Bd, list[i].FB, list[i].HB,
        list[i].SqFt, list[i].Sold_Price, list[i].Sold_Date, honestDoorValue, cityAssessedValue);
    
    //csv.WriteRecord(assessedProperty);
    //csv.NextRecord();

    // if (i % 5 == 0) {
    //     driver.Close();
    //     driver.Quit();
    //     driver.Dispose();
    //     driver = new EdgeDriver();
    // }
    
    completedList.Add(assessedProperty);


}

PropertyAssessmentLookup.CsvUtils.WriteAssessmentData(completedList);

Console.WriteLine("Complete");










Console.WriteLine("Hello, World!");