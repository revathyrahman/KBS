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
                EnterValueById(PageObjects.idLoginUserName, UserId);

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
             browserDriver.Manage().Window.Maximize();
             WebDriverWait wait = new WebDriverWait(browserDriver, TimeSpan.FromSeconds(10));
         }
          catch
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
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Id(id)).SendKeys(value);
                excelReporter.ReportStep("Element with id :" + id + "is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
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
        /// Takes two arguments name and value and  no return value
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

        /// <summary>
        /// This method is to select the value by index in the dropdown list
        /// </summary>
        /// <remarks>
        /// Takes two arguments id and index and no return value
        /// </remarks>
        
        public void SelectDropdownValueByIndex(String id, int index)
        {
            Boolean flag = true;
            try
            {
                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByIndex(index);
                flag = true;
                excelReporter.ReportStep("Element with id :" + id + " is found and index :" + index + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                flag = false;
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
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

        /// <summary>
        /// This method is to select the value by text in the dropdown list
        /// </summary>
        /// <remarks>
        /// Takes two arguments id and visibletext and  no return value
        /// </remarks>
        
        public void SelectDropdownValueByVisibleText(String id, String VisibleText)
        {
            Boolean flag = false;
            try
            {

                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByText(VisibleText);
                flag = true;
                excelReporter.ReportStep("Element with id :" + id + " is found and Visible Text :" + VisibleText + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
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
        /// Takes no arguments and  no return value
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

        /// <summary>
        /// This method is for closing the application
        /// </summary>
        /// <remarks>
        /// Takes one  argument testcase name and no return value
        /// </remarks>
        
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

          public void ClickByCSS(string css)
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.CssSelector(css)).Click();
                excelReporter.ReportStep("Element with css :" + css + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with css :" + css + "could not be found..", "FAILURE");
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

          /// <summary>
          /// This method is to verify  the element is present
          /// </summary>
          /// <remarks>
          /// Takes one argument Xpath and returns true on element present and false if it doesn't
          /// </remarks>
          
        public Boolean VerifyElementExists(string XPath)
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
                excelReporter.ReportStep("Element with XPath :" + XPath + "could not be found..", "FAILURE");
            }

            return flag;
        }

        /// <summary>
        /// This method is to click on the text
        /// </summary>
        /// <remarks>
        /// Takes one argument text and returns nothing
        /// </remarks>

        public void LinkClickByText(string text)
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

        /// <summary>
        /// This method is to click based on the Xpath
        /// </summary>
        /// <remarks>
        /// Takes one argument Xpath and  no return value
        /// </remarks>
        
        public void ClickByXPath(string XPath)
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.XPath(XPath)).Click();
                excelReporter.ReportStep("Element with XPath :" + XPath + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with XPath :" + XPath + "could not be found..", "FAILURE");
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
        /// Takes one argument text and  no return value
        /// </remarks>
        
        public void ClickById(string Id)
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Id(Id)).Click();
                excelReporter.ReportStep("Element with ID :" + Id + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with ID :" + Id + "could not be found..", "FAILURE");
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

        /// <summary>
        /// This method creates a new Club form and enters value to the fields and save
        /// </summary>
        ///<remarks>
        ///Takes no arguments returns the clubName Created
        /// </remarks>
        public string AddClubFormEntry()
        {
            //Create a random Number to append to ClubName for unique
            Random rdmNo = new Random();
            int randnum = rdmNo.Next(1000);
            
            string ClubName = "Club" + randnum;
            Boolean flag = true;
            try
            {
                
                //Enter ClubName - Unique
                EnterValueById(PageObjects.idClubName, ClubName);

                //Select Club Type
                SelectDropdownValueByVisibleText(PageObjects.ddlClubType, "Lions Club");

                //Enter Club City 
                EnterValueById(PageObjects.idClubCity, "Automation TestCity");


                //Select from Club Language
                SelectDropdownValueByVisibleText(PageObjects.ddlclubLang, "English");


                // Select from Club Language
                SelectDropdownValueByVisibleText(PageObjects.ddlclubLang, "English");
                VerifyLanguageListAddClub();

                //Click for a sponsoring club
                ClickById(PageObjects.btnSponsoringClub);
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath(PageObjects.xpathListSponsoringClubs);
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
                                                                       
                //Enter New Club President creation details
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

        /// <summary>
        /// This method is for logout of the application
        /// </summary>
        /// <remarks>
        /// Takes no argument and no return value
        /// </remarks>

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

        /// <summary>
        /// This method is to verify the task is available in the tasks list and clicks on the task 
        /// </summary>
        /// <remarks>
        /// Takes one argument and no return value
        /// </remarks>
        /// 
        public void VerifyMyTask(string taskName)
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

        /// <summary>
        /// This method is to discontinue the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns nothing
        /// </remarks>
        /// 
        public void DiscontinueClub(string ClubName)
        {
            try
            {
                ClickById(PageObjects.idDiscontinueClubchkbox);
                EnterValueById(PageObjects.idDiscontinueNote, "Test Comment");
                ClickById(PageObjects.idBtnSave);
                string ConfirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;

                if (ConfirmationMessage.Contains("Discontinued"))
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is Discontinued", "Pass");
                }
                else
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is not Discontinued", "Fail");
                }
                LinkClickByText(PageObjects.linkTextGotoApplication);
                Thread.Sleep(7000);
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
        
        public void ViewApplication(String clubname)
        {
            Boolean flag=true;
            try
            {
                IList<IWebElement> clubList = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));
                IList<IWebElement> viewAppList = browserDriver.FindElements(By.XPath(PageObjects.xpathViewAppList));
                int countClubList= clubList.Count();     

                int clubCount = 0;
                foreach (IWebElement clubNames in clubList)
                {

                    Thread.Sleep(1000);
                    string clubValue = clubNames.Text;

                    if (clubValue.Contains(clubname))
                    {
                        viewAppList[clubCount].Click();
                        excelReporter.ReportStep("View Application is clicked successfully", "SUCCESS");
                        break;
                    }
                    clubCount++;

                    if (clubCount == countClubList)
                    {
                        IWebElement pagination = browserDriver.FindElement(By.XPath(PageObjects.xpathClubListNextPage));
                        pagination.Click();
                        Thread.Sleep(5000);
                        clubList = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));

                    }
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
        /// This method is to remove from discontinuing the club and on success navigate to application
        /// </summary>
        /// Takes one argument ClubName returns nothing
        /// </remarks>
        
        public void ContinueClub(string ClubName)
        {
            Boolean flag = true;
            try
            {
                ClickById(PageObjects.idContinueClub);
                EnterValueById(PageObjects.idDiscontinueNote, "Test Comment");
                ClickById("btnSave");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;

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

        /// <summary>
        /// This method is to  verfiy the club is moved to next status on submit
        ///<remarks>
        ///Takes one argument CurrentStatus returns nothing
        /// </remarks>
        
        public void MoveClubtoNextStatus(String CurrentStatus)
        {
            Boolean flag = true;
            try
            {
                ClickById(PageObjects.idNewClubCriteria);
                ClickById(PageObjects.idBtnSubmitSelection);
                Thread.Sleep(5000);
                ClickById("btnSave");
                Thread.Sleep(5000);
                string confirmationMessage = browserDriver.FindElement(By.XPath(PageObjects.xpathClubConfirmationMsg)).Text;
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

        /// <summary>
        /// Thsi method to view the status of the clubs listed and click on the desired club
        /// </summary>
        /// <remarks>
        /// Takes one argument Filtername returns nothing
        /// </remarks>
        
        public void FindDesiredClub(String Filtername)
        {
            Boolean flag=true;
            try
            {

                ClickById(PageObjects.lblSearchOption);
                 IList<IWebElement> findClubList = browserDriver.FindElements(By.XPath(PageObjects.xpathFindClubQuickViewList));
                
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
        ///Takes two arguments Fielddesc and id and returns nothing
        /// </remarks>
        
        public void VerifyFieldEdit(string Fieldname, string id)
        {
            Boolean flag = false;
            try
            {
                if (browserDriver.FindElement(By.Id(id)).Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("The" + Fieldname + " is Editable", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("The" + Fieldname + " is  not Editable", "Fail");
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

        /// <summary>
        /// This method is to verify the dropdownlist is editable
        /// </summary>
        /// <remarks>
        /// Takes two arguments Fielddesc and id returns nothing
        /// </remarks>
        /// 
        public void VerifyDropdownEdit(string Fieldname, string id)
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
                        excelReporter.ReportStep("The" + Fieldname + "is Editable", "Pass");
                        break;
                     }
                    else
                    {
                        flag = false;
                        excelReporter.ReportStep("The" + Fieldname + "is  not Editable", "Fail");
                    }

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

        /// <summary>
        /// This method is to verify button is enabled
        /// </summary>
        /// <remarks>
        /// Takes two arguments Fileddesc and id returns nothing
        /// </remarks>
        
        public void VerifyButtonExists(string Fieldname, string id)
        {
            Boolean flag = false;
            try
            {
                IWebElement button = browserDriver.FindElement(By.Id(id));

                if (button.Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("The" + Fieldname + " is Enabled", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("The" + Fieldname + " is  not Enabled", "Fail");
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

        /// <summary>
        /// This method is to verify the fields are editable in New club officers section
        /// </summary>
        /// <remarks>
        /// Takes no arguments and returns nothing
        /// </remarks>
        
        public void NewClubOfficersChk()
        {
            Boolean flag = true;
            try
            {
                //President fieldlevel chk

                ClickByXPath(PageObjects.xpathClubPresidentPanel);
                VerifyFieldEdit("First Name", PageObjects.xpathClubSecretaryPanel);
                VerifyFieldEdit("Last Name", PageObjects.idPresidentFirstname);
                VerifyFieldEdit("Year Of Birth", PageObjects.idPresidentLastname);
                VerifyDropdownEdit("Gender", PageObjects.idPresidentGender);
                VerifyFieldEdit("Email Address", PageObjects.idPresidentEmailaddress);
                VerifyButtonExists("Clear",PageObjects.idPresidentClearbtn);

                //Secretary Fieldlevel chk

                ClickByXPath(PageObjects.xpathClubSecretaryPanel);
                VerifyFieldEdit("First Name", PageObjects.idSecretaryFirstname);
                VerifyFieldEdit("Last Name", PageObjects.idSecretaryLastname);
                VerifyFieldEdit("Year Of Birth", PageObjects.idSecretaryYOB);
                VerifyDropdownEdit("Gender", PageObjects.idSecretaryGender);
                VerifyFieldEdit("Email Address", PageObjects.idSsecretaryEmailaddress);
                VerifyButtonExists("Clear", PageObjects.idsecretaryclearbtn);
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
        /// Takes no arguments and returns nothing
        /// </remarks>
        
        public void ECMforLionsClub()
        {
            Boolean flag = true;
            try
            {
                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.idStudentMemberscount);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
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
        /// This method is to verify for the fields editable in Estimate Charter of Members section while selection of University/Campus club in the club type
        /// </summary>
        /// <remarks>
        /// Takes no arguments and returns nothing
        /// </remarks>
        
        public void ECMForUniversityClub()
        {
            Boolean flag = true;
            try
            {
                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Students Over 30 Years", PageObjects.idStudentsOver30Yrs);
                VerifyFieldEdit("Students 30 Years or younger", PageObjects.idStudentsUnder30yrs);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
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
        /// Takes no arguments and returns nothing
        /// </remarks>
        
        public void ECMForLeoLionsClub()
        {
            Boolean flag = true;
            try
            {

                VerifyFieldEdit("New Members", PageObjects.idNewMemberscount);
                VerifyFieldEdit("Transfer Members", PageObjects.idTransferMemberscount);
                VerifyFieldEdit("Student Members", PageObjects.idStudentMemberscount);
                VerifyFieldEdit("Young Adults", idYoungAdultscount);
                VerifyFieldEdit("Leo Lions", PageObjects.idLeoLionscount);
                
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
        /// This method is to verify of the checkbox is visible
        /// </summary>
        /// <remarks>
        /// Takes two arguments label and id returns nothing
        /// </remarks>
        
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
        ///<remarks>
        /// Takes two arguments Xpath and text returns nothing
        /// </remarks>
        
        public void VerifyTextDisplay(String Xpath, string text)
        {
            Boolean flag = true;
            try
            {
                IWebElement Element = browserDriver.FindElement(By.XPath(Xpath));
                string Label = Element.Text;

                if (Label.Contains(text))
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

        /// <summary>
        /// This method is to verify the club application is deleted is not
        /// </summary>
        /// <remarks>
        /// Takes one argument clubname checks for the clubname in the list on the clubname is not present returns nothing
        /// </remarks>
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

        /// <summary>
        /// This method is verify button is invisible
        /// </summary>
        ///<remarks>
        ///Takes four arguments Fieldname1,id1,Fieldname2,id2 returns nothing
        /// </remarks>
        
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

        /// <summary>
        /// This method is to verify the message is displayed
        /// </summary>
        ///<remarks>
        ///Takes three arguments Fielddesc,Xpath,Value returns nothing
        ///</remarks>
        
        public void VerifyMessage(String Fieldname,String Xpath,String Value)
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

        /// <summary>
        /// This method is to verify the Message is displayed for the club when the count  is more than 10 in the same district
        /// </summary>
        /// <remarks>
        /// Takes no arguments and returns nothing
        /// </remarks>
        
        public void VerifyMessageclubcount()
        {
           Boolean flag = false;

           try
               {

                   IList<IWebElement> clublist = browserDriver.FindElements(By.XPath(PageObjects.xpathClubList));

                   int Countclublist= clublist.Count();
                   if (Countclublist > 10)
                   {
                       flag = true;
                       VerifyMessage("Clubcount", PageObjects.xpathTenPlusClubWarning, "New Club Request represents 10 or more new clubs for your district");
                   }
                   else
                       flag = true;

                    excelReporter.ReportStep("The Club Count is less than 10 hence No message is displayed","Pass");
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
     

