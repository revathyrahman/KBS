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


namespace KBS
{
    public class UtilityMethods
    {
        IWebDriver driver;
        int i = 1;
        private String testCaseName;
        private int dataSet;
        ExcelReporter er = new ExcelReporter();
   
        public UtilityMethods(String testCaseName, int dataSet)
        {
            this.testCaseName = testCaseName;
            this.dataSet = dataSet;
        }
        public Boolean LoginMyLCI(String Role, String UserId, String Password)
        {
            Boolean flag = false;

            //Invoke a new browser and load the MyLCI application URL
            InvokeApplication("Firefox", "https://mylcibeta.lionsclubs.org/");
            try
            {
                // Enter user name
                EnterValueById("PageContent_Login1_txtUsername", UserId);

                // Enter password
                EnterValueById("PageContent_Login1_txtPassword", Password);

                // Click login button
                ClickById("PageContent_Login1_btnSubmit");

                if (driver.FindElement(By.LinkText("Home")).Displayed)
                {
                    er.ReportStep("Login is Successful for Userid: " + UserId, "Pass");
                    flag = true;
                }
                else
                {
                    er.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
                    flag = false;
                   
                }
            }
            catch (WebDriverException e)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
             return flag;
        }
        public Boolean VerifyAddClubLinkExists(String LinkText)
        {
            Boolean flag = false;
            try
            {
                driver.FindElement(By.Id("a_3_1_28")).Click();
                driver.FindElement(By.Id("a_3_2_40")).Click();

                if (driver.FindElement(By.LinkText("Add Club")).Enabled)
                {
                    flag = true;
                    er.ReportStep("Add Club Link exists", "Pass");
                }
                else
                {
                    flag = false;
                    er.ReportStep("Add Club Link exists", "Pass");
                }
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element AddClub does not Exist", "FAILURE");
            }
            finally
            {
                TakeSnapshot(); 
            }
            return flag;
      }
       
        public IWebDriver InvokeApplication(String browser, String url)
        {
            er.createReportHeader();
            try
            {
                if (browser.ToLower().Equals("ie"))
                {
                    //driver = new InternetExplorerDriver(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "\\drivers\\" + "IEDriverServer");
                    driver = new InternetExplorerDriver(@"D:\\drivers\\IEDriverServer.exe");
                    // System.Environment.SetEnvironmentVariable("webdriver.IE.driver", @"D:\\drivers\\IEDriverServer.exe");
                    // driver = new InternetExplorerDriver();
                }
                else if (browser.ToLower().Equals("chrome"))
                {
                    System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"D:\\chromedriver.exe");
                    driver = new ChromeDriver();
                }
                else
                {
                    driver = new FirefoxDriver();
                }
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                er.ReportStep("Application invoked successfully", "SUCCESS");

            }
            catch (WebDriverException exception)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
            }

            return driver;
        }

        public void EnterValueById(String id, String value)
        {
            try
            {
                driver.FindElement(By.Id(id)).SendKeys(value);
                er.ReportStep("Element with id :" + id + " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                er.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                driver.FindElement(By.Name(name)).Clear();
                driver.FindElement(By.Name(name)).SendKeys(value);
                er.ReportStep("Element with name :" + name + " is found and value :" + value + " entered successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exc)
            {
                er.ReportStep("Element with name :" + name + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exception)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                IWebElement ele = driver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByIndex(index);
                er.ReportStep("Element with id :" + id + " is found and index :" + index + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                er.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                IWebElement ele = driver.FindElement(By.Id(id));
                SelectElement dropDownElement = new SelectElement(ele);
                dropDownElement.SelectByText(VisibleText);
                er.ReportStep("Element with id :" + id + " is found and Visible Text :" + VisibleText + " selected successfully..", "SUCCESS");
            }
            catch (NoSuchElementException exception)
            {
                er.ReportStep("Element with id :" + id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException e)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "\\Screenshots\\" + "Snap-" + i + ".png", System.Drawing.Imaging.ImageFormat.Png);
                i++;
            }
            catch (IOException ioe)
            {
                // TODO Auto-generated catch block
                er.ReportStep("Unable to copy the file", "FAILURE");
            }
        }

        public void CloseApplication()
        {
            try
            {
                driver.Quit();
                er.ReportStep("The application is closed successfully..", "SUCCESS");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be closed for unknown reason !!!", "FAILURE");
            }

            er.FlushWorkbook(testCaseName + "-Run" + dataSet);
        }
        public void LinkClickByText(String text)
        {
            try
            {
                driver.FindElement(By.LinkText(text)).Click();
                er.ReportStep("Element with text :" + text + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with text :" + text + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                driver.FindElement(By.CssSelector(css)).Click();
                er.ReportStep("Element with css :" + css + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with css :" + css + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                if (driver.FindElement(By.XPath(XPath)).Displayed)
                    flag = true;
                else
                    flag = false;

            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with XPath :" + XPath + "could not be found..", "FAILURE");
            }

            return flag;
        }

        public void ClickByXPath(String XPath)
        {
            try
            {
                driver.FindElement(By.XPath(XPath)).Click();
                er.ReportStep("Element with XPath :" + XPath + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with XPath :" + XPath + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
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
                driver.FindElement(By.Id(Id)).Click();
                er.ReportStep("Element with ID :" + Id + " is found and clicked successfully..", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with ID :" + Id + "could not be found..", "FAILURE");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
      
        public void AddClubFormEntry()
        {
            try
            {
                //Create a random Number to append to ClubName for unique
           
                Random rdmNo = new Random();
                int randnum=rdmNo.Next(1000);

                //Enter ClubName - Unique
                EnterValueById("txtClubName", "Club_" + randnum);

                //Select Club Type
                SelectDropdownValueByVisibleText("ddlClubType", "Lions Club");

                //Enter Club City 
                EnterValueById("txtCity", "Automation TestCity");

                //Select from Club Language
                SelectDropdownValueByVisibleText("ddlClubLanguage", "English");

                //Click for a sponsoring club
                ClickById("btnSelectSponsoringClub");
                ClickByCSS("div.span2.availableDistrictClub");

                //Enter New Club President creation details
                ClickByXPath("//*[@id='pnlNewClubPresidentHeader']/div/b");
                EnterValueById("txtPresidentFirstName", "PresidentFirstname");
                EnterValueById("txtPresidentLastName", "PresidentLastname");
                EnterValueById("txtPresidentYearOfBirth", "1980");
                SelectDropdownValueByVisibleText("ddlPresidentGender", "Male");
                EnterValueById("txtPresidentEmailAddress", "president@test.com");

                //Enter New Club Secretary Creation details
                ClickByCSS("p.controls > b.caret");
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

                //Enter Comment
                EnterValueById("cbReadNewClubCriteria", "txtNewClubAppComment");
                EnterValueById("txtNewClubAppComment", "test comment");

                //Click on Save
                ClickById("btnSave");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void LogoutMyLCI()
        {
            try
            {
                LinkClickByText("Logout");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with Link Logout could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public Boolean Verify_AddClub(String addclub)
        {
            Boolean blnFlag = false;
            try
            {

                if (driver.FindElement(By.Id(addclub)).Displayed)
                {
                    blnFlag = true;
                    er.ReportStep("Add club button is present", "SUCCESS");
                }
                else
                {
                    blnFlag = false;
                    er.ReportStep("Add club button is not present", "FAILURE");
                }
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with Link Logout could not be found", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
            return blnFlag;
        }
        public void MyLCI_Logout()
        {
            try
            {

                // click logout button
                ClickById("hylLogout");

                er.ReportStep("Verify Logout is successfull", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with id could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                er.ReportStep("Driver could not found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
        public void MyLCI_Login(String usename, String pwd)
        {
            try
            {
                // enter user name

                EnterValueById("PageContent_Login1_txtUsername", usename);

                // enter password

                EnterValueById("PageContent_Login1_txtPassword", pwd);

                // click submit button
                ClickById("PageContent_Login1_btnSubmit");

                er.ReportStep("Verify Login is successfull", "SUCCESS");
            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with id :" + usename + "&" + pwd + "could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                er.ReportStep("Driver could not found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }

        public void CloseAllapp()
        {
            try
            {
                driver.Quit();
                er.ReportStep("The application is closed successfully..", "SUCCESS");
            }
            catch (WebDriverException exe)
            {
                er.ReportStep("Driver could not be closed for unknown reason !!!", "FAILURE");
            }

            er.FlushWorkbook(testCaseName + "-Run");
        }
        public void Verify_MyTask(String taskname)
        {
            try
            {

                // List of Tasks
                
                IList<IWebElement> tasks = driver.FindElements(By.XPath(PageObject.home_viewtask));
                
                String[] listvalue=new String[tasks.Count];
                int val=0;
                foreach (IWebElement tlist in tasks)
                {
                    listvalue[i] = tlist.Text;
                    if(listvalue[i].Contains(taskname)
                    {
                        tlist.Click(); 
                        er.ReportStep("Pending Authorization task is clicked successfully", "SUCCESS");
                        break;
                    }
                    val++;
                }

            }
            catch (NoSuchElementException e)
            {
                er.ReportStep("Element with id could not found..", "FAILURE");
            }
            catch (WebDriverException ex)
            {
                er.ReportStep("Driver could not found !!!", "FAILURE");
            }
            finally
            {
                TakeSnapshot();
            }
        }
    }
}