using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace LCI.QualityTools.BrowserTests.MyLCI
{
    //using NUnit.Framework;

    [TestClass]
    public class DiscontinueClub
    {
        bool browserStatus;

        string clubName = null;
        ExcelReporter excelReporter = new ExcelReporter();
        static string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");
        UtilityMethods utilityMethods = new UtilityMethods("DiscontinueClubAll", Properties["ScreenshotCaptureFlag"]);
        //[Test]
        [TestMethod]

        public void DiscontinueClubAllStatus()
        {
            clubName = DiscontinuePendingSubmission();
            DiscontinueClubAtStatus(clubName, "Pending DG/CL Authorization");
            DiscontinueClubAtStatus(clubName, "Pending LCI Authorization");
        }
        public string DiscontinuePendingSubmission()
        {
            UtilityMethods utilityMethods = new UtilityMethods("DiscontinueClubAtPendingSubmission", Properties["ScreenshotCaptureFlag"]);
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<string>> lciLoginData = dataInputProvider.GetInputData("LCILogin");

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);

            if (browserStatus == false)
            {
                excelReporter.ReportStep("This browser type is not supported", "FAILURE");
            }
            else
            {
                utilityMethods.InvokeApplication(Properties["ApplicationURL"]);

                try
                {
                    for (int i = 0; i < lciLoginData.Count; i++)
                    {

                        // Call the login method and to verify the home page is displayed
                        utilityMethods.LoginMyLCI(lciLoginData[i][0], lciLoginData[i][1], lciLoginData[i][2]);

                        //Verify "Add Club" link exists for this user
                        Boolean addClubLinkstatus = utilityMethods.VerifyAddClubLinkExists("hlAddClub");

                        //Click Add Club link
                        utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");

                        //Create a new club
                        clubName = utilityMethods.AddClubFormEntry();

                        //Move club to Pending Submission status
                        utilityMethods.MoveClubtoNextStatus("Pending Submission");

                        //Discontinue Club 
                        utilityMethods.DiscontinueClub(clubName);

                        //Continue Club
                        utilityMethods.ContinueClub(clubName);

                        //Logout from the application
                        utilityMethods.LogoutMyLCI();
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
                //Close the application and browser
                utilityMethods.CloseApplication();
            }
            return clubName;    
        }

        //Function to Discontinue club at different status        
        public static void DiscontinueClubAtStatus(string clubName, string clubStatus)
        {
            bool browserStatus = true;
            ExcelReporter excelReporter = new ExcelReporter();
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LoginDiscontinueClub");
            List<List<string>> lciLoginData = dataInputProvider.GetInputData("LCILogin");
            UtilityMethods utilityMethods = new UtilityMethods("DiscontinueClubAtStatus", Properties["ScreenshotCaptureFlag"]);

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);

            if (browserStatus == false)
            {
                excelReporter.ReportStep("This browser type is not supported", "FAILURE");
            }
            else
            {
                utilityMethods.InvokeApplication(Properties["ApplicationURL"]);
                try
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        //Login to MyLCI Application
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                        //Find the clubs
                        utilityMethods.FindClubs();

                        //Find based on Club status filter 
                        utilityMethods.FindDesiredClub(clubStatus);

                        //View the specific club application
                        utilityMethods.ViewApplication(clubName);

                        //Discontinue Club
                        utilityMethods.DiscontinueClub(clubName);

                        //Verify Add Comments button exist
                        utilityMethods.VerifyButtonExists("Add Comments", PageObjects.idAddCommentbtn);

                        //Verify the current Club Status message is displayed
                        utilityMethods.VerifyTextDisplayById(PageObjects.lblTxtDisplayClubStatus, "The Club Application has been Discontinued");

                        //Logout from application
                        utilityMethods.LogoutMyLCI();

                        for (int j = 0; j < lciLoginData.Count; j++)
                        {
                            utilityMethods.LoginMyLCI(lciLoginData[j][0], lciLoginData[j][1], lciLoginData[j][2]);

                            //Find the clubs
                            utilityMethods.FindClubs();

                            ////Find clubs based on Club status filter  
                            utilityMethods.FindDesiredClub("All Discontinued");

                            //Select a specific club
                            utilityMethods.ViewApplication(clubName);

                            //Continue Club
                            utilityMethods.ContinueClub(clubName);

                            //Verify the confirmation message
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathMsgClubConfirmation, ("pending" + clubStatus));

                            //Logout from application
                            utilityMethods.LogoutMyLCI();
                        }
                    }

                    if (clubStatus == "DG/CL Authorization")
                    {
                        //Move to next status
                        utilityMethods.MoveClubtoNextStatus("LCI Authorization");
                    }
                    
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }

                // close application
                utilityMethods.CloseApplication();
            }            
        }
    }
}
    

          