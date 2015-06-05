using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        [TestMethod]
        public void FieldChk()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login");

            for (int i = 0; i < data.Count; i++)
            {
                utilityMethods = new UtilityMethods("Field Chk_469", i);
                if (data[i][0].Equals("LCI"))
                {
                    utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    utilityMethods.ClickById("a_3_1_28");

                    utilityMethods.ClickById("a_3_2_40");
                    utilityMethods.FindDesiredClub("All Pending");

                    utilityMethods.ViewApplication("Alpha club");

                    utilityMethods.VerifyFieldEdit("Club Name", "txtClubName']");



                    utilityMethods.LogoutMyLCI();

                    utilityMethods.CloseApplication();
                }
            }
        }
    }
}
