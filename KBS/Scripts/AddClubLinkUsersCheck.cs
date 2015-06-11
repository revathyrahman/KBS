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
            bool browserStatus;
            DataInputProvider dataInputProvider = new DataInputProvider();
            ExcelReporter excelreport = new ExcelReporter();
            List<List<String>> data = dataInputProvider.GetInputData("Authorization");
            ExcelReporterAuthorization authorizationReport = new ExcelReporterAuthorization();

            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");

            UtilityMethods utilityMethods = new UtilityMethods("AddClubLinkUsersCheck", Properties["ScreenshotCaptureFlag"]);

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);
            utilityMethods.InvokeApplication(Properties["ApplicationURL"]);
            if (browserStatus == false)
            {
                authorizationReport.ReportStep("This browser type is not supported", "FAILURE");
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {

                    try
                    {
                        //Call the login method and to verify the home page is displayed
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                        //Verify "Add Club" link exists for this user
                        Boolean status = utilityMethods.VerifyAddClubLinkExists(PageObjects.linkTextAddClub);

                        if (status.Equals(true))
                        {
                            authorizationReport.ReportStep("User is Authorized User", "SUCCESS");
                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub);
                            authorizationReport.ReportStep("Add club link exists", "PASS");
                        }
                        //Validation for an unauthorized User the "Add Club" not visible
                        else
                        {
                            authorizationReport.ReportStep("User is not an Authorized User", "SUCCESS");
                            authorizationReport.ReportStep("Add club link does not exist", "PASS");
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
                authorizationReport.FlushWorkbook("Authorization-Run");
                utilityMethods.CloseApplication("Authorization");

            }

        }
    }
}