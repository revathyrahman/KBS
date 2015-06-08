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
        ExcelReporter excelReporterAuth = new ExcelReporter("Authorization");
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

        public UtilityMethods(string testcasename, string screenshotflag)
        {
            this.testCaseName = testcasename;
            this.screenShotFlag = screenshotflag;
        }

         public UtilityMethods(string testCaseName, int dataSet,string screenshotflag)
        {
            this.testCaseName = testCaseName;
            this.dataSet = dataSet;
            this.screenShotFlag = screenshotflag;
        }


        ///<summary>
        ///This method is created to Login to MyLCI Application
        ///</summary>
        ///<remarks>
        ///Takes three arguments and returns true if the login is successful and false if its is not
        ///</remarks>

        public Boolean LoginMyLCI(String Role, String UserId, String Password)
        {
            Boolean flag = false;

            try
            {
                // Enter user name
                EnterValueById("PageContent_Login1_txtUsername", UserId);

                // Enter password
                EnterValueById("PageContent_Login1_txtPassword", Password);

                // Click login button
                ClickById("PageContent_Login1_btnSubmit");

                if (browserDriver.FindElement(By.LinkText("Home")).Displayed)
                {
                    excelReporter.ReportStep("Login is Successful for Userid: " + UserId, "Pass");
                    excelReporterAuth.ReportStep("Authorization", "User id: " + UserId + " is Successful", "SUCCESS");
                    flag = true;
                }
                else
                {
                    excelReporter.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
                    excelReporterAuth.ReportStep("Authorization", "User id: " + UserId + " is UnSuccessful", "FAILURE");
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
            Boolean flag = false;
            try
            {
                browserDriver.FindElement(By.Id("a_3_1_28")).Click();
                browserDriver.FindElement(By.Id("a_3_2_40")).Click();

                if (browserDriver.FindElement(By.LinkText("Add Club")).Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("Add Club Link exists", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("Add Club Link does not exist", "Pass");
                }
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element AddClub does not Exist", "FAILURE");
            }
            finally
            {
                TakeSnapshot(this.screenShotFlag,flag); 
            }
            return flag;
      }
       
      public IWebDriver InvokeApplication(String browser, String url)
      {
          excelReporter.CreateReportHeader("Authorization");
          
          try
          {
             if (browser.ToLower().Equals("ie"))
             {
                //Code to invoke IE Browser
                 browserDriver = new InternetExplorerDriver();
             }
             else if (browser.ToLower().Equals("chrome"))
             {
                //Code to invoke Chrome browser 
                 browserDriver = new ChromeDriver();
             }
             else
             {
                 browserDriver = new FirefoxDriver();
             }
             browserDriver.Navigate().GoToUrl(url);
             browserDriver.Manage().Window.Maximize();
             WebDriverWait wait = new WebDriverWait(browserDriver, TimeSpan.FromSeconds(10));
             excelReporter.ReportStep("Application invoked successfully", "SUCCESS");
             }
             catch (WebDriverException exception)
             {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
             }
             return browserDriver;
        }
      public void VerifyLanguageListAddClub()
      {
          IWebElement ele = browserDriver.FindElement(By.Id("ddlClubLanguage"));
          IList<IWebElement> options= ele.FindElements(By.TagName("option"));
          if (options.Count() == 12)
          {
              excelReporter.ReportStep("12 languages are listed", "PASS");
              int languageCount = 1;
              foreach (IWebElement option in options)
              {
                   
                  if(languageCount < 12)
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
        public void EnterValueById(String id, String value)
        {
            Boolean flag = true;
            try
            {
                browserDriver.FindElement(By.Id(id)).SendKeys(value);
                excelReporter.ReportStep("Element with id :" + id + " is found and value :" + value + " entered successfully..", "SUCCESS");
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



   

        public void ClickByCSS(String css)
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
        public Boolean VerifyElementExists(String XPath)
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

       
        public void LinkClickByText(String text)
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

       
        
        public void ClickByXPath(String XPath)
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


        public void ClickById(String Id)
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
                EnterValueById("txtClubName", ClubName);

                //Select Club Type
                SelectDropdownValueByVisibleText("ddlClubType", "Lions Club");

                //Enter Club City 
                EnterValueById("txtCity", "Automation TestCity");


                //Select from Club Language
                SelectDropdownValueByVisibleText("ddlClubLanguage", "English");

                //Click for a sponsoring club
                ClickById("btnSelectSponsoringClub");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath("//div[@class='DistrictClubResults']/div/div/div[1]");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

               // Select from Club Language
                SelectDropdownValueByVisibleText("ddlClubLanguage", "English");
                VerifyLanguageListAddClub();
                
                //Click for a sponsoring club
                ClickById("btnSelectSponsoringClub");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                ClickByXPath("//div[@class='DistrictClubResults']/div/div/div[1]");
                browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

                               
                //Enter New Club President creation details
                ClickByXPath("//*[@id='pnlNewClubPresidentHeader']/div/b");
                EnterValueById("txtPresidentFirstName", "PresidentFirstname");
                EnterValueById("txtPresidentLastName", "PresidentLastname");
                EnterValueById("txtPresidentYearOfBirth", "1980");
                SelectDropdownValueByVisibleText("ddlPresidentGender", "Male");
                EnterValueById("txtPresidentEmailAddress", "president@test.com");

                //Enter New Club Secretary Creation details
                ClickByXPath("//*[@id='pnlNewClubSecretaryHeader']/p/b");
                EnterValueById("txtSecretaryFirstName", "SecretaryFirstName");
                EnterValueById("txtSecretaryLastName", "SecretaryLastName");
                EnterValueById("txtSecretaryYearOfBirth", "1980");
                SelectDropdownValueByVisibleText("ddlSecretaryGender", "Female");
                EnterValueById("txtSecretaryEmailAddress", "testsecretary@test.com");

                //Enter Charter Member details
                EnterValueById("txtNewMemberCount", "2");
                EnterValueById("txtTransferMemberCount", "0");
                EnterValueById("txtStudentCount", "0");
                EnterValueById("txtLeoLionCount", "0");

                //Check New Club Criteria checkbox
                ClickById("cbReadNewClubCriteria");
                //Enter Comment
                EnterValueById("txtNewClubAppComment", "test comment");

                //Click on Save
                ClickById("btnSave");
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

        public void LogoutMyLCI()
        {
            Boolean flag=true;
            try
            {
                LinkClickByText("Logout");

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

      
        public void VerifyMyTask(String taskname)
        {
            Boolean flag=true;
            try
            {
               IList<IWebElement> tasks = browserDriver.FindElements(By.XPath("//div[@id='Tab265']/div/ul/li/div/div/a"));
                
                int val=0;
                foreach (IWebElement tlist in tasks)
                {
                    string listvalue = tlist.Text;
                    if(listvalue.Contains(taskname))

                    {
                        tlist.Click(); 
                        excelReporter.ReportStep("Pending Authorization task is clicked successfully", "SUCCESS");
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

        public void DiscontinueClub(string ClubName)
        {
            try
            {
                ClickById("cbStatusAction_Discontinue");
                EnterValueById("txtDiscontinueNote", "Test Comment");
                ClickById("btnSave");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;

                if (ConfirmationMessage.Contains("Discontinued"))
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is Discontinued", "Pass");
                }
                else
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is not Discontinued", "Fail");
                }
                LinkClickByText("Go to Application");
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }
        }

        public void ViewApplication(String clubname)
        {
            Boolean flag=true;
            try
            {

                //IList<IWebElement> clublist = driver.FindElements(By.XPath("//div[@class='gridData']/div/div/div/div"));
                IList<IWebElement> clublist = browserDriver.FindElements(By.XPath("//div[@class='gridHeader']/div/div/div[1]"));
                IList<IWebElement> viewApplist = browserDriver.FindElements(By.XPath("//a[.='View Application']"));
                // int Countclublist= clublist.Count();     

                int val = 0;
                foreach (IWebElement clnames in clublist)
                {
                    Thread.Sleep(1000);
                    string clvalue = clnames.Text;

                    if (clvalue.Contains(clubname))
                    {
                        viewApplist[val].Click();
                        excelReporter.ReportStep("View Application is clicked successfully", "SUCCESS");
                        break;
                    }
                    val++;
                    //if (val == Countclublist)
                    //{
                      //  IWebElement pagnation = browserDriver.FindElement(By.XPath(".//*[@id='myGrid']/div[2]/div[3]/a[2]/img"));
                      //  pagnation.Click();
                    //}
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

        public void ContinueClub(string ClubName)
        {
            Boolean flag = true;
            try
            {
                ClickById("cbStatusAction_Continue");
                EnterValueById("txtDiscontinueNote", "Test Comment");
                ClickById("btnSave");
                string ConfirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;

                if (ConfirmationMessage.Contains("removed from Discontinued"))
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is removed from Discontinued", "Pass");
                }
                else
                {
                    excelReporter.ReportStep("The club Name" + ClubName + "is not removed from Discontinued", "Fail");
                }
                LinkClickByText("Go to Application");
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
        public void MoveClubtoNextStatus(String CurrentStatus)
        {
            Boolean flag = true;
            try
            {
                ClickById("cbReadNewClubCriteria");
                ClickById("cbStatusAction_Submit");
                ClickById("btnSave");
                string confirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;
                if (confirmationMessage.Contains("District Governor authorization"))

                    excelReporter.ReportStep("Club application moved to DG Authorization status", "Pass");

                else
                    flag = false;
                    excelReporter.ReportStep("Club application not moved to DG Authorization status", "Pass");

                LinkClickByText("Go to Application");
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
                

        public void FindDesiredClub(String Filtername)
        {
            Boolean flag=true;
            try
            {
                
                ClickById("lblSearchOptionTitle");
                 IList<IWebElement> findlist = browserDriver.FindElements(By.XPath("//*[@id='pnlQuickViews']/li/a"));
                
                int val = 0;
                foreach (IWebElement findnames in findlist)
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


        public void VerifyFieldEdit(string Fieldname, string Xpath)
        {
            Boolean flag = false;
            try
            {
                if (browserDriver.FindElement(By.XPath(Xpath)).Enabled)
                {
                    flag = true;
                    excelReporter.ReportStep("The" + Fieldname + "is Editable", "Pass");
                }
                else
                {
                    flag = false;
                    excelReporter.ReportStep("The" + Fieldname + "is  not Editable", "Fail");
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

        public void VerifyDropdownEdit(string Fieldname, string id)
        {
            Boolean flag = false;
            try
            {
                IWebElement element = browserDriver.FindElement(By.Id(id));
                IList<IWebElement> opts = element.FindElements(By.XPath(".//option"));
                foreach (IWebElement opt in opts)
                {
                    if (opt.Enabled)
                    {
                        flag = true;
                        excelReporter.ReportStep("The" + Fieldname + "is Editable", "Pass");
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

        public void NewClubOfficersChk()
        {
            Boolean flag = true;
            try
            {
                //President fieldlevel chk

                ClickByXPath(".//*[@id='pnlNewClubPresidentHeader']/div/b");
                VerifyFieldEdit("First Name", ".//*[@id='txtPresidentFirstName']");
                VerifyFieldEdit("Last Name", ".//*[@id='txtPresidentLastName']");
                VerifyFieldEdit("Year Of Birth", ".//*[@id='txtPresidentYearOfBirth']");
                VerifyDropdownEdit("Gender", "ddlPresidentGender");
                VerifyFieldEdit("Email Address", ".//*[@id='txtPresidentEmailAddress']");
                VerifyButtonExists("Clear", "btnClearPresident");

                //Secretary Fieldlevel chk

                ClickByXPath(".//*[@id='pnlNewClubSecretaryHeader']/p/b");
                VerifyFieldEdit("First Name", ".//*[@id='txtSecretaryFirstName']");
                VerifyFieldEdit("Last Name", ".//*[@id='txtSecretaryLastName']");
                VerifyFieldEdit("Year Of Birth", ".//*[@id='txtSecretaryYearOfBirth']");
                VerifyDropdownEdit("Gender", "ddlSecretaryGender");
                VerifyFieldEdit("Email Address", ".//*[@id='txtSecretaryEmailAddress']");
                VerifyButtonExists("Clear", "btnClearSecretary");
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

        public void ECMforLionsClub()
        {
            Boolean flag = true;
            try
            {
                VerifyFieldEdit("New Members", ".//*[@id='txtNewMemberCount']");
                VerifyFieldEdit("Transfer Members", ".//*[@id='txtTransferMemberCount']");
                VerifyFieldEdit("Student Members", ".//*[@id='txtStudentCount']");
                VerifyFieldEdit("Leo Lions", ".//*[@id='txtLeoLionCount']");
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

        public void ECMForUniversityClub()
        {
            Boolean flag = true;
            try
            {
                VerifyFieldEdit("New Members", ".//*[@id='txtNewMemberCount']");
                VerifyFieldEdit("Transfer Members", ".//*[@id='txtTransferMemberCount']");
                VerifyFieldEdit("Students Over 30 Years", ".//*[@id='txtStudentOver30YrsCount']");
                VerifyFieldEdit("Students 30 Years or younger", " .//*[@id='txtStudentUnder30YrsCount']");
                VerifyFieldEdit("Leo Lions", ".//*[@id='txtLeoLionCount']");
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
        public void ECMForLeoLionsClub()
        {
            Boolean flag = true;
            try
            {
                VerifyFieldEdit("New Members", ".//*[@id='txtNewMemberCount']");
                VerifyFieldEdit("Transfer Members", ".//*[@id='txtTransferMemberCount']");
                VerifyFieldEdit("Student Members", ".//*[@id='txtStudentCount']");
                VerifyFieldEdit("Young Adults", " .//*[@id='txtYoungAdultMemberCount']");
                VerifyFieldEdit("Leo Lions", ".//*[@id='txtLeoLionCount']");
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

        public void VerifyCheckboxExists(String label, string Xpath)
        {
            Boolean flag = true;
            try
            {
                IWebElement Chkbox = browserDriver.FindElement(By.XPath(Xpath));
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

        public void VerifyDeleteApplication(String clubname)
        {
            Boolean flag = true;
            try
            {

                //IList<IWebElement> clublist = driver.FindElements(By.XPath("//div[@class='gridData']/div/div/div/div"));
                IList<IWebElement> clublist = browserDriver.FindElements(By.XPath("//div[@class='gridHeader']/div/div/div[1]"));
                IList<IWebElement> viewApplist = browserDriver.FindElements(By.XPath("//a[.='View Application']"));

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
    }
}
     

