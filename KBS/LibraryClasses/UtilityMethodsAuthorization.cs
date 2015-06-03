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
    public class UtilityMethodsAuthorization
    {
        IWebDriver browserDriver;
        int snapShotCount = 1;
        private String testCaseName;
        ExcelReporterAuthorization excelReporterAuth = new ExcelReporterAuthorization();
        ExcelReporter excelReporter = new ExcelReporter();
        
        public UtilityMethodsAuthorization(String testCaseName)
        {
            this.testCaseName = testCaseName;
            
        }

        ///<summary>
        ///This method is created to Login to MyLCI Application for Authorization
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
                    excelReporterAuth.ReportStep("Login is Successful for Userid: " + UserId, "Pass");
                    flag = true;
                }
                else
                {
                    excelReporter.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
                    excelReporterAuth.ReportStep("Login is not Successful for UserID: " + UserId, "Fail");
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

                if (browserDriver.FindElement(By.Id(LinkText)).Enabled)
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
      public IWebDriver InvokeApplication(String browser, String url)
      {
          excelReporter.CreateReportHeader();
          excelReporterAuth.CreateReportHeader();
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
            excelReporter.FlushWorkbook(testCaseName + "-Run");
            excelReporterAuth.FlushWorkbook(testCaseName + "-RunAuthorization");
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

        
        
           
        }

    }
     

