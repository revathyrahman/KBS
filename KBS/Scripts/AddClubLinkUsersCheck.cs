using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class AddClubLinkUsersCheck
    {
        //This method enters all the field values and saves a New Club Application
        [TestMethod]
        public void AddClubLinkCheck()
        {
            DataInputProvider dataInputProvider = new DataInputProvider();
            ExcelReporter excelReporter = new ExcelReporter();
            List<List<String>> data = dataInputProvider.GetInputData("Login");
            UtilityMethods utilityMethods;
            for (int i = 0; i < data.Count; i++)
            {
                utilityMethods= new UtilityMethods("AddClubLinkUsersCheck", i);
                utilityMethods.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");
                        
                try
                {
                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0],data[i][1],data[i][2]);

                    //Click on My Districts Link in the home page
                    utilityMethods.ClickById("a_3_1_28");

                    //Click on Clubs Link under My Districts Menu List
                    utilityMethods.ClickById("a_3_2_40");

                    //Verify "Add Club" link exists for this user
                    Boolean status=utilityMethods.VerifyAddClubLinkExists("hlAddClub");

                    if (status.Equals(true))
                    {
                        excelReporter.ReportStep("User is Authorized User", "SUCCESS");
                    }
                    else
                    {
                        excelReporter.ReportStep("User is not Authorized User", "FAILURE");
                    }

                    //Click Add Club link
                    utilityMethods.ClickById("hlAddClub");
                                        
                    //Logout from the application
                    utilityMethods.LogoutMyLCI();
                }
                catch (WebDriverException e)
                {
                    e.StackTrace.ToString();
                }

                // Close the browser and write the report into Excelsheet
                utilityMethods.CloseApplication();
            }
        }
    }
}
