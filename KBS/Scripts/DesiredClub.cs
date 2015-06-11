using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class DesiredClub
    {
        [TestMethod]
        public void DesiredClubSelection()
        {
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login");
            
            
            for (int i = 0; i < data.Count; i++)
            {
                UtilityMethods utilityMethods = new UtilityMethods("DesiredClub", i,"FAILED"); 
                if (data[i][0].Equals("LCI"))
                {

                    
                 //  utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");


                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);
                   


                    //utilityMethods.ClickById("a_3_1_28");


                    //utilityMethods.ClickById("a_3_2_40");

                    //utilityMethods.FindDesiredClub("LCI Authorization");

                    utilityMethods.FindDesiredClub("All Pending");

                    utilityMethods.ViewApplication("Alpha Club");
                    utilityMethods.VerifyFieldEdit("Find Club", "txtClubName");


               
                     //utilityMethods.ViewApplication("Club123");

                    //utilityMethods.LogoutMyLCI();

                    utilityMethods.CloseApplication();
                }       
            }
        }
    }
}
