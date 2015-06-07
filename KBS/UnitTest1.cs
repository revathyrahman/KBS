using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace MyLCIAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void TestMethod1()
        {
            
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://mylcibeta.lionsclubs.org/");
            driver.FindElement(By.Id("PageContent_Login1_txtUsername")).SendKeys("4141201");
            driver.FindElement(By.Id("PageContent_Login1_txtPassword")).SendKeys("password0");
            driver.FindElement(By.Id("PageContent_Login1_btnSubmit")).Click();



        public void FieldChk()
        {
            UtilityMethods utilityMethods;
            DataInputProvider dataInputProvider = new DataInputProvider();
            List<List<String>> data = dataInputProvider.GetInputData("Login");

            for (int i = 0; i < data.Count; i++)
            {
                utilityMethods = new UtilityMethods("Cancel and  Delete and Field lvl chk", i);
                if (data[i][0].Equals("LCI"))
                {
                    utilityMethods.InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");

                    utilityMethods.LoginMyLCI(data[i][0], data[i][1], data[i][2]);

                    utilityMethods.ClickById("a_3_1_28");

                    utilityMethods.ClickById("a_3_2_40");

                    //Verify  Cancel Request

                    utilityMethods.ClickById("hlAddClub");
                   utilityMethods.ClickById("btnCancel");
                   utilityMethods.VerifyTextDisplay(".//*[@id='lblCaption']", "District Clubs");

                    // Verify Delete with Clubname

                    utilityMethods.ClickById("hlAddClub");
                   utilityMethods.EnterValueById("txtClubName", "Club-Jeynew");
                   utilityMethods.ClickById("btnSave");
                   utilityMethods.VerifyButtonExists("Save", "btnSave");
                   utilityMethods.VerifyButtonExists("Delete", "btnDelete");
                   utilityMethods.VerifyButtonExists("Cancel", "btnCancel");

                   utilityMethods.ClickById("btnDelete");
                   utilityMethods.VerifyTextDisplay(".//*[@id='form1x']/div[4]/div[1]/div[5]/div[1]/div[1]/div/div[6]/div[2]/div/div", "Club-Jeynew");
                   utilityMethods.ClickById("btnSave");
                   utilityMethods.VerifyTextDisplay(".//*[@id='lblCaption']", "District Clubs");
                    utilityMethods.FindDesiredClub("All Pending");
                    utilityMethods.VerifyDeleteApplication("Club-Jeynew");
                    //New Club creation and deletion

                    utilityMethods.ClickById("hlAddClub");
                    utilityMethods.EnterValueById("txtClubName", "club-Jeynew");
                    utilityMethods.ClickById("btnSave");
                    utilityMethods.VerifyButtonExists("Save", "btnSave");
                    utilityMethods.VerifyButtonExists("Delete", "btnDelete");
                    utilityMethods.VerifyButtonExists("Cancel", "btnCancel");

                    //Once again to delete the existing club
                    utilityMethods.ClickById("btnDelete");
                    utilityMethods.ClickById("btnSave");
                    utilityMethods.VerifyTextDisplay(".//*[@id='lblCaption']", "District Clubs");
                    utilityMethods.FindDesiredClub("All Pending");


                    // Verify Field level Edit

                    utilityMethods.ViewApplication("Alpha club");
                    utilityMethods.VerifyFieldEdit("Club Name","txtClubName");
                    utilityMethods.VerifyDropdownEdit("Club Type", "ddlClubType");
                    utilityMethods.VerifyFieldEdit("City", "txtCity");
                    utilityMethods.VerifyDropdownEdit("Club Language", "ddlClubLanguage");
                    utilityMethods.VerifyButtonExists("Sponsoring Club", "btnSelectSponsoringClub");
                    utilityMethods.NewClubOfficersChk();
                    utilityMethods.ECMforLionsClub();
                    utilityMethods.VerifyCheckboxExists("Club Criteria", "cbReadNewClubCriteria");
                    utilityMethods.VerifyFieldEdit("Comments", "txtNewClubAppComment");
                                                                         


                    utilityMethods.LogoutMyLCI();

                    utilityMethods.CloseApplication();
                }
            }

        }
    }
}
