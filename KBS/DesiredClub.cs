using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace KBS
{
    [TestClass]
    public class DesiredClub
    {
        [TestMethod]
        public void TestMethod1()
        {
            UtilityMethods UM = new UtilityMethods("Login",1);
            DataInputProvider DIP = new DataInputProvider();
            List<List<String>> data = DIP.GetInputData("Login");
            
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i][0].Equals("LCI"))
                {
                    UM.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    UM.ClickById("a_3_1_28");

                    UM.ClickById("a_3_2_40");

                    UM.FindDesiredClub("LCI Authorization");

                    UM.ViewApplication("Club123");

                    UM.LinkClickByText("Logout");

                    UM.CloseApplication();
                }
                else
                {
                    // click logout
                    UM.CloseApplication();
                }
                
            }
        }
    }
}
