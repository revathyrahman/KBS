using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class Deletebtnchk
    {
        IWebDriver driver;
        [TestMethod]
        public void DeleteButtonChk()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login-2data");
            utilityMethods = new UtilityMethods("Deletevisible_839", "FAILED");
            utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {

                if (data[i][0].Equals("CS"))
                {
                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                    //Click My Districts Selection
                    utilityMethods.ClickById(PageObjects.csmnuLinkDistricts,"District Menu");
                    //Click Clubs link
                    utilityMethods.ClickById(PageObjects.cssubMenuLinkClubs,"Clubs Sub menu");
                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName, "Alphabet-Club","Club Name");
                    // Click Save button
                    utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                }
                if (data[i][0].Equals("DG"))
                {
                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                    //Click My Districts Selection
                    utilityMethods.ClickById(PageObjects.mnuLinkDistricts,"District Menu");
                    //Click Clubs link
                    utilityMethods.ClickById(PageObjects.subMenuLinkClubs,"Clubs Sub menu");
                    //Navigating to the Club status
                    utilityMethods.FindDesiredClub("All Pending");
                    //Navigating to the Desired Application
                    utilityMethods.ViewApplication("Alphabet-Club");
                    //Verify the invisibility of delete
                    utilityMethods.VerifyInvisibleButton("Save", PageObjects.savebtn, "Cancel", PageObjects.cancelbtn);
                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                    // Close the  Application
                    utilityMethods.CloseApplication();

                }
            }
        }
    }
}


