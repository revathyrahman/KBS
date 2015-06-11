using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;



namespace MyLCIAutomation.Scripts
{
    [TestClass]
    public class Messagechk
    {
        IWebDriver browserDriver;
        [TestMethod]
        public void Messagecheck()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");

            utilityMethods = new UtilityMethods("Messages chk", "FAILED");
            utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {


                if (data[i][0].Equals("LCI"))
                {

                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    //Click My Districts Selection
                    utilityMethods.ClickById("a_3_1_28");
                    //Click Clubs link
                    utilityMethods.ClickById("a_3_2_40");

                    //Verify the Duplicate validation for Club Name

                    //Click Add Club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "Beta-club");
                    // Click Save button
                    utilityMethods.ClickById("btnSave");
                    //Click Cancel Bbutton
                    utilityMethods.ClickById("btnCancel");
                   // Verify Districts Club Page is displayed
                    utilityMethods.VerifyTextDisplay(".//*[@id='lblCaption']", "District Clubs");
                    //Click Add Club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "Beta-club");
                    //Click Save button
                    utilityMethods.ClickById("btnSave");
                    //verify the message for Club name duplicate 
                    utilityMethods.VerifyTextDisplay(".//*[@id='lblerr']", "The Club Name already exists.");
                    //Click on Cancel button
                    utilityMethods.ClickById("btnCancel");


                    //Verify the School name mandatory message
                   
                    //Click Add Club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "A1-club");
                    //Drop down selection  for club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 1);
                    //Verify the Field School name exists
                    utilityMethods.VerifyElementExists(".//*[@id='txtSchoolName']");
                    //Select the checkbox of club criteria
                    utilityMethods.ClickById("cbReadNewClubCriteria");
                    //Select the checkbox for submit
                    utilityMethods.ClickById("cbStatusAction_Submit");
                    //Click save button
                    utilityMethods.ClickById("btnSave");
                    //Verify the message for School name is mandatory
                    utilityMethods.VerifyMessage("School Name", ".//*[@id='lblerr']", "School Name is required");
                    //Click on Cancel button
                    utilityMethods.ClickById("btnCancel");



                    //Verify the Message for Lioness Lions Club selection
                    //Click Add club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "A1-club");
                    //Drop down selection for Club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 2);
                    //Verify the message for Lioness Lion Club selection in Club type
                    utilityMethods.VerifyMessage("Lioness Lion Club", ".//*[@id='lblLionessWarning']", " Lioness Conversion Program Form");
                    //Click cancel button
                    utilityMethods.ClickById("btnCancel");

                   //Verify the Message for Morethan 10 Clubs in same District
                    utilityMethods.VerifyMessageclubcount();
                    //Click Cancel button
                    utilityMethods.ClickById("btnCancel");


                    //verify Help message in the Club Name
                    //utilityMethods.ClickByXPath(".//*[@id='Image1']");
                    
                    //utilityMethods.VerifyMessage("Club name help msg", ".//*[@id='lblClubNamingHelp']", "A proposed Lions club must be known by the actual name");
                    //utilityMethods.ClickByXPath(".//*[@id='Image1']");
                    //utilityMethods.ClickById("btnCancel");

                    //Verify the Comments section
                    //utilityMethods.EnterValueById("txtNewClubAppComment", "test comment");
                    //utilityMethods.ClickById("btnSave");
                    //utilityMethods.VerifyButtonExists("Add Comment", "btnNewComment");
                    //utilityMethods.ClickById("btnNewComment");
                    //utilityMethods.EnterValueById("txtModalComments", "Comments Entered");
                    //utilityMethods.ClickById("btnModalCommentsSave");
                    //utilityMethods.VerifyMessage("Comments", ".//*[@id='notes']", "Comments Entered");

                    
                    //Verify the Message for Leo Lions Club selection
                    //Click Add club link
                    utilityMethods.ClickById("hlAddClub");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById("txtClubName", "A2-club");
                    //Drop down selection for Club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 3);
                    //Verify the message for the Leo Lions club selection in club type
                    utilityMethods.VerifyMessage("Leo Lion Club", ".//*[@id='lblLeoLionWarning']", "Leo Lions club requires the submission of a Leo to Lion Certification");



                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                    // Close the  Application
                    utilityMethods.CloseApplication();
                }
            }
                
                  
        }
    }
}
