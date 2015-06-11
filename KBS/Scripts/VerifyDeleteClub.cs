using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class VerifyDeleteClub
    {
        IWebDriver driver;

        [TestMethod]
        public void DeleteClub()
        {
            bool browserStatus;
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login-2data");
            ExcelReporter excelReporter = new ExcelReporter();

            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");

            utilityMethods = new UtilityMethods("Deletevisible_839", Properties["ScreenshotCaptureFlag"]);

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);
            utilityMethods.InvokeApplication(Properties["ApplicationURL"]);

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
                        if (data[i][0].Equals("CS"))
                        {
                            //Call the login method and to verify the home page is displayed
                            utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                            //Click My Districts Selection
                            utilityMethods.ClickById(PageObjects.csmnuLinkDistricts);

                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.cssubMenuLinkClubs);

                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.linkTextAddClub);

                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, "Alphabet-Club");

                            // Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave);

                            //Logout from the Application
                            utilityMethods.LogoutMyLCI();
                        }
                        if (data[i][0].Equals("DG"))
                        {
                            //Call the login method and to verify the home page is displayed
                            utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                            //Click My Districts Selection
                            utilityMethods.ClickById(PageObjects.mnuLinkDistricts);

                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.subMenuLinkClubs);

                            //Navigating to the Club status
                            utilityMethods.FindDesiredClub("All Pending");

                            //Navigating to the Desired Application
                            utilityMethods.ViewApplication("Alphabet-Club");

                            //Verify the invisibility of delete button
                            utilityMethods.VerifyInvisibleButton("Save Button", PageObjects.idBtnSave, "Cancel Button", PageObjects.idBtnCancel);

                            //Logout from the Application
                            utilityMethods.LogoutMyLCI();

                            // Close the  Application
                            utilityMethods.CloseApplication();
                        }
                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
            }
        }
    }
}

