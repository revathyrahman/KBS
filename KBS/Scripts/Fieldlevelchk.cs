using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;



namespace MyLCIAutomation
{
    [TestClass]
    public class Fieldlevelchk
    {
        [TestMethod]

    public void FieldChk()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login");

            utilityMethods = new UtilityMethods("Cancel and  Delete and Field lvl chk", "FAILED");
            utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

            for (int i = 0; i < data.Count; i++)
            {
              
               
                if (data[i][0].Equals("LCI"))
                {

                    //Call the login method and to verify the home page is displayed
                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    //Click My Districts Selection
                    utilityMethods.ClickById(PageObjects.mnuLinkDistricts);
                    //Click Clubs link
                    utilityMethods.ClickById(PageObjects.subMenuLinkClubs);

                   //Verify Cancel request of the New Club 

                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub);
                    //Click  on Cancel button
                    utilityMethods.ClickById(PageObjects.cancelbtn);
                    //Verify Districts Club Page is displayed
                    utilityMethods.VerifyTextDisplay(PageObjects.labelcaption, "District Clubs");


                    // Verify Delete functionality with Clubname Exists after deletion

                   // Click Add Club link
                   utilityMethods.ClickById(PageObjects.linkTextAddClub);
                   // Passing the Club name to the text field
                   utilityMethods.EnterValueById(PageObjects.clubName, "Club-Jeynew");
                   // Click Save button
                   utilityMethods.ClickById(PageObjects.savebtn);
                   // Verify visibility of  buttons
                   utilityMethods.VerifyButtonExists("Save", PageObjects.savebtn);
                   utilityMethods.VerifyButtonExists("Delete", PageObjects.deletebtn);
                   utilityMethods.VerifyButtonExists("Cancel", PageObjects.cancelbtn);
                   // Click Delete button
                   utilityMethods.ClickById(PageObjects.deletebtn);
                  // Validate the Club name to be deleted is displayed
                   utilityMethods.VerifyTextDisplay(".//*[@id='form1x']/div[4]/div[1]/div[5]/div[1]/div[1]/div/div[6]/div[2]/div/div", "Club-Jeynew");
                  //Click Save button
                   utilityMethods.ClickById(PageObjects.savebtn);
                   //Verify Districts Club Page is displayed
                   utilityMethods.VerifyTextDisplay(PageObjects.labelcaption, "District Clubs");
                   //Navigating to the Club status
                    utilityMethods.FindDesiredClub("All Pending");
                    //Verify Club is deleted
                    utilityMethods.VerifyDeleteApplication("Club-Jeynew");



                    //New Club creation and deletion
                    //Click Add Club link
                    utilityMethods.ClickById(PageObjects.linkTextAddClub);
                    // Passing the Club name to the text field
                    utilityMethods.EnterValueById(PageObjects.clubName, "club-Jeynew");
                    //Click Save button
                    utilityMethods.ClickById(PageObjects.savebtn);
                    //Verify visibility  of button
                    utilityMethods.VerifyButtonExists("Save", PageObjects.savebtn);
                    utilityMethods.VerifyButtonExists("Delete", PageObjects.deletebtn);
                    utilityMethods.VerifyButtonExists("Cancel", PageObjects.cancelbtn);

                    //Once again to delete the existing club
                    //Click Delete button
                    utilityMethods.ClickById(PageObjects.deletebtn);
                    //Click Save button
                    utilityMethods.ClickById(PageObjects.savebtn);
                    //Verify Districts Page is displayed
                    utilityMethods.VerifyTextDisplay(PageObjects.labelcaption, "District Clubs");
                    //Navigating to the Club status
                    utilityMethods.FindDesiredClub("All Pending");


                    // Verify Fields are editable
                    //Navigating to the Desired Application
                    utilityMethods.ViewApplication("Alpha club");
                    //Verify the Club Name field is editable
                    utilityMethods.VerifyFieldEdit("Club Name",PageObjects.clubName);
                    //Verify the Club Type Dropdown is editable
                    utilityMethods.VerifyDropdownEdit("Club Type",PageObjects.clubType);
                    //Verify the City field is editable 
                    utilityMethods.VerifyFieldEdit("City", PageObjects.city);
                    //Verify the Club language Dropdown  is editable
                    utilityMethods.VerifyDropdownEdit("Club Language", PageObjects.clubLang);
                    //Verify the Sponsoring Club button is enabled
                    utilityMethods.VerifyButtonExists("Sponsoring Club", PageObjects.sponsoringClub);
                    //Verify New Club Officers fields are editable
                    utilityMethods.NewClubOfficersChk();
                    //Verify Estimate of Chambers fields are editable
                    utilityMethods.ECMforLionsClub();
                    //Verify the Checkbox is visible for Club criteria
                    utilityMethods.VerifyCheckboxExists("Club Criteria", PageObjects.clubcriteria);
                    //Verify the Comments text area  is editable
                    utilityMethods.VerifyFieldEdit("Comments", PageObjects.comments);

                                                                         

                    //Logout from the Application
                    utilityMethods.LogoutMyLCI();
                   // Close the  Application
                   utilityMethods.CloseApplication();
               }
           }

        }
   }
}
