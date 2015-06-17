using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;



namespace LCI.QualityTools.BrowserTests.MyLCI
{
    [TestClass]
    public class VerifyClubOfficersFieldValidations
    {
        IWebDriver browserDriver;
    [TestMethod]
          
        public void ClubOfficersValidations()
        {
            bool browserStatus;
            string ClubName = null;
            UtilityMethods utilityMethods;
            
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");
            ExcelReporter excelReporter = new ExcelReporter();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");
            utilityMethods = new UtilityMethods("ClubOfficersValidation", Properties["ScreenshotCaptureFlag"]);

            browserStatus = utilityMethods.invokeBrowser(Properties["Browser"]);

            if (browserStatus == false)
            {
                excelReporter.ReportStep("This browser type is not supported", "FAILURE");
            }
            else
            {
                utilityMethods.InvokeApplication(Properties["ApplicationURL"]);
                try
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i][0].Equals("LCI"))
                        {
                            //Call the login method and to verify the home page is displayed
                            utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                            //Click My Districts Selection
                            utilityMethods.ClickById(PageObjects.mnuLinkDistricts, "Districts Menu");
                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.subMenuLinkClubs, " Clubs Sub menu");
                            //Click Add club
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add club");
                            //Create new club
                             ClubName=utilityMethods.AddNewclub();
                             // Passing the Club name to the text field
                             utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                            

                             //Select Club Type
                             utilityMethods.SelectDropdownValueByVisibleText(PageObjects.ddlClubType, "Lions Club", "Club Type");

                             //Enter Club City 
                             utilityMethods.EnterValueById(PageObjects.idClubCity, "Automation TestCity", "Club City");


                             //Select from Club Language
                             utilityMethods.SelectDropdownValueByVisibleText(PageObjects.ddlclubLang, "English", "Club Language");

                             //Click for a sponsoring club
                             utilityMethods.ClickById(PageObjects.btnSponsoringClub, "Sponsoring ClubName");
                             //browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                             utilityMethods.ClickByXPath(PageObjects.xpathListSponsoringClubs, "Sponsoring Club");
                             //browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

                             //Check New Club Criteria checkbox
                             //utilityMethods.ClickById(PageObjects.idNewClubCriteria, "New Club Criteria");

                             //Enter Charter Member details
                             utilityMethods.EnterValueById(PageObjects.idNewMemberscount, "20", "New Member Count");
                             utilityMethods.EnterValueById(PageObjects.idTransferMemberscount, "0", "Transfer Member Count");
                             utilityMethods.EnterValueById(PageObjects.idStudentMemberscount, "0", "Student Member Count");
                             utilityMethods.EnterValueById(PageObjects.idLeoLionscount, "0", "LeoLions Member Count");


                            //Click Save button
                             utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Verify Delete button is enabled to check save is successful
                             utilityMethods.VerifyButtonExists("Delete", PageObjects.idBtnDelete);
                            //click submit for DG Authorization
                             utilityMethods.ClickById(PageObjects.idBtnSubmitPendingSubmission, "DG Auth Submit button");
                            //click save button
                             utilityMethods.ClickById(PageObjects.idBtnSave, "Save Button");
                            //verify message for new club officers validation on pending submission status
                             utilityMethods.VerifyMessage("Label error", PageObjects.xpathErrorLabel, "At least one New Club Officer is required. Either the President or Secretary fields need to be completed.");
                            //Click Cancel button
                             utilityMethods.ClickById(PageObjects.idBtnCancel,"Cancel");
                            //Navigating to club status
                             utilityMethods.FindDesiredClub("All Pending");
                            //Navigating to the desired view Application
                             utilityMethods.ViewApplication(ClubName);

                             //Enter New Club President creation details
                             utilityMethods.ClickByXPath(PageObjects.xpathClubPresidentPanel, "New Club President Creation Panel");
                             utilityMethods.EnterValueById(PageObjects.idPresidentFirstname, "PresidentFirstname1", "New Club President FirstName");
                             utilityMethods.EnterValueById(PageObjects.idPresidentLastname, "PresidentLastname1", "New Club President LastName");
                             utilityMethods.EnterValueById(PageObjects.idPresidentYOB, "1980", "New Club President YOB");
                             utilityMethods.SelectDropdownValueByVisibleText(PageObjects.idPresidentGender, "Male", "New Club President Gender");
                             utilityMethods.EnterValueById(PageObjects.idPresidentEmailaddress, "president1@test.com", "New Club President Email");


                            //Move the status from pending Submission to Pending DG authorization
                             utilityMethods.MoveClubtoNextStatus("Pending Submission");

                             //Enter New Club Secretary Creation details
                             utilityMethods.ClickByXPath(PageObjects.xpathClubSecretaryPanel, "New Club Secretary Creation Panel");
                             utilityMethods.EnterValueById(PageObjects.idSecretaryFirstname, "SecretaryFirstName1", "New Club Secretary Firstname");
                             utilityMethods.EnterValueById(PageObjects.idSecretaryLastname, "SecretaryLastName1", "New Club Secretary LastName");
                             utilityMethods.EnterValueById(PageObjects.idSecretaryYOB, "1980", "New Club Secretary YOB");
                             utilityMethods.EnterValueById(PageObjects.idSecretaryGender, "Female", "New Club Secretary Gender");
                             utilityMethods.EnterValueById(PageObjects.idSsecretaryEmailaddress, "testsecretary1@test.com", "New Club Secretary Email");
                             //Click Save button
                             utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Click Cancel button
                             utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");

                            ////Verify message for new club officer validation in pending submission status
                            // //Click Add club
                            // utilityMethods.ClickById(PageObjects.idAddClub, "Add club");
                            // //Create new club
                            // ClubName = utilityMethods.AddNewclub();


                             //Logout from the Application
                             utilityMethods.LogoutMyLCI();

                             // Close the  Application
                             utilityMethods.CloseApplication();


                           


               



                        }

                    }
                }
                catch (Exception e)
                {
                    e.StackTrace.ToString();
                }
            }
        }
    }
}

