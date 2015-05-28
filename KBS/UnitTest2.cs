using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace KBS
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            UtilityMethods UM = new UtilityMethods("Login",1);
            DataInputProvider DIP = new DataInputProvider();
            List<List<String>> data = DIP.GetInputData("Login");
            for (int i = 0; i < data.Count; i++)
            {
                if(data[i][0] == "DG")
                {
                    UM.LoginMyLCI(data[i][0], data[i][1], data[i][2]);



                }
                else
                    continue;
                // click logout
                    UM.LinkClickByText("Logout");
                //       }


                //    catch (Exception e)
                //    {
                //        // TODO Auto-generated catch block
                //    }

                //    // close app
                   UM.CloseApplication();
                // }
                 
                //}
            }
        }
    }
}
