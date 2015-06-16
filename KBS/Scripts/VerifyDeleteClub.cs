
﻿using System;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace LCI.QualityTools.BrowserTests.MyLCI
{
    [TestClass]
    public class VerifyDeleteClub
    {
        IWebDriver driver;

        [TestMethod]
        public void DeleteClub()
        {
            bool browserStatus;
            String ClubName = null;
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LoginDeleteClub");
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
                            utilityMethods.ClickById(PageObjects.csmnuLinkDistricts,"Districts Menu");
                            
                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.cssubMenuLinkClubs, "Clubs Sub Menu");

                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub,"Add Club");
                            //Create new Club
                            ClubName = utilityMethods.AddNewclub();
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                           
                            // Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave,"Save");

                            //Logout from the Application
                            utilityMethods.LogoutMyLCI();
                        }
                        //Login as "DG" and verify the Delete functionality is not available
                        if (data[i][0].Equals("DG"))
                        {
                            //Call the login method and to verify the home page is displayed
                            utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                            //Click My Districts Selection
                            utilityMethods.ClickById(PageObjects.mnuLinkDistricts,"Districts Menu");

                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.subMenuLinkClubs,"Clubs Sub Menu");

                            //Navigating to the Club status
                            utilityMethods.FindDesiredClub("All Pending");

                            //Navigating to the Desired Application
                            utilityMethods.ViewApplication(ClubName);

                            //Verify the invisibility of delete button
                            utilityMethods.VerifyInvisibleButton("Save Button", PageObjects.idBtnSave, "Cancel Button", PageObjects.idBtnCancel);

                            //Logout from the Application
                            utilityMethods.LogoutMyLCI();
                        }
                        //Login as "LCI" and verify the Delete functionality of the Club created by Club Secretary
                        if (data[i][0].Equals("LCI"))
                        {

                            //Call the login method and to verify the home page is displayed
                            utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                            //Click My Districts Selection
                            utilityMethods.ClickById(PageObjects.mnuLinkDistricts, "Districts Menu");

                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.subMenuLinkClubs, "Clubs Sub Menu");

                            //Navigating to the Club status
                            utilityMethods.FindDesiredClub("All Pending");

                            //Navigating to the Desired Application
                            utilityMethods.ViewApplication(ClubName);

                            //Click Delete button
                            utilityMethods.ClickById(PageObjects.idBtnDelete, "Delete");
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Verify Districts Page is displayed
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathLabelCaption, "District Clubs");
                             //Logout from the Application
                            utilityMethods.LogoutMyLCI();
                        } 
                                               
                        }
                    // Close the  Application
                    utilityMethods.CloseApplication();
                    }
                
           catch (Exception e)
             {
                    e.StackTrace.ToString();
             }
            }
        }
    }
}

