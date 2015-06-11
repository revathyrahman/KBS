using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Collections.Concurrent;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;


namespace MyLCIAutomation
{
    public class UtilityMethods
    {
        IWebDriver browserDriver;
        int snapShotCount = 1;
        private string testCaseName;
        public string screenShotFlag;
        ExcelReporter excelReporter = new ExcelReporter();
        ExcelReporterAuthorization excelReporterAuth = new ExcelReporterAuthorization();
        private int dataSet
        {
            get;
            set;
        }
       
        public UtilityMethods(string testCaseName, int dataSet)
        {
            this.testCaseName = testCaseName;
            this.dataSet = dataSet;
        }

        public UtilityMethods(string testCaseName, string screenshotFlag)
        {
            this.testCaseName = testCaseName;
            this.screenShotFlag = screenshotFlag;
        }

         public UtilityMethods(string testCaseName, int dataSet,string screenshotFlag)
        {
            this.testCaseName = testCaseName;
            this.dataSet = dataSet;
            this.screenShotFlag = screenshotFlag;
        }


         ///<summary>
         ///This method is created to Login to MyLCI Application
         ///</summary>
         ///<remarks>
         ///Takes three arguments and returns true if the login is successful and false if its is not
         ///</remarks>
         ///
        public Boolean LoginMyLCI(String Role, String UserId, String Password)
        {
            Boolean flag = false;

            try
            {
                // Enter user name
<<<<<<< HEAD
                EnterValueById(PageObjects.idLoginUserName, UserId);
=======
                EnterValueById(PageObjects.idLoginUserName,UserId);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                // Enter password
                EnterValueById(PageObjects.idLoginPassword, Password);

                // Click login button
                ClickById(PageObjects.btnLogin);

                if (browserDriver.FindElement(By.LinkText(PageObjects.linkTextMyLCIHome)).Displayed)
                {
                    excelReporter.ReportStep("Login is Successful for Userid: " + UserId, "Pass");
                    excelReporterAuth.ReportStep("User id: " + UserId + " is Successful", "SUCCESS");
                    flag = true;
                }
                else
                {
                    excelReporter.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
                    excelReporterAuth.ReportStep("User id: " + UserId + " is UnSuccessful", "FAILURE");
                    flag = false;
                   
                }
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
             return flag;
        }

        ///<summary>
        ///This method is created to Check if Add Club Link Exists for a logged in user
        ///</summary>
        ///<remarks>
        ///Takes one argument and returns true if the Link exists and false if link does not exist 
        ///</remarks>
        public Boolean VerifyAddClubLinkExists(String LinkText)
        {
            Boolean flag = true;
            try
            {

                browserDriver.FindElement(By.Id(PageObjects.mnuLinkDistricts)).Click();                               
                Thread.Sleep(1000);
                browserDriver.FindElement(By.Id(PageObjects.subMenuLinkClubs)).Click();
                
                if (browserDriver.FindElement(By.LinkText(PageObjects.linkTextAddClub)).Enabled)
                {
                    flag = true;
                
                }
                else
                {
                    flag = false;
                    
                }
            }
            catch (NoSuchElementException e)
            {
                e.StackTrace.ToString();
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag); 
            }
            return flag;
      }
<<<<<<< HEAD
      

        public Boolean FindClubs()
        {
            Boolean flag = false;
            try
            {
                browserDriver.FindElement(By.Id(PageObjects.mnuLinkDistricts)).Click();
                browserDriver.FindElement(By.Id(PageObjects.subMenuLinkClubs)).Click();

                if (browserDriver.FindElement(By.Id(PageObjects.lblSearchOption)).Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("Find Clubs Link exists", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("Find Clubs Link does not exist", "Pass");
                }
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element FindClubs does not Exist", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
            return flag;
        }
       public bool invokeBrowser(string browserType)
       {
=======
       /// <summary>
       /// This method is for invoking the application
       /// </summary>
       /// <remarks>
       /// Takes  two arguments browser and url returns browser driver based on the browser parameterized
       /// </remarks>
      public IWebDriver InvokeApplication(String browser, String url)
      {
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
          excelReporter.CreateReportHeader();
          excelReporterAuth.CreateReportHeader();
          bool browserStatus = false;
          
          try
          {
             if (browserType.ToLower().Equals("ie"))
             {
                //Code to invoke IE Browser
                 browserDriver = new InternetExplorerDriver();
                 excelReporter.ReportStep("IE Browser invoked Successfully", "SUCCESS");
                 browserStatus = true;
             }
             else if (browserType.ToLower().Equals("chrome"))
             {
                //Code to invoke Chrome browser 
                 browserDriver = new ChromeDriver();
                 excelReporter.ReportStep("Chrome Browser invoked Successfully", "SUCCESS");
                 browserStatus = true;
             }
             else if (browserType.ToLower().Equals("firefox"))
             {
                 //Code to invoke Firefox browser 
                 browserDriver = new FirefoxDriver();
                 excelReporter.ReportStep("Firefox Browser invoked Successfully", "SUCCESS");
                 browserStatus = true;
             }
             else
             {
                 browserStatus = false;
             }
<<<<<<< HEAD
             browserDriver.Manage().Window.Maximize();
             WebDriverWait wait = new WebDriverWait(browserDriver, TimeSpan.FromSeconds(10));
         }
          catch
=======
             return browserDriver;
        }


        /// <summary>
        /// This method is verify the Language listed in the dropdownlist of Add club form
        /// </summary>
        ///<remarks>
        /// Takes no arguments if languages count is 12 returns true  and  false if language count is not 12
        /// </remarks>
         
      public void VerifyLanguageListAddClub()
      {
          IWebElement ele = browserDriver.FindElement(By.Id("ddlClubLanguage"));
          IList<IWebElement> options= ele.FindElements(By.TagName("option"));
          if (options.Count() == 12)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
          {
              excelReporter.ReportStep("The Browser could not be invoked", "FAILURE");
          }
          return browserStatus;       
    }
       /// <summary>
       /// This method is for invoking the application
       /// </summary>
       /// <remarks>
       /// Takes  one argument ApplicationURL ; No return values
       /// </remarks>
       /// 
    public void InvokeApplication(String url)
    {
        try
        {
             browserDriver.Navigate().GoToUrl(url);
            
             excelReporter.ReportStep("Application invoked successfully", "SUCCESS");
          }
          catch (WebDriverException exception)
          {
             excelReporter.ReportStep("Driver could not be found", "FAILURE");
          }
      }
<<<<<<< HEAD

    /// <summary>
    /// This method is verify the Language listed in the dropdownlist of Add club form
    /// </summary>
    ///<remarks>
    /// Takes no arguments if languages count is 12 ; no return value
    /// </remarks>
         
    public void VerifyLanguageListAddClub()
    {
        IWebElement ele = browserDriver.FindElement(By.Id(PageObjects.ddlclubLang));
        IList<IWebElement> options = ele.FindElements(By.TagName(PageObjects.ulLanguageOptions));
        if (options.Count() == 12)
        {
            excelReporter.ReportStep("12 languages are listed", "PASS");
            int languageCount = 1;
            foreach (IWebElement option in options)
            {

                if (languageCount <= 12)
                {
                    string languages = option.Text;
                    excelReporter.ReportStep("Language" + languageCount + "= " + languages.ToString(), "SUCCESS");

                }
                languageCount = languageCount + 1;
            }
        }
        else
        {
            excelReporter.ReportStep("12 Languages are not listed", "FAILURE");
        }
    }
    /// <summary>
    /// This method is to send values to the text field
    /// </summary>
    /// <remarks>
    /// Takes two arguments id and value returns true on passing value to the text field and false if it doesn't
    /// </remarks>
        public void EnterValueById(String id, String value)
=======
        /// <summary>
        /// This method is to send values to the text field
        /// </summary>
        /// <remarks>
        /// Takes three arguments id , value and fielddesc  returns true on passing value to the text field and false if it doesn't
        /// </remarks>
        public void EnterValueById(String id, String value,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Id(id)).SendKeys(value);
                excelReporter.ReportStep("Element with id :" + id +  "of" +fielddesc+ " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                excelReporter.ReportStep("Element with id :" + id + "of" + fielddesc + " could not be found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
        }
        /// <summary>
        /// This method is to send values to the text field
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments name and value and  no return value
=======
        /// Takes two arguments name and value returns true on passing value to the text field and false if it doesn't 
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// </remarks>
        public void EnterValueByName(String name, String value)
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Name(name)).Clear();
                browserDriver.FindElement(By.Name(name)).SendKeys(value);
                flag = true;
                excelReporter.ReportStep("Element with name :" + name + " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                flag = false;
                excelReporter.ReportStep("Element with name :" + name + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exception)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to select the value by index in the dropdown list
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments id and index and no return value
        /// </remarks>
        
        public void SelectDropdownValueByIndex(String id, int index)
=======
        /// Takes three arguments id, index,fielddesc returns true on selection of value in dropdown list and returns false  if it doesn't
        /// </remarks>
        public void SelectDropdownValueByIndex(String id, int index,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = true;
            try
            {
                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByIndex(index);
                flag = true;
                excelReporter.ReportStep("Element with id :" + id + "of" + fielddesc + " is found and index :" + index + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                flag = false;
                excelReporter.ReportStep("Element with id :" + id + "of" + fielddesc + " could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to select the value by text in the dropdown list
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments id and visibletext and  no return value
        /// </remarks>
        
        public void SelectDropdownValueByVisibleText(String id, String VisibleText)
=======
        /// Takes two arguments id ,visibletext and fielddesc returns true on selection of value in dropdown list and returns false  if it doesn't
        /// </remarks>
        public void SelectDropdownValueByVisibleText(String id, String VisibleText,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = false;
            try
            {

                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByText(VisibleText);
                flag = true;
                excelReporter.ReportStep("Element with id :" + id + "of" + fielddesc + " is found and Visible Text :" + VisibleText + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                excelReporter.ReportStep("Element with id :" + id + "of" + fielddesc + " could not be found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method takes snapshot in png format
        /// </summary>
        /// <remarks>
        /// Takes two arguments screenshot flag and flag returns image
        /// </remarks>
        public void TakeSnapshot(string screenshotflag, Boolean flag)
        {
            try
            {
                if (((flag==false) & (screenshotflag.Contains("FAILED")) | screenShotFlag.Contains("ALL")))
                {
                    Screenshot ss = ((ITakesScreenshot)browserDriver).GetScreenshot();
                    ss.SaveAsFile(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\Screenshots\\" + "Snap-" + snapShotCount + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    snapShotCount++;
                }
                

            }
            catch (IOException ioe)
            {
                // TODO Auto-generated catch block
                excelReporter.ReportStep("Unable to copy the file", "FAILURE");
            }
        }
        /// <summary>
        /// This method is for closing the application
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and  no return value
=======
        /// Takes no arguments returns true on success
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// </remarks>
        public void CloseApplication()
        {
            try
            {
                browserDriver.Quit();
                excelReporter.ReportStep("The application is closed successfully..", "SUCCESS");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be closed for unknown reason !!!", "FAILURE");
            }

            excelReporter.FlushWorkbook(testCaseName + "-Run" + dataSet);
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is for closing the application
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes one  argument testcase name and no return value
        /// </remarks>
        
=======
        /// Takes one  arguments returns true on success
        /// </remarks>
       
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void CloseApplication(string testcasename)
        {
            try
            {
                browserDriver.Quit();
                excelReporter.ReportStep("The application is closed successfully..", "SUCCESS");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be closed for unknown reason !!!", "FAILURE");
            }

            excelReporter.FlushWorkbook(testCaseName + "OnlyRun");
        }

        /// <summary>
        /// This method is to verify element is clicked using CSS
        /// </summary>
        /// <remarks>
        /// This takes one argument CSS and  no return value
        /// </remarks>

<<<<<<< HEAD
          public void ClickByCSS(string css)
=======

   
        /// <summary>
        /// This method is to verify element is clicked using CSS
        /// </summary>
        /// <remarks>
        /// This takes two argument CSS and fielddesc and returns true on element is clicked and returns false if it doesn't
        /// </remarks>
        public void ClickByCSS(String css,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.CssSelector(css)).Click();
                excelReporter.ReportStep("Element with css :" + css + "of" + fielddesc + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with css :" + css + "of" + fielddesc + " could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

          /// <summary>
          /// This method is to verify  the element is present
          /// </summary>
          /// <remarks>
          /// Takes one argument Xpath and returns true on element present and false if it doesn't
          /// </remarks>
          
        public Boolean VerifyElementExists(string XPath)
=======
        /// <summary>
        /// This method is to verify  the element is present
        /// </summary>
        /// <remarks>
        /// Takes two argument Xpath and fielddesc and returns true on element present and false if it doesn't
        /// </remarks>
        public Boolean VerifyElementExists(String XPath,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = false;
            try
            {
                if (browserDriver.FindElement(By.XPath(XPath)).Displayed)
                    flag = true;
                else
                    flag = false;

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with XPath :" + XPath + "of" + fielddesc + " could not be found..", "FAILURE");
            }

            return flag;
        }

<<<<<<< HEAD
        /// <summary>
        /// This method is to click on the text
        /// </summary>
        /// <remarks>
        /// Takes one argument text and returns nothing
        /// </remarks>

        public void LinkClickByText(string text)
=======
       /// <summary>
       /// This method is to click on the text
       /// </summary>
       /// <remarks>
       /// Takes one argument text and returns true on element is clicked and false if it doesn't
       /// </remarks>
        public void LinkClickByText(String text)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag=true;
            try
            {
                browserDriver.FindElement(By.LinkText(text)).Click();
                excelReporter.ReportStep("Element with text :" + text + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with text :" + text + "could not be found..", "FAILURE");
                flag=false;
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
                flag=false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
        }

<<<<<<< HEAD
=======

>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to click based on the Xpath
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes one argument Xpath and  no return value
        /// </remarks>
        
        public void ClickByXPath(string XPath)
=======
        /// Takes two argument Xpath and fielddesc and returns true on element is clicked and false if it doesn't
        /// </remarks>
        
        public void ClickByXPath(String XPath,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.XPath(XPath)).Click();
                excelReporter.ReportStep("Element with XPath :" + XPath + "of" + fielddesc + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with XPath :" + XPath + "of" + fielddesc + " could not be found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
        }

        /// <summary>
        /// This method is to click based on  Id
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes one argument text and  no return value
        /// </remarks>
        
        public void ClickById(string Id)
=======
        /// Takes two argument text and fielddesc and returns true on element is clicked and false if it doesn't
        /// </remarks>
      
        public void ClickById(String Id,string fielddesc)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Id(Id)).Click();
                excelReporter.ReportStep("Element with ID :" + Id + " of" + fielddesc + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with ID :" + Id + " of" + fielddesc + " could not be found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found !!!", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

        /// <summary>
        /// This method creates a new Club form and enters value to the fields and save
        /// </summary>
        ///<remarks>
        ///Takes no arguments returns the clubName Created
        /// </remarks>
=======
      /// <summary>
      /// This method creates a new Club form and enters value to the fields and save
      /// </summary>
      ///<remarks>
      ///Takes no arguments returns true on club form is saved and false if it doesn't
      /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public string AddClubFormEntry()
        {
            //Create a random Number to append to ClubName for unique
            Random rdmNo = new Random();
            int randnum = rdmNo.Next(1000);
            
            string ClubName = "Club_" + randnum;
            Boolean flag = true;
            try
            {
                
                //Enter ClubName - Unique
<<<<<<< HEAD
                EnterValueById(PageObjects.idClubName, ClubName);

                //Select Club Type
                SelectDropdownValueByVisibleText(PageObjects.ddlClubType, "Lions Club");

                //Enter Club City 
                EnterValueById(PageObjects.idClubCity, "Automation TestCity");


                //Select from Club Language
                SelectDropdownValueByVisibleText(PageObjects.ddlclubLang, "English");


                // Select from Club Language
                SelectDropdownValueByVisibleText(PageObjects.ddlclubLang, "English");
=======
                EnterValueById("txtClubName", ClubName,"Club Name");

                //Select Club Type
                SelectDropdownValueByVisibleText("ddlClubType", "Lions Club","Club Type");

                //Enter Club City 
                EnterValueById("txtCity", "Automation TestCity","City");


                //Select from Club Language
                SelectDropdownValueByVisibleText("ddlClubLanguage", "English","Club Language");

                //Click for a sponsoring club
                ClickById("btnSelectSponsoringClub","Sponsoring Club");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath("//div[@class='DistrictClubResults']/div/div/div[1]","District Club results");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

               // Select from Club Language
                SelectDropdownValueByVisibleText("ddlClubLanguage", "English","Club Language");
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                VerifyLanguageListAddClub();

                //Click for a sponsoring club
<<<<<<< HEAD
                ClickById(PageObjects.btnSponsoringClub);
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath(PageObjects.xpathListSponsoringClubs);
=======
                ClickById("btnSelectSponsoringClub","Sponsoring Club");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath("//div[@class='DistrictClubResults']/div/div/div[1]","District Club results");
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
                                                                       
                //Enter New Club President creation details
<<<<<<< HEAD
                ClickByXPath(PageObjects.xpathClubPresidentPanel);
                EnterValueById(PageObjects.idPresidentFirstname, "PresidentFirstname");
                EnterValueById(PageObjects.idPresidentLastname, "PresidentLastname");
                EnterValueById(PageObjects.idPresidentYOB, "1980");
                SelectDropdownValueByVisibleText(PageObjects.idPresidentGender, "Male");
                EnterValueById(PageObjects.idPresidentEmailaddress, "president@test.com");

                //Enter New Club Secretary Creation details
                ClickByXPath(PageObjects.xpathClubSecretaryPanel);
                EnterValueById(PageObjects.idSecretaryFirstname, "SecretaryFirstName");
                EnterValueById(PageObjects.idSecretaryLastname, "SecretaryLastName");
                EnterValueById(PageObjects.idSecretaryYOB, "1980");
                SelectDropdownValueByVisibleText(PageObjects.idSecretaryGender, "Female");
                EnterValueById(PageObjects.idSsecretaryEmailaddress, "testsecretary@test.com");

                //Enter Charter Member details
                EnterValueById(PageObjects.idNewMemberscount, "20");
                EnterValueById(PageObjects.idTransferMemberscount, "0");
                EnterValueById(PageObjects.idStudentMemberscount, "0");
                EnterValueById(PageObjects.idLeoLionscount, "0");

                //Check New Club Criteria checkbox
                ClickById(PageObjects.idNewClubCriteria);
                //Enter Comment
                EnterValueById(PageObjects.idComments, "test comment");

                //Click on Save
                ClickById(PageObjects.idBtnSave);
=======
                ClickByXPath("//*[@id='pnlNewClubPresidentHeader']/div/b","President Header");
                EnterValueById("txtPresidentFirstName", "PresidentFirstname","President FirstName");
                EnterValueById("txtPresidentLastName", "PresidentLastname","President Lastname");
                EnterValueById("txtPresidentYearOfBirth", "1980","Year of Birth");
                SelectDropdownValueByVisibleText("ddlPresidentGender", "Male","Gender");
                EnterValueById("txtPresidentEmailAddress", "president@test.com","Email Address");

                //Enter New Club Secretary Creation details
                ClickByXPath("//*[@id='pnlNewClubSecretaryHeader']/p/b","Secretary Header");
                EnterValueById("txtSecretaryFirstName", "SecretaryFirstName","Secretary FirstName");
                EnterValueById("txtSecretaryLastName", "SecretaryLastName","Secretary LastName");
                EnterValueById("txtSecretaryYearOfBirth", "1980","Year of Birth");
                SelectDropdownValueByVisibleText("ddlSecretaryGender", "Female","Gender");
                EnterValueById("txtSecretaryEmailAddress", "testsecretary@test.com","Email Address");

                //Enter Charter Member details
                EnterValueById(PageObjects.newMemberscount, "2"," New Members count");
                EnterValueById(PageObjects.transferMemberscount, "0","Transfer Members count");
                EnterValueById("txtStudentCount", "0"," Students count");
                EnterValueById("txtLeoLionCount", "0","Leo Lions Count");

                //Check New Club Criteria checkbox
                ClickById("cbReadNewClubCriteria","Club Criteria");
                //Enter Comment
                EnterValueById("txtNewClubAppComment", "test comment","Comments");

                //Click on Save
                ClickById("btnSave","Save");
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                browserDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(2));

            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
            return ClubName;
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is for logout of the application
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no argument and no return value
        /// </remarks>

=======
        /// Takes no argument and returns true on success and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void LogoutMyLCI()
        {
            Boolean flag=true;
            try
            {
                LinkClickByText(PageObjects.linkTextLogout);

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with Link Logout could not be found", "FAILURE");
                flag=false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// This method is to verify the task is available in the tasks list and clicks on the task 
        /// </summary>
        /// <remarks>
        /// Takes one argument and no return value
        /// </remarks>
        /// 
        public void VerifyMyTask(string taskName)
=======
      /// <summary>
      /// This method is to verify the task is available in the tasks list and clicks on the task 
      /// </summary>
      /// <remarks>
      /// Takes one argument and  returns true on task found and clicked and false if it doesn't
      /// </remarks>
        public void VerifyMyTask(String taskname)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag=true;
            try
            {
                IList<IWebElement> listOfTasks = browserDriver.FindElements(By.XPath(PageObjects.xpathTaskList));
                
                int clubCount=0;
                foreach (IWebElement tlist in listOfTasks)
                {
                    string listValue = tlist.Text;
                    if(listValue.Contains(taskName))
                    {
                        tlist.Click(); 
                        excelReporter.ReportStep("Pending Authorization task is clicked successfully", "SUCCESS");
                        break;
                    }
                    clubCount++;
                }

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
                flag=false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag=false;

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
        }
<<<<<<< HEAD

        /// <summary>
        /// This method is to discontinue the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns nothing
        /// </remarks>
        /// 
=======
        /// <summary>
        /// This method is to discontinue the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns true on discontinue club is successful and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void DiscontinueClub(string ClubName)
        {
            try
            {
<<<<<<< HEAD
                ClickById(PageObjects.idDiscontinueClubchkbox);
                EnterValueById(PageObjects.idDiscontinueNote, "Test Comment");
                ClickById(PageObjects.idBtnSave);
                string ConfirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;
=======
                ClickById("cbStatusAction_Discontinue","Discontinue");
                EnterValueById("txtDiscontinueNote", "Test Comment","Comments");
                ClickById("btnSave","Save");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                if (ConfirmationMessage.Contains("Discontinued"))
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is Discontinued", "Pass");
                }
                else
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is not Discontinued", "Fail");
                }
<<<<<<< HEAD
                LinkClickByText(PageObjects.linkTextGotoApplication);
                Thread.Sleep(7000);
=======
                LinkClickByText("Go to Application");
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }
        }
        /// <summary>
        /// This method is to click on the Club application and view the application
        /// </summary>
       ///<remarks>
       ///Takes one argument Clubname searches for the club name in the list and on found the application is clicked on success and returns false if it doesnot exist
       /// </remarks>

        /// <summary>
        /// This method is to click on the Club application and view the application
        /// </summary>
        ///<remarks>
        ///Takes one argument Clubname searches for the club name in the list and on found the application is clicked on success and returns false if it doesnot exist
        /// </remarks>
        
        public void ViewApplication(String clubname)
        {
            Boolean flag=true;
            try
            {
                IList<IWebElement> clubList = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));
                IList<IWebElement> viewAppList = browserDriver.FindElements(By.XPath(PageObjects.xpathViewAppList));
                int countClubList= clubList.Count();     

<<<<<<< HEAD
                int clubCount = 0;
                foreach (IWebElement clubNames in clubList)
=======
                //IList<IWebElement> clublist = driver.FindElements(By.XPath("//div[@class='gridData']/div/div/div/div"));
                IList<IWebElement> clublist = browserDriver.FindElements(By.XPath("//div[@class='gridHeader']/div/div/div[1]"));
                IList<IWebElement> viewApplist = browserDriver.FindElements(By.XPath("//a[.='View Application']"));
                // int Countclublist= clublist.Count();     

                int val = 0;
                foreach (IWebElement clnames in clublist)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                {
                    Thread.Sleep(1000);
                    string clubValue = clubNames.Text;

                    if (clubValue.Contains(clubname))
                    {
                        viewAppList[clubCount].Click();
                        excelReporter.ReportStep("View Application is clicked successfully", "SUCCESS");
                        break;
                    }
<<<<<<< HEAD
                    clubCount++;

                    if (clubCount == countClubList)
                    {
                        IWebElement pagination = browserDriver.FindElement(By.XPath(PageObjects.xpathClubListNextPage));
                        pagination.Click();
                        Thread.Sleep(5000);
                        clubList = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));

                    }
=======
                    val++;
                    //if (val == Countclublist)
                    //{
                      //  IWebElement pagnation = browserDriver.FindElement(By.XPath(".//*[@id='myGrid']/div[2]/div[3]/a[2]/img"));
                      //  pagnation.Click();
                    //}
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                }

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
                flag=false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag=false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }
        }
<<<<<<< HEAD

        /// <summary>
        /// This method is to remove from discontinuing the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns nothing
        /// </remarks>
        
=======
        /// <summary>
        /// This method is to remove from discontinuing the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns true on removing from discontinued club is successful and false if it doesn't
        /// </remarks>
       
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void ContinueClub(string ClubName)
        {
            Boolean flag = true;
            try
            {
<<<<<<< HEAD
                ClickById(PageObjects.idContinueClub);
                EnterValueById(PageObjects.idDiscontinueNote, "Test Comment");
                ClickById("btnSave");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;
=======
                ClickById("cbStatusAction_Continue","Continue");
                EnterValueById("txtDiscontinueNote", "Test Comment","Comments");
                ClickById("btnSave","Save");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                if (ConfirmationMessage.Contains("removed from Discontinued"))
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is removed from Discontinued", "Pass");
                }
                else
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is not removed from Discontinued", "Fail");
                }
                LinkClickByText(PageObjects.linkTextGotoApplication);
            }
            catch (Exception e)
            {
                flag = false;
                e.StackTrace.ToString();
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

        /// <summary>
        /// This method is to  verfiy the club is moved to next status on submit
        ///<remarks>
        ///Takes one argument CurrentStatus returns nothing
        /// </remarks>
        
=======
        /// <summary>
        /// This method is to  verfiy the club is moved to next status on submit
        ///<remarks>
        ///Takes one argument CurrentStatus returns true on submit the application to next status and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void MoveClubtoNextStatus(String CurrentStatus)
        {
            Boolean flag = true;
            try
            {
<<<<<<< HEAD
                ClickById(PageObjects.idNewClubCriteria);
                ClickById(PageObjects.idBtnSubmitSelection);
                Thread.Sleep(5000);
                ClickById("btnSave");
                Thread.Sleep(5000);
                string confirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;
=======
                ClickById("cbReadNewClubCriteria","Club Criteria");
                ClickById("cbStatusAction_Submit","Submit");
                ClickById("btnSave","Save");
                string confirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                if (confirmationMessage.Contains("District Governor authorization"))

                    excelReporter.ReportStep("Club application moved to DG Authorization status", "Pass");

                else
                    flag = false;
                    excelReporter.ReportStep("Club application not moved to DG Authorization status", "Pass");

                LinkClickByText(PageObjects.linkTextGotoApplication);
            }

            catch (NoSuchElementException e)
            {
                flag = false;
                e.StackTrace.ToString();
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

=======
                
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// Thsi method to view the status of the clubs listed and click on the desired club
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes one argument Filtername returns nothing
        /// </remarks>
        
=======
        /// Takes one argument Filtername returns true on click of the desired status of the club and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void FindDesiredClub(String Filtername)
        {
            Boolean flag=true;
            try
            {
<<<<<<< HEAD

                ClickById(PageObjects.lblSearchOption);
                 IList<IWebElement> findClubList = browserDriver.FindElements(By.XPath(PageObjects.xpathFindClubQuickViewList));
=======
                
                ClickById("lblSearchOptionTitle","Find Clubs");
                 IList<IWebElement> findlist = browserDriver.FindElements(By.XPath("//*[@id='pnlQuickViews']/li/a"));
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                
                int val = 0;
                foreach (IWebElement findnames in findClubList)
                {
                    string findvalue = findnames.Text;

                    if (findvalue.Contains(Filtername))
                    {
                        findnames.Click();
                        excelReporter.ReportStep(findvalue+" is clicked successfully", "SUCCESS");
                        break;
                    }
                    val++;
                }

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
                flag=false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag=false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag);
            }

                
            }

        /// <summary>
        /// /This method to verify the text field is editable
        /// </summary>
        ///<remarks>
<<<<<<< HEAD
        ///Takes two arguments Fielddesc and id and returns nothing
        /// </remarks>
        
        public void VerifyFieldEdit(string Fieldname, string id)
=======
        ///Takes two arguments Fielddesc and id and returns true if field is editable and false if it doesn't
        /// </remarks>
        public void VerifyFieldEdit(string Fielddesc, string id)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = false;
            try
            {
                if (browserDriver.FindElement(By.Id(id)).Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("The" + Fielddesc + " is Editable", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("The" + Fielddesc + " is  not Editable", "Fail");
                }

            }
            catch (NoSuchElementException e)
            {
                flag = false;        
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify the dropdownlist is editable
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments Fielddesc and id returns nothing
        /// </remarks>
        /// 
        public void VerifyDropdownEdit(string Fieldname, string id)
=======
        /// Takes two arguments Fielddesc and id returns true if the dropdown list is editable and false if it doen't
        /// </remarks>
        public void VerifyDropdownEdit(string Fielddesc, string id)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = false;
            try
            {
                IWebElement element = browserDriver.FindElement(By.Id(id));
                IList<IWebElement> opts = element.FindElements(By.XPath(PageObjects.xpathDdlOptions));
                foreach (IWebElement opt in opts)
                {
                    if (opt.Enabled)
                    {
                        flag = true;
                        excelReporter.ReportStep("The" + Fielddesc + "is Editable", "Pass");
                        break;
                     }
                    else
                    {
                        flag = false;
                        excelReporter.ReportStep("The" + Fielddesc + "is  not Editable", "Fail");
                    }

                }
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
         }

<<<<<<< HEAD
=======

        }
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify button is enabled
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments Fileddesc and id returns nothing
        /// </remarks>
        
        public void VerifyButtonExists(string Fieldname, string id)
=======
        /// Takes two arguments Fileddesc and id returns true on button is enabled and false if it doesn't
        /// </remarks>
        public void VerifyButtonExists(string Fielddesc, string id)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
            Boolean flag = false;
            try
            {
                IWebElement button = browserDriver.FindElement(By.Id(id));

                if (button.Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("The" + Fielddesc + " button is Enabled", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("The" + Fielddesc + "  button is not Enabled", "Fail");
                }

            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify the fields are editable in New club officers section
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and returns nothing
        /// </remarks>
        
=======
        /// Takes no arguments and returns true for the fields which are  editable and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void NewClubOfficersChk()
        {
            Boolean flag = true;
            try
            {
                // Verify President fieldlevel check

<<<<<<< HEAD
                ClickByXPath(PageObjects.xpathClubPresidentPanel);
                VerifyFieldEdit("First Name", PageObjects.xpathClubSecretaryPanel);
                VerifyFieldEdit("Last Name", PageObjects.idPresidentFirstname);
                VerifyFieldEdit("Year Of Birth", PageObjects.idPresidentLastname);
                VerifyDropdownEdit("Gender", PageObjects.idPresidentGender);
                VerifyFieldEdit("Email Address", PageObjects.idPresidentEmailaddress);
                VerifyButtonExists("Clear",PageObjects.idPresidentClearbtn);
=======
                ClickByXPath(".//*[@id='pnlNewClubPresidentHeader']/div/b","President Header");
                VerifyFieldEdit("First Name", PageObjects.presidentFirstname);
                VerifyFieldEdit("Last Name", PageObjects.presidentLastname);
                VerifyFieldEdit("Year Of Birth", PageObjects.presidentYOB);
                VerifyDropdownEdit("Gender", PageObjects.presidentGender);
                VerifyFieldEdit("Email Address", PageObjects.presidentEmailaddress);
                VerifyButtonExists("Clear", PageObjects.presidentClearbtn);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                // Verify Secretary Fieldlevel check

<<<<<<< HEAD
                ClickByXPath(PageObjects.xpathClubSecretaryPanel);
                VerifyFieldEdit("First Name", PageObjects.idSecretaryFirstname);
                VerifyFieldEdit("Last Name", PageObjects.idSecretaryLastname);
                VerifyFieldEdit("Year Of Birth", PageObjects.idSecretaryYOB);
                VerifyDropdownEdit("Gender", PageObjects.idSecretaryGender);
                VerifyFieldEdit("Email Address", PageObjects.idSsecretaryEmailaddress);
                VerifyButtonExists("Clear", PageObjects.idsecretaryclearbtn);
=======
                ClickByXPath(".//*[@id='pnlNewClubSecretaryHeader']/p/b","Secretary Header");
                VerifyFieldEdit("First Name", PageObjects.secretaryFirstname);
                VerifyFieldEdit("Last Name", PageObjects.secretaryLastname);
                VerifyFieldEdit("Year Of Birth", PageObjects.secretaryYOB);
                VerifyDropdownEdit("Gender", PageObjects.secretaryGender);
                VerifyFieldEdit("Email Address", PageObjects.secretaryEmailaddress);
                VerifyButtonExists("Clear", PageObjects.secretaryclearbtn);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }

        /// <summary>
        /// This method is to verify for the fields editable in Estimate Charter of Members section while selection of Lions club in the club type
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and returns nothing
        /// </remarks>
        
=======
        /// Takes no arguments and returns true for the fields which are  editable and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void ECMforLionsClub()
        {
            Boolean flag = true;
            try
            {
<<<<<<< HEAD
                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.idStudentMemberscount);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
=======
                //Verify field is editable for Estimate Charter Members of Lions Club
                VerifyFieldEdit("New Members", PageObjects.newMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.transferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.studentMemberscount);
                VerifyFieldEdit("Leo Lions", PageObjects.leoLionscount);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify for the fields editable in Estimate Charter of Members section while selection of University/Campus club in the club type
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and returns nothing
        /// </remarks>
        
=======
        /// Takes no arguments and returns true for the fields which are  editable and false if it doesn't
        /// </remarks>
       
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void ECMForUniversityClub()
        {
            Boolean flag = true;
            try
            {
<<<<<<< HEAD
                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Students Over 30 Years", PageObjects.idStudentsOver30Yrs);
                VerifyFieldEdit("Students 30 Years or younger", PageObjects.idStudentsUnder30yrs);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
=======
                //Verify field is editable for Estimate Charter Members of University/Campus Club
                VerifyFieldEdit("New Members", PageObjects.newMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.transferMemberscount);
                VerifyFieldEdit("Students Over 30 Years", PageObjects.studentsOver30Yrs);
                VerifyFieldEdit("Students 30 Years or younger", PageObjects.studentsUnder30yrs);
                VerifyFieldEdit("Leo Lions", PageObjects.leoLionscount);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag = false;
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }

        }

        /// <summary>
        /// This method is to verify for the fields editable in Estimate Charter of Members section while selection of Leo Lions club in the club type
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and returns nothing
        /// </remarks>
        
=======
        /// Takes no arguments and returns true for the fields which are  editable and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void ECMForLeoLionsClub()
        {
            Boolean flag = true;
            try
            {
<<<<<<< HEAD

                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.idStudentMemberscount);
                VerifyFieldEdit("Young Adults", idYoungAdultscount);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
                
=======
                //Verify field is editable for Estimate Charter Members of Leo Lions Club
                VerifyFieldEdit("New Members", PageObjects.newMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.transferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.studentMemberscount);
                VerifyFieldEdit("Young Adults", PageObjects.youngAdultscount);
                VerifyFieldEdit("Leo Lions", PageObjects.leoLionscount);
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify of the checkbox is visible
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes two arguments label and id returns nothing
        /// </remarks>
        
=======
        /// Takes two arguments label and id returns true on checkbox is present and false if it doesnot exists
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void VerifyCheckboxExists(String label, string id)
        {
            Boolean flag = true;
            try
            {
                IWebElement Chkbox = browserDriver.FindElement(By.Id(id));
                if (Chkbox.Enabled)
                    excelReporter.ReportStep("Check box for" + label + "Is present", "Pass");
                else
                    excelReporter.ReportStep("Check box for" + label + "Is  not present", "Fail");
            }

            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag = false;

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
        /// <summary>
        /// This method is to verify the text is displayed
        /// </summary>
<<<<<<< HEAD
        ///<remarks>
        /// Takes two arguments Xpath and text returns nothing
        /// </remarks>
        
=======
        /// Takes two arguments Xpath and text returns true on text is displayed correct and false if it doesn't
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void VerifyTextDisplay(String Xpath, string text)
        {
            Boolean flag = true;
            try
            {
                IWebElement Element = browserDriver.FindElement(By.XPath(Xpath));
                string Label = Element.Text;

                if (Label == text)
                    excelReporter.ReportStep(text + " is Displayed Successfully", "Pass");
                else
                    excelReporter.ReportStep(text + " is  not Displayed", "Fail");
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }


        }
<<<<<<< HEAD

        public void VerifyTextDisplayById(String Id, string text)
        {
            Boolean flag = true;
            try
            {
                IWebElement Element = browserDriver.FindElement(By.Id(Id));
                string Label = Element.Text;

                if (Label == text)
                    excelReporter.ReportStep(text + " is displayed Successfully", "Pass");
                else
                    excelReporter.ReportStep(text + " is  not Displayed", "Fail");
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify the club application is deleted is not
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes one argument clubname checks for the clubname in the list on the clubname is not present returns nothing
        /// </remarks>
=======
        /// Takes one argument clubname checks for the clubname in the list on the clubname is not present returns true the club name is deleted and false if it doesn't
       /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void VerifyDeleteApplication(String clubname)
        {
            Boolean flag = true;
            try
            {

                //IList<IWebElement> clublist = driver.FindElements(By.XPath("//div[@class='gridData']/div/div/div/div"));
                IList<IWebElement> clublist = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));
                IList<IWebElement> viewApplist = browserDriver.FindElements(By.XPath(PageObjects.xpathViewAppList));

                int val = 0;
                foreach (IWebElement clnames in clublist)
                {
                    string clvalue = clnames.Text;

                    if (clvalue.Equals(clubname))
                    {
                        excelReporter.ReportStep("Clubname is not Deleted Successfully.", "FAILURE");
                        break;
                    }
                    else
                        continue;
                    val++;
                }
                excelReporter.ReportStep("Clubname is  Deleted Successfully.", "SUCCESS");

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
                flag = false;
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
                flag = false;
            }

            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }

<<<<<<< HEAD
=======

>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is verify button is invisible
        /// </summary>
        ///<remarks>
<<<<<<< HEAD
        ///Takes four arguments Fieldname1,id1,Fieldname2,id2 returns nothing
        /// </remarks>
        
=======
        ///Takes four arguments Fieldname1,id1,Fieldname2,id2 returns true on button is invisible and false if button exists
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void VerifyInvisibleButton(string Fieldname1, string id1, string Fieldname2, string id2)
        {
            Boolean flag = false;
            try
            {
                IWebElement button1 = browserDriver.FindElement(By.Id(id1));
                IWebElement button2 = browserDriver.FindElement(By.Id(id2));

                if (button1.Enabled && button2.Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("Delete Button is not Present", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("Delete Button is Present ", "Fail");
                }

            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

        /// <summary>
        /// This method is to verify the message is displayed
        /// </summary>
        ///<remarks>
        ///Takes three arguments Fielddesc,Xpath,Value returns nothing
        ///</remarks>
        
        public void VerifyMessage(String Fieldname,String Xpath,String Value)
=======
        /// <summary>
        /// This method is to verify the message is displayed
        /// </summary>
        ///Takes three arguments Fielddesc,Xpath,Value returns true on message is displayed and false if it doesn't
        public void VerifyMessage(String Fielddesc,String Xpath,String Value)
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        {
             Boolean flag = false;
            try
            {
                string Message = browserDriver.FindElement(By.XPath(Xpath)).Text;
                    if(Message.Contains(Value))
                    {
                      flag = true;
                    excelReporter.ReportStep( Value + " message is displayed Successfully", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep( Value + " message is not displayed ", "Fail");
                }

            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        /// <summary>
        /// This method is to verify the Message is displayed for the club when the count  is more than 10 in the same district
        /// </summary>
        /// <remarks>
<<<<<<< HEAD
        /// Takes no arguments and returns nothing
        /// </remarks>
        
=======
        /// Takes no arguments and returns true if the message displayed and false if it doesn't
        /// </remarks>
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
        public void VerifyMessageclubcount()
        {
           Boolean flag = false;

           try
               {
<<<<<<< HEAD

                   IList<IWebElement> clublist = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));
=======
                   FindDesiredClub("All Pending");
                 IList<IWebElement> clublist = browserDriver.FindElements(By.XPath("//div[@class='gridHeader']/div/div/div[1]"));
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4

                   int Countclublist= clublist.Count();
                   if (Countclublist > 10)
                   {
                       flag = true;
<<<<<<< HEAD
                       VerifyMessage("Clubcount", PageObjects.xpathTenPlusClubWarning, "New Club Request represents 10 or more new clubs for your district");
=======
                       ClickById(PageObjects.linkTextAddClub,"Add Club");
                       VerifyMessage("Clubcount", ".//*[@id='lblDistrictNewClubRequestCountWarning']", "New Club Request represents 10 or more new clubs for your district");
                       
>>>>>>> 08179da9301276880e1ad79170424002a6c6ffb4
                   }
                   else
                   {
                       flag = true;

                       excelReporter.ReportStep("The Club Count is less than 10 hence No message is displayed", "Pass");
                   }
            }
            catch (NoSuchElementException e)
            {
                flag = false;
                excelReporter.ReportStep("Element with Fieldname could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                flag = false;
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot(this.screenShotFlag, flag);
            }
        }
    }
}
     

