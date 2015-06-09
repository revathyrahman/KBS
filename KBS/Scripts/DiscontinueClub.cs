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
           DataInputProvider dataInputProvider = new DataInputProvider();
            ExcelReporter excelReporter = new ExcelReporter();
            List<List<String>> data = dataInputProvider.GetInputData("Login");
           UtilityMethods utilityMethods;
            

            for (int i = 0; i < data.Count; i++)
            {
                utilityMethods = new UtilityMethods("DiscontinueClub",i, "ALL");
                utilityMethods.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");
                try
                {
                   
                       // Call the login method and to verify the home page is displayed
                        utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                        //Verify "Add Club" link exists for this user
                        Boolean addClubLinkstatus = utilityMethods.VerifyAddClubLinkExists("hlAddClub");

                                             
                       //Click Add Club link
                        utilityMethods.ClickById("hlAddClub");

                        string ClubName = utilityMethods.AddClubFormEntry();

                        utilityMethods.MoveClubtoNextStatus("Pending DG/CL Authorization");
                        
                        utilityMethods.DiscontinueClub(ClubName);

                        // click logout
                        utilityMethods.LinkClickByText("Logout");

                        if (data[i][0].Equals("LCI"))
                        {

                            utilityMethods.LoginMyLCI(data[i][0],data[i][1], data[i][2]);

                            utilityMethods.FindDesiredClub("All Discontinued");
                           
                            utilityMethods.ViewApplication(ClubName);
                            
                            utilityMethods.ContinueClub(ClubName);
                            utilityMethods.LogoutMyLCI();

                        }

                        // close application
                        utilityMethods.CloseApplication();
                    }
                
                

                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
                finally
                {
                    utilityMethods.CloseApplication();
               }
         
           }

        }
    }
}
          