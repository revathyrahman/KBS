using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class UnitTest2
    {
        IWebDriver driver;
        [TestMethod]
        public void DeleteButtonChk()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");
            utilityMethods = new UtilityMethods("Deletevisible_839", "FAILED");
            utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i][0].Equals("LCI"))
                {

                    //    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    //    utilityMethods.ClickById("a_3_1_30");

                    //    utilityMethods.ClickById("a_3_2_35");
                    //    utilityMethods.ClickById("hlAddClub");
                    //    utilityMethods.EnterValueById("txtClubName", "Club-Sec-chk2","ClubName");
                    //    utilityMethods.ClickById("btnSave");
                    //    utilityMethods.LogoutMyLCI();
                    //}
                    //    if (data[i][0].Equals("DG"))
                    {
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                        // utilityMethods.LoginMyLCI(data[1][0], data[1][1], data[1][2]);
                        utilityMethods.ClickById("a_3_1_28");

                        utilityMethods.ClickById("a_3_2_40");
                        
                        utilityMethods.FindDesiredClub("All Pending");

                        utilityMethods.ViewApplication("Club875");
                        utilityMethods.VerifyInvisibleButton("Save", "btnSave", "Cancel", "btnCancel");

                        utilityMethods.LogoutMyLCI();

                        utilityMethods.CloseApplication();
                    }
                }
            }
        }
    }
}

