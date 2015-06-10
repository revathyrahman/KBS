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
            ExcelReporter excelreport = new ExcelReporter();
            List<List<String>> data = dataInputProvider.GetInputData("Authorization");
            ExcelReporterAuthorization authorizationreport = new ExcelReporterAuthorization();

            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");

<<<<<<< HEAD
            UtilityMethods utilityMethods = new UtilityMethods("AddClubLinkUsersCheck", Properties["Report"]);
            utilityMethods.InvokeApplication(Properties["Browser"], "http://mylcibeta.lionsclubs.org/");
=======
            UtilityMethods utilityMethods = new UtilityMethods("AddClubLinkUsersCheck",Properties["ScreenshotCaptureFlag"]);
            
            utilityMethods.InvokeApplication(Properties["Browser"], Properties["ApplicationURL"]);
>>>>>>> f4a832810035900c019cf823b84c313a3eb18a0b

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
                        authorizationreport.ReportStep("User is Authorized User", "SUCCESS");
                        //Click Add Club link
                        utilityMethods.ClickById("hlAddClub");
                    }
                    else
                    {
                        authorizationreport.ReportStep("User is not Authorized User", "SUCCESS");
                       
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
            authorizationreport.FlushWorkbook("Authorization-Run");
            utilityMethods.CloseApplication("Authorization");
            
        }
        
    }
}
