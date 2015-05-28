using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace KBS
{
    [TestClass]
    public class SelectViewTask
    {

        [TestMethod]
        public void TestMethod1()
        {
            UtilityMethods UM = new UtilityMethods("Login",1);
            DataInputProvider DIP = new DataInputProvider();
            ExcelReporter er = new ExcelReporter();
            List<List<String>> data = DIP.GetInputData("Login");
            UM.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");
            for (int i = 0; i < data.Count; i++)
            {


                try
                {
                    // enter user name

                    UM.MyLCI_Login(data[i][0], data[i][1]);

                    //UM.AddClubFormEntry();

                    UM.VerifyMyTask("Authorization");

                    UM.MyLCI_Logout();
                }
                catch (WebDriverException e)
                {
                    e.StackTrace.ToString();
                }



            }
            // close app
            UM.CloseApplication();

        }


    }
}
