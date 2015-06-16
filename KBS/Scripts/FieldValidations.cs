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
    public class VerifyFieldValidations
    {
        [TestMethod]

        public void FieldValidations()
        {
            bool browserStatus;
            string ClubName =null;
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("LCILogin");
            ExcelReporter excelReporter = new ExcelReporter();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Dictionary<string, string> Properties = ReadProperty.GetProperties(theDirectory + "\\..\\..\\ConfigProperities.txt");
            utilityMethods = new UtilityMethods("FieldLevelValidation", Properties["ScreenshotCaptureFlag"]);

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

                            //Verify Cancel request of the New Club 

                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub,"Add Club");
                            //Click  on Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel,"Cancel");
                            //Verify Districts Club Page is displayed
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathLabelCaption, "District Clubs");


                            // Verify Delete functionality with Clubname Exists after deletion

                            // Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            //Create new Club
                            ClubName = utilityMethods.AddNewclub();
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName,ClubName,"Club Name");
                            // Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave,"Save");
                            // Verify visibility of  buttons
                            utilityMethods.VerifyButtonExists("Save", PageObjects.idBtnSave);
                            utilityMethods.VerifyButtonExists("Delete", PageObjects.idBtnDelete);
                            utilityMethods.VerifyButtonExists("Cancel", PageObjects.idBtnCancel);
                            // Click Delete button
                            utilityMethods.ClickById(PageObjects.idBtnDelete,"Delete");
                            // Validate the Club name to be deleted is displayed
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathClubText,ClubName);
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave,"Save");
                            //Verify Districts Club Page is displayed
                            utilityMethods.VerifyTextDisplay(PageObjects.xpathLabelCaption, "District Clubs");
                            //Navigating to the Club status
                            utilityMethods.FindDesiredClub("All Pending");
                            //Verify Club is deleted
                            utilityMethods.VerifyDeleteApplication(ClubName);



                            //New Club creation and deletion
                            //Click Add Club link
                            utilityMethods.ClickById(PageObjects.idAddClub, "Add Club");
                            // Passing the Club name to the text field
                            utilityMethods.EnterValueById(PageObjects.idClubName,ClubName,"Club Name");
                            //Click Save button
                            utilityMethods.ClickById(PageObjects.idBtnSave,"Save");
                            //Verify visibility  of button
                            utilityMethods.VerifyButtonExists("Save", PageObjects.idBtnSave);
                            utilityMethods.VerifyButtonExists("Delete", PageObjects.idBtnDelete);
                            utilityMethods.VerifyButtonExists("Cancel", PageObjects.idBtnCancel);
                            //Click Cancel button
                            utilityMethods.ClickById(PageObjects.idBtnCancel, "Cancel");
                            //Navigating to the Club status
                            utilityMethods.FindDesiredClub("All Pending");


                            // Verify Fields are editable
                            //Navigating to the Desired Application

                            utilityMethods.ViewApplication(ClubName);

                            //Verify the Club Name field is editable
                            utilityMethods.VerifyFieldEdit("Club Name", PageObjects.idClubName);

                            //Verify the Club Type Dropdown is editable
                            utilityMethods.VerifyDropdownEdit("Club Type", PageObjects.ddlClubType);

                            //Verify the City field is editable 
                            utilityMethods.VerifyFieldEdit("City", PageObjects.idClubCity);

                            //Verify the Club language Dropdown  is editable
                            utilityMethods.VerifyDropdownEdit("Club Language", PageObjects.ddlclubLang);

                            //Verify the Sponsoring Club button is enabled
                            utilityMethods.VerifyButtonExists("Sponsoring Club", PageObjects.sponsoringClub);

                            //Verify New Club Officers fields are editable
                            utilityMethods.NewClubOfficersChk();

                            //Verify Estimate of Chambers fields are editable
                            utilityMethods.ECMforLionsClub();

                            //Verify the Checkbox is visible for Club criteria
                            utilityMethods.VerifyCheckboxExists("Club Criteria", PageObjects.idClubCriteria);

                            //Verify the Comments text area  is editable
                            utilityMethods.VerifyFieldEdit("Comments", PageObjects.idComments);

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




















