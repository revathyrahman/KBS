using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class DiscontinueClub
    {
        
        [TestMethod]
        public void DiscontinueClubDGAuth()
        {
            bool browserStatus;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login");
            List<List<string>> lciLoginData = dataInputProvider.GetInputData("LCILogin");
            string ClubName = null;
            ExcelReporter excelReporter = new ExcelReporter();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");

            UtilityMethods utilityMethods = new UtilityMethods("DiscontinueClub", Properties["ScreenshotCaptureFlag"]);

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);
            
            if(browserStatus==false)
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
                        utilityMethods.ClickById(PageObjects.idAddClub);

                        ClubName = utilityMethods.AddClubFormEntry();

                        utilityMethods.MoveClubtoNextStatus("Pending DG/CL Authorization");

                        utilityMethods.DiscontinueClub(ClubName);

                        utilityMethods.ContinueClub(ClubName);

<<<<<<< HEAD
                        utilityMethods.LogoutMyLCI();
                    }
=======
                   // utilityMethods.FindClubs();
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                    for (int i = 0; i < data.Count; i++)
                    {
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                        utilityMethods.FindClubs();

                        utilityMethods.FindDesiredClub("Pending DG/CL Authorization");


                        utilityMethods.ViewApplication(ClubName);

                        utilityMethods.DiscontinueClub(ClubName);

                        utilityMethods.VerifyButtonExists("Add Comments", "btnNewComment");

                        utilityMethods.VerifyTextDisplay("//div[@id='divNewClubApplication']/div[1]/div/div[2]", "The Club Application has been Discontinued");

<<<<<<< HEAD
                        utilityMethods.LogoutMyLCI();
=======
                       // utilityMethods.FindClubs();
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                        for (int j = 0; j < lciLoginData.Count; j++)
                        {

                            utilityMethods.LoginMyLCI(lciLoginData[j][0], lciLoginData[j][1], lciLoginData[j][2]);

                            utilityMethods.FindClubs();

                            utilityMethods.FindDesiredClub("All Discontinued");

                            utilityMethods.ViewApplication(ClubName);

                            utilityMethods.ContinueClub(ClubName);

                            utilityMethods.VerifyTextDisplay(PageObjects.xpathMsgClubConfirmation, "pending District Governor authorization");

                            utilityMethods.LogoutMyLCI();
                        }
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
                                
                // close application
                utilityMethods.CloseApplication();
            }
            excelReporter.FlushWorkbook("DiscontinueClub");
        }
       
        
    }
}
    

          