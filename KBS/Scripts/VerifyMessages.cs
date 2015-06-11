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
    public class VerifyMessages
    {
        IWebDriver browserDriver;
        [TestMethod]
        public void Messagecheck()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");

            utilityMethods = new UtilityMethods("Messages chk", "FAILED");
           // utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {


                if (data[i][0].Equals("LCI"))
                {

                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    //Click My Districts Selection
                    utilityMethods.ClickById(PageObjects.mnuLinkDistricts,"District Menu");
                    //Click Clubs link
                    utilityMethods.ClickById(PageObjects.subMenuLinkClubs,"Clubs Sub menu");

                    //Verify the Duplicate validation for Club Name

                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName,"Beta-club","Club Name");
                    // Click Save button
                    utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //Click Cancel Bbutton
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");
                   // Verify Districts Club Page is displayed
                    utilityMethods.VerifyTextDisplay(".//*[@id='lblCaption']", "District Clubs");
                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName,"Beta-club","Club Name");
                    //Click Save button
                    utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //verify the message for Club name duplicate 
                    utilityMethods.VerifyTextDisplay(".//*[@id='lblerr']", "The Club Name already exists.");
                    //Click on Cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");


                    //Verify the School name mandatory message
                   
                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName, "A1-club","Club Name");
                    //Drop down selection  for club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 1,"Club Type");
                    //Verify the Field School name exists
                    utilityMethods.VerifyElementExists(".//*[@id='txtSchoolName']","School Name");
                    //Select the checkbox of club criteria
                    utilityMethods.ClickById("cbReadNewClubCriteria","Club Criteria");
                    //Select the checkbox for submit
                    utilityMethods.ClickById(PageObjects.submitselection,"Submit");
                    //Click save button
                    utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //Verify the message for School name is mandatory
                    utilityMethods.VerifyMessage("School Name", ".//*[@id='lblerr']", "School Name is required");
                    //Click on Cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");



                    //Verify the Message for Lioness Lions Club selection
                    //Click Add club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName, "A1-club","Club Name");
                    //Drop down selection for Club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 2,"Club Type");
                    //Verify the message for Lioness Lion Club selection in Club type
                    utilityMethods.VerifyMessage("Lioness Lion Club", ".//*[@id='lblLionessWarning']", " Lioness Conversion Program Form");
                    //Click cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");

                    //Verify the Message for Morethan 10 Clubs in same District
                    utilityMethods.VerifyMessageclubcount();
                    //Click Cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");


                    //verify Help message in the Club Name
                    //utilityMethods.ClickByXPath(".//*[@id='Image1']");
                    
                    //utilityMethods.VerifyMessage("Club name help msg", ".//*[@id='lblClubNamingHelp']", "A proposed Lions club must be known by the actual name");
                    //utilityMethods.ClickByXPath(".//*[@id='Image1']");
                    //utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");

                    //Verify the Comments section
                    //utilityMethods.EnterValueById("txtNewClubAppComment", "test comment");
                    //utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //utilityMethods.VerifyButtonExists("Add Comment", "btnNewComment");
                    //utilityMethods.ClickById("btnNewComment");
                    //utilityMethods.EnterValueById("txtModalComments", "Comments Entered");
                    //utilityMethods.ClickById("btnModalCommentsSave");
                    //utilityMethods.VerifyMessage("Comments", ".//*[@id='notes']", "Comments Entered");

                    
                    //Verify the Message for Leo Lions Club selection
                    //Click Add club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName, "A2-club","Club Name");
                    //Drop down selection for Club type
                    utilityMethods.SelectDropdownValueByIndex("ddlClubType", 3,"Club Type");
                    //Verify the message for the Leo Lions club selection in club type
                    utilityMethods.VerifyMessage("Leo Lion Club", ".//*[@id='lblLeoLionWarning']", "Leo Lions club requires the submission of a Leo to Lion Certification");


                    //Verify the Message for required fields on submit
                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub,"Add Club");
                    //Select the checkbox for submit
                    utilityMethods.ClickById(PageObjects.submitselection,"Submit");
                    //Click save button
                    utilityMethods.ClickById(PageObjects.savebtn,"Save");
                    //Verify the message for Club Criteria is selected
                    utilityMethods.VerifyMessage(" Club criteria", ".//*[@id='lblerr']", "Club criteria should be selected" );
                    //Verify the message for Club name is mandatory
                    utilityMethods.VerifyMessage(" ClubName", ".//*[@id='lblerr']", "Club Name is required");
                    //Click Cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn,"Cancel");


                    //Verify the Pending DG/CL authorization link is displayed
                    utilityMethods.FindDesiredClub("Pending DG/CL Authorization");
                    utilityMethods.VerifyMessage("Status",".//*[@id='myGrid']/div[1]","Pending DG/CL Authorization")

                   


                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                    // Close the  Application
                    utilityMethods.CloseApplication();
                }
            }
                
                  
        }
    }
}
