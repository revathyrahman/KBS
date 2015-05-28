using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace KBS
{
    [TestClass]
    public class AddClub
    {
        //This method enters all the field values and saves a New Club Form
        [TestMethod]
        public void AddClubFormEntry()
        {
            DataInputProvider DIP = new DataInputProvider();
            ExcelReporter er = new ExcelReporter();
            List<List<String>> data = DIP.GetInputData("Login");
            for (int i = 0; i < data.Count; i++)
            {
                UtilityMethods UM = new UtilityMethods("AddClubFormEntry", i);
                UM.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");
                        
                try
                {
                    //Call the login method and to verify the home page is displayed
                    UM.LoginMyLCI(data[i][0],data[i][1],data[i][2]);

                    //Click on My Districts Link in the home page
                    UM.ClickById("a_3_1_28");

                    //Click on Clubs Link under My Districts Menu List
                    UM.ClickById("a_3_2_40");

                    //Verify "Add Club" link exists for this user
                    Boolean status=UM.VerifyAddClubLinkExists("hlAddClub");

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

                    //Calling the method to create a new club
                    UM.AddClubFormEntry();

                    //Logout from the application
                    UM.LogoutMyLCI();
                }
                catch (WebDriverException e)
                {
                    e.StackTrace.ToString();
                }

                // Close the browser and write the report into Excelsheet
                UM.CloseApplication();
            }
        }
    }
}
