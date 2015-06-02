using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private String testCaseName;
        ExcelReporter excelReporter = new ExcelReporter();
        ExcelReporter excelReporterforAuthorization = new ExcelReporter();
        private int dataSet
        {
            get;
            set;
        }
   
        public UtilityMethods(String testCaseName, int dataSet)
        {
            this.testCaseName = testCaseName;
            this.dataSet = dataSet;
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
                    flag = true;
                }
                else
                {
                    excelReporter.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
                    flag = false;
                   
                }
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
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
                TakeSnapshot(); 
            }
            return flag;
      }
       
      public IWebDriver InvokeApplication(String browser, String url)
      {
          excelReporter.CreateReportHeader();
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

        public void EnterValueById(String id, String value)
        {
            try
            {
                browserDriver.FindElement(By.Id(id)).SendKeys(value);
                excelReporter.ReportStep("Element with id :" + id + " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
        public void EnterValueByName(String name, String value)
        {
            try
            {
                browserDriver.FindElement(By.Name(name)).Clear();
                browserDriver.FindElement(By.Name(name)).SendKeys(value);
                excelReporter.ReportStep("Element with name :" + name + " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                excelReporter.ReportStep("Element with name :" + name + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exception)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void SelectDropdownValueByIndex(String id, int index)
        {
            try
            {
                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByIndex(index);
                excelReporter.ReportStep("Element with id :" + id + " is found and index :" + index + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void SelectDropdownValueByVisibleText(String id, String VisibleText)
        {
            try
            {
                IWebElement ele = browserDriver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByText(VisibleText);
                excelReporter.ReportStep("Element with id :" + id + " is found and Visible Text :" + VisibleText + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                excelReporter.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void TakeSnapshot()
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)browserDriver).GetScreenshot();
                ss.SaveAsFile(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\Screenshots\\" + "Snap-" + snapShotCount + ".png", System.Drawing.Imaging.ImageFormat.Png);
                snapShotCount++;
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

        public void CloseApplicationForAuthorizationUsers()
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

            excelReporterforAuthorization.FlushWorkbook(testCaseName + "-Run");
        }
        public void LinkClickByText(String text)
        {
            try
            {
                browserDriver.FindElement(By.LinkText(text)).Click();
                excelReporter.ReportStep("Element with text :" + text + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with text :" + text + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void ClickByCSS(String css)
        {
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
                TakeSnapshot();
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

        public void ClickByXPath(String XPath)
        {
            try
            {
                browserDriver.FindElement(By.XPath(XPath)).Click();
                excelReporter.ReportStep("Element with XPath :" + XPath + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with XPath :" + XPath + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
        public void ClickById(String Id)
        {
            try
            {
                browserDriver.FindElement(By.Id(Id)).Click();
                excelReporter.ReportStep("Element with ID :" + Id + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with ID :" + Id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                excelReporter.ReportStep("Driver could not be found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
      
        public string AddClubFormEntry()
        {
            //Create a random Number to append to ClubName for unique
            Random rdmNo = new Random();
            int randnum = rdmNo.Next(1000);
            
            string ClubName = "Club_" + randnum;
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
                TakeSnapshot();
            }
            return ClubName;
        }

        public void LogoutMyLCI()
        {
            try 
            {
                LinkClickByText("Logout");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with Link Logout could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        
        public void MyLCI_Logout()
        {
            try
            {

                // click logout button
                ClickById("hylLogout");

                excelReporter.ReportStep("Verify Logout is successfull", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
        
        public void VerifyMyTask(String taskname)
        {
            try
            {

                // List of Tasks
                
                IList<IWebElement> tasks = browserDriver.FindElements(By.XPath("//div[@id='Tab265']/div/ul/li/div/div/a"));
                
                //string[] listvalue=new string[tasks.Count];
                int val=0;
                foreach (IWebElement tlist in tasks)
                {

                    //listvalue[i] = tlist.Text;
                    //if(listvalue[i].Contains(taskname))

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
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
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
            try
            {

                //IList<IWebElement> clublist = driver.FindElements(By.XPath("//div[@class='gridData']/div/div/div/div"));
                IList<IWebElement> clublist = browserDriver.FindElements(By.XPath("//div[@class='gridHeader']/div/div/div[1]"));
                IList<IWebElement> viewApplist = browserDriver.FindElements(By.XPath("//a[.='View Application']"));
                       
                int val = 0;
                foreach (IWebElement clnames in clublist)
                {
                    string clvalue = clnames.Text;
                   
                    if (clvalue.Contains(clubname))
                    {
                        viewApplist[val].Click();
                        excelReporter.ReportStep("View Application is clicked successfully", "SUCCESS");
                        break;
                    }
                    val++;
                }

            }
            catch (NoSuchElementException e)
            {
                excelReporter.ReportStep("Element with id could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void ContinueClub(string ClubName)
        {
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
                e.StackTrace.ToString();
            }
            finally
            {
                TakeSnapshot();
            }
        }
        public void MoveClubtoNextStatus(String CurrentStatus)
        {
            try
            {
                ClickById("cbReadNewClubCriteria");
                ClickById("cbStatusAction_Submit");
                ClickById("btnSave");
                string confirmationMessage = browserDriver.FindElement(By.XPath("//div[@class='confirmationMessages']/table/tbody/tr/td")).Text;
                if (confirmationMessage.Contains("District Governor authorization"))

                    excelReporter.ReportStep("Club application moved to DG Authorization status", "Pass");

                else
                    excelReporter.ReportStep("Club application not moved to DG Authorization status", "Pass");

                LinkClickByText("Go to Application");
            }

            catch (NoSuchElementException e)
            {
                e.StackTrace.ToString();
            }
            finally
            {
                TakeSnapshot();
            }
        }


        public void FindDesiredClub(String Filtername)
        {
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
            }
            catch (WebDriverException ex)
            {
                excelReporter.ReportStep("Driver could not found !!!", "FAILURE");

            }
            finally
            {
                TakeSnapshot();
            }

                
            }
           
        }

    }
     

