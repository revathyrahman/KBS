using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
namespace KBS
{
    using NUnit.Framework;
    
    public class UnitTest3
    {
        [TestCase]
        public void TestMethod1()
        {
            DataInputProvider DIP = new DataInputProvider();
            List<List<String>> data = DIP.GetInputData("Login");
            for (int i = 0; i < data.Count; i++)
            {
                UtilityMethods UM = new UtilityMethods("Login", i);
                UM.InvokeApplication("Firefox", "http://test.jiatro.com/mhs");
                try
                {
                     // enter user name
                     UM.EnterValueById("land_username", data[i][0]);

                   // enter password
                     UM.EnterValueByName("txtPassword", data[i][1]);

                    // click login
                    UM.ClickByCSS("input[type='image']");

                    // click logout
                    UM.LinkClickByText("Logout");
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                }

                // close app
                UM.CloseApplication();
            }
        }
    }
}
