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
            List<List<string>> lciLoginData = dataInputProvider.GetInputData("LCILogin");
            UtilityMethods utilityMethods = new UtilityMethods("DiscontinueClub", "ALL");
            string ClubName = null;

            utilityMethods.InvokeApplication("Firefox", "http://mylcibeta.lionsclubs.org/");
            try
            {
                for (int i = 0; i < lciLoginData.Count; i++)
                {
           
                     // Call the login method and to verify the home page is displayed
                     utilityMethods.LoginMyLCI(lciLoginData[i][0], lciLoginData[i][1], lciLoginData[i][2]);

                     //Verify "Add Club" link exists for this user
                     Boolean addClubLinkstatus = utilityMethods.VerifyAddClubLinkExists("hlAddClub");
                                                        
                     //Click Add Club link
                     utilityMethods.ClickById("hlAddClub");

                     ClubName = utilityMethods.AddClubFormEntry();

                     utilityMethods.MoveClubtoNextStatus("Pending DG/CL Authorization");

                     utilityMethods.DiscontinueClub(ClubName);

                     utilityMethods.ContinueClub(ClubName);

                     utilityMethods.LogoutMyLCI();
                 }
                                                           
                 for (int i = 0; i < data.Count; i++)
                 {
                    utilityMethods.LoginMyLCI(data[i][0],data[i][1], data[i][2]);

                   // utilityMethods.FindClubs();

                    utilityMethods.FindDesiredClub("Pending DG/CL Authorization");

                    utilityMethods.ViewApplication("Club352");
                    
                     // utilityMethods.ViewApplication(ClubName);

                     utilityMethods.DiscontinueClub("Club352");
                     
                     //  utilityMethods.DiscontinueClub(ClubName);

                     utilityMethods.VerifyButtonExists("Add Comments", "btnNewComment");

                     utilityMethods.VerifyTextDisplay("//div[@id='divNewClubApplication']/div[1]/div/div[2]", "The Club Application has been Discontinued");

                     utilityMethods.LogoutMyLCI();

                     for (int j = 0; j < lciLoginData.Count; j++)
                     {

                        utilityMethods.LoginMyLCI(lciLoginData[j][0], lciLoginData[j][1], lciLoginData[j][2]);

                       // utilityMethods.FindClubs();

                        utilityMethods.FindDesiredClub("All Discontinued");

                        utilityMethods.ViewApplication("Club352");

                        utilityMethods.ContinueClub("Club352");

                        utilityMethods.VerifyTextDisplay("//div[@id='divNewClubApplication']/div[1]/div/div[2]", "pending District Governor authorization");
                        
                        utilityMethods.LogoutMyLCI();
                      }
                  }
              }
              catch (Exception e)
              {
                  e.StackTrace.ToString();
              }
                  
              // close application
              utilityMethods.CloseApplication();
          }
     }
}
    

          