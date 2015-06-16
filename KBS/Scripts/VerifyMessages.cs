using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace LCI.QualityTools.BrowserTests.MyLCI.Scripts
{
    [TestClass]
    public class VerifyMessages
    {
        IWebDriver browserDriver;
        [TestMethod]
        public void Messagecheck()
        {
            bool browserStatus;
            string ClubName = null;
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");
            ExcelReporter excelReporter = new ExcelReporter();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");
            utilityMethods = new UtilityMethods("VerifyMessages", Properties["ScreenshotCaptureFlag"]);

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
                            utilityMethods.ClickById(PageObjects.mnuLinkDistricts, "District Menu");
                            //Click Clubs link
                            utilityMethods.ClickById(PageObjects.subMenuLinkClubs, "Clubs Sub menu");

                            //Verify the Duplicate validation for Club Name

                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add club");
                            //Create new Club
                            ClubName = utilityMethods.AddNewclub();
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                            
                            // Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Click Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");
                            // Verify Districts Club Page is displayed
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathLabelCaption, "District Clubs");
                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName,ClubName, "Club Name");
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //verify the message for Club name duplicate 
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathErrorLabel, "The Club Name already exists.");
                            //Click  Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");


                            //Verify the School name mandatory message

                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            //Create new Club
                            ClubName = utilityMethods.AddNewclub();
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                            //Drop down selection  for club type
                            utilityMethods.SelectDropdownValueByIndex(PageObjects.ddlClubType, 1, "Club Type");
                            //Verify the Field School name exists
                            utilityMethods.VerifyElementExists(PageObjects.xpathSchoolName, "School Name");
                            //Select the checkbox of club criteria
                            utilityMethods.ClickById(PageObjects.idClubCriteria, "Club Criteria");
                            //Select the checkbox for submit
                            utilityMethods.ClickById(PageObjects.idBtnSubmitPendingSubmission, "Submit");
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Verify the message for School name is mandatory
                            utilityMethods.VerifyMessage("School Name", PageObjects.xpathErrorLabel, "School Name is required");
                            //Click  Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");



                            //Verify the Message for Lioness Lions Club selection
                            //Click Add club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                            //Drop down selection for Club type
                            utilityMethods.SelectDropdownValueByIndex(PageObjects.ddlClubType, 2, "Club Type");
                            //Verify the message for Lioness Lion Club selection in Club type
                            utilityMethods.VerifyMessage("Lioness Lion Club", PageObjects.xpathLionessWrng, " Lioness Conversion Program Form");
                            //Click cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");

                            //Verify the Message for Morethan 10 Clubs in same District
                            utilityMethods.VerifyMessageclubcount();
                            //Click Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");


                            //verify Help message in the Club Name
                            //Click Add club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");

                            //Click on Help Icon
                            utilityMethods.ClickByCSS(PageObjects.cssImgClubHelp, "Club Help");
                            //Verify the Club Help Message
                            utilityMethods.VerifyMessage("Club name help msg", PageObjects.xpathClubHelp, "A proposed Lions club must be known by the actual name");
                            //Click on Help Icon
                            utilityMethods.ClickByCSS(PageObjects.cssImgClubHelp, "Club Help");

                           

                            //Verify the Comments section
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                           //Passing Comments to the comments text area
                            utilityMethods.EnterValueById(PageObjects.idComments, "test comment", "Comments");
                            //Click  Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Verify Add Comment button exists
                            utilityMethods.VerifyButtonExists("Add Comment", "btnNewComment");
                            //Click  Add comment button
                            utilityMethods.ClickById(PageObjects.idAddCommentbtn, "Add comment");
                            //Passing the value to Add comments section
                            utilityMethods.EnterValueById(PageObjects.idAddModalComments, "Comments Entered", "Add Comments");
                            //Click  Add comments save button
                            utilityMethods.ClickById(PageObjects.btnModalSave, "Modal Save");
                            //Verify the messages in the add comments section
                            utilityMethods.VerifyMessage("Comments", PageObjects.xpathAddCommentsNotes, "Comments Entered");
                            //Click  Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");

                            //Verify the Message for Leo Lions Club selection
                            //Click Add club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName, ClubName, "Club Name");
                            //Drop down selection for Club type
                            utilityMethods.SelectDropdownValueByIndex(PageObjects.ddlClubType, 3, "Club Type");
                            //Verify the message for the Leo Lions club selection in club type
                            utilityMethods.VerifyMessage("Leo Lion Club", PageObjects.xpathLeolionWarning, "Leo Lions club requires the submission of a Leo to Lion Certification");
                            //Click of Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");

                            //Verify the Message for required fields on submit
                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            //Select the checkbox for submit
                            utilityMethods.ClickById(PageObjects.idBtnSubmitPendingSubmission, "Submit");
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave, "Save");
                            //Verify the message for Club Criteria is selected
                            utilityMethods.VerifyMessage(" Club criteria", PageObjects.xpathErrorLabel, "You must check that you have read the New Club Criteria document");
                            //Verify the message for Club name is mandatory
                            utilityMethods.VerifyMessage(" ClubName", PageObjects.xpathErrorLabel, "Club Name is required");
                            //Click Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");


                            //Verify the Pending DG/CL authorization link is displayed
                            utilityMethods.FindDesiredClub("Pending DG/CL Authorization");
                            utilityMethods.VerifyMessage("Status", PageObjects.xpathFindClubStatusListLabel, "Pending DG/CL Authorization");

                            //Verify the Pending LCI Authorization is displayed
                            utilityMethods.FindDesiredClub("Pending LCI Authorization");
                            utilityMethods.VerifyMessage("Status", PageObjects.xpathFindClubStatusListLabel, "Pending LCI Authorization");

                            //Verify the Pending Club Completion  link is displayed
                            utilityMethods.FindDesiredClub("Pending Club Completion");
                            utilityMethods.VerifyMessage("Status", PageObjects.xpathFindClubStatusListLabel, "Pending Club Completion");

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
