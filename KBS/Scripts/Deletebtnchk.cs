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
                    utilityMethods.ClickById("a_3_1_30");
                    //Click Clubs link
                    utilityMethods.ClickById("a_3_2_35");
                    //Click Add Club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "Club-Sec-chk2");
                    // Click Save button
                    utilityMethods.ClickById("btnSave");
                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                }
                    if (data[i][0].Equals("DG"))
                    {
                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                    //Click My Districts Selection
                    utilityMethods.ClickById("a_3_1_28");
                    //Click Clubs link
                    utilityMethods.ClickById("a_3_2_40");
                    //Navigating to the Club status
                    utilityMethods.FindDesiredClub("All Pending");
                    //Navigating to the Desired Application
                    utilityMethods.ViewApplication("Club-Sec-chk2");
                    //Verify the invisibility of delete
                    utilityMethods.VerifyInvisibleButton("Save", "btnSave", "Cancel", "btnCancel");
                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                    // Close the  Application
                    utilityMethods.CloseApplication();
                    }
                }
            }
        }
    }

