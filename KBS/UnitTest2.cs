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
            DataInputProvider DIP = new DataInputProvider();
            ExcelReporter er = new ExcelReporter();
            List<List<String>> data = DIP.GetInputData("Login");
            
            for (int i = 0; i < data.Count; i++)
            {
<<<<<<< HEAD
                UtilityMethods UM = new UtilityMethods("AddClubFormEntry", i);
                UM.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");

                try
=======
                if (data[i][0].Equals("LCI"))
>>>>>>> ccd28713b69c6da8790ee10d48767be591c52ad4
                {
                    //Call the login method and to verify the home page is displayed
                    UM.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

<<<<<<< HEAD
                    //Click on My Districts Link in the home page
                    UM.ClickById("a_3_1_28");

                    //Click on Clubs Link under My Districts Menu List
                    UM.ClickById("a_3_2_40");

                    //Verify "Add Club" link exists for this user
                    Boolean status = UM.VerifyAddClubLinkExists("hlAddClub");

                    if (status.Equals(true))
                    {
                        er.ReportStep("User is Authorized User", "SUCCESS");
                    }
                    else
                    {
                        er.ReportStep("User is not Authorized User", "FAILURE");
                    }

                    //Click Add Club link
                    UM.ClickById("hlAddClub");

                    string ClubName = UM.AddClubFormEntry();
                    UM.MoveClubtoNextStatus("DG Auth");
                    UM.DiscontinueClub(ClubName);
                    UM.ContinueClub(ClubName);
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

                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
=======
                    UM.VerifyMyTask("Authorization");

                    UM.ViewApplication("Club123");

                    UM.LinkClickByText("Logout");

                    UM.CloseApplication();
                }
                else
                {
                    // click logout
                    UM.CloseApplication();
                }
                
>>>>>>> ccd28713b69c6da8790ee10d48767be591c52ad4
            }
        }
    }
}
