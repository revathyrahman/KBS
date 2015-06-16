using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace LCI.QualityTools.BrowserTests.MyLCI
{
    [TestClass]
    public class TestSample
    {
        [TestMethod]
        public void TestMethod1()
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

                    
                        //Call the login method and to verify the home page is displayed
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                        //Verify "Add Club" link exists for this user

                    

                       // utilityMethods.ClickByName("a_3_1_28", "N);


                       // utilityMethods.ClickById("a_3_2_42");
                       // utilityMethods.LinkClickByText("Add Club");

                       // utilityMethods.ClickByCSS("#imgClubNamingHelp > #Image1");

                    

                        utilityMethods.FindDesiredClub("All Pending");

                        utilityMethods.ViewApplication("Club875");
                    
                }
            }
        }

    }
}
