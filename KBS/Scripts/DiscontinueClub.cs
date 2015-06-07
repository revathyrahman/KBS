using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace MyLCIAutomation
{
    [TestClass]
    public class DiscontinueClub
    {
        [TestMethod]
        public void DiscontinueClubDGAuth()
        {
        //    DataInputProvider dataInputProvider = new DataInputProvider();
        //    ExcelReporter excelReporter = new ExcelReporter();
        //    List<List<String>> data = dataInputProvider.GetInputData("Login");
        //    UtilityMethods utilityMethods;
            
        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        utilityMethods = new UtilityMethods("DiscontinueClub", i);
        //        utilityMethods.InvokeApplication("chrome", "http://mylcibeta.lionsclubs.org/");
        //        try
        //        {
        //            if (data[i][0].Equals("LCI"))
        //            {
        //                //Call the login method and to verify the home page is displayed
        //                utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);


        //                ////Click on My Districts Link in the home page
        //                //utilityMethods.ClickById("a_3_1_28");

        //                ////Click on Clubs Link under My Districts Menu List
        //                //utilityMethods.ClickById("a_3_2_40");

        //                //Verify "Add Club" link exists for this user
        //                Boolean status = utilityMethods.VerifyAddClubLinkExists("hlAddClub");

        //                if (status.Equals(true))
        //                    excelReporter.ReportStep("User is Authorized User", "SUCCESS");

        //                else
        //                    excelReporter.ReportStep("User is not Authorized User", "FAILURE");


        //                //Click Add Club link
        //                //utilityMethods.ClickById("hlAddClub");

        //                //string ClubName = utilityMethods.AddClubFormEntry();

        //                //utilityMethods.MoveClubtoNextStatus("DG Auth");

        //                //utilityMethods.DiscontinueClub(ClubName);

        //                //utilityMethods.ContinueClub(ClubName);
        //                //// click logout
        //                //utilityMethods.LinkClickByText("Logout");


        //                //Verify "Add Club" link exists for this user
        //                Boolean status = utilityMethods.VerifyAddClubLinkExists("hlAddClub");

        //                if (status.Equals(true))
        //                    excelReporter.ReportStep("User is Authorized User", "SUCCESS");

        //                else
        //                    excelReporter.ReportStep("User is not Authorized User", "FAILURE");


        //                //Click Add Club link
        //                utilityMethods.ClickById("hlAddClub");

        //                string ClubName = utilityMethods.AddClubFormEntry();

        //                utilityMethods.MoveClubtoNextStatus("DG Auth");

        //                utilityMethods.DiscontinueClub(ClubName);

        //                utilityMethods.ContinueClub(ClubName);
        //                // click logout
        //                utilityMethods.LinkClickByText("Logout");

        //                // close application
        //                utilityMethods.CloseApplication();
        //            }
        //        }

        //        catch (Exception e)
        //        {
        //            e.StackTrace.ToString();
        //        }
        //        finally
        //        {
        //            utilityMethods.CloseApplication();
        //        }
         
        //    }

        }
    }
}
