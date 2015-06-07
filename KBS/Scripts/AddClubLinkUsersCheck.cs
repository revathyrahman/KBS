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
            ExcelReporterAuthorization authorizationReport = new ExcelReporterAuthorization();
            List<List<String>> data = dataInputProvider.GetInputData("Authorization");

            UtilityMethods utilityMethods = new UtilityMethods("AddClubLinkUsersCheck","FAILED");

            utilityMethods.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {
                
                try
                {
                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                   
                    //Verify "Add Club" link exists for this user
                    Boolean status = utilityMethods.VerifyAddClubLinkExists("hlAddClub");

                    if (status.Equals(true))
                    {
                        authorizationReport.ReportStep("User is Authorized User", "SUCCESS");
                        //Click Add Club link
                        utilityMethods.ClickById("hlAddClub");
                    }
                    else
                    {
                        authorizationReport.ReportStep("User is not Authorized User", "SUCCESS");
                       
                    }

                    
                                        
                    //Logout from the application
                    utilityMethods.LogoutMyLCI();
                }
                catch (WebDriverException e)
                {
                    e.StackTrace.ToString();
                }

                
            }
            // Close the browser and write the report into Excelsheet
            utilityMethods.CloseApplicationForAuthorizationUsers();
        }
        
    }
}
