using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NPOI.SS;
using NPOI.XSSF;
using NPOI.SS.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace MyLCIAutomation
{
   public class ExcelReporterAuthorization
    {
       private static IWorkbook workbookAuthorization;
       private static IRow rowAuth;
       private static ISheet sheetAuth;
        public void CreateReportHeader()
        {
            workbookAuthorization = new XSSFWorkbook();
            sheetAuth = workbookAuthorization.CreateSheet("Report");
            rowAuth = sheetAuth.CreateRow(0);    
            rowAuth.CreateCell(0).SetCellValue("Step No");
            rowAuth.CreateCell(1).SetCellValue("Test Description");
            rowAuth.CreateCell(2).SetCellValue("Test Status");
        }
        // Write to the Excel Report file
   
       public void FlushWorkbook(String TestCaseName)
        {
            try
            {
                FileStream sw = File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\report\\" + TestCaseName + ".xlsx");
                workbookAuthorization.Write(sw);
                sw.Close();
            }
            catch (IOException ioe)
            {
                ioe.StackTrace.ToString();
            }
        }
        // Write Stepwise Results
        public void ReportStep(String Desc, String Status)
        {   
           rowAuth = sheetAuth.CreateRow(sheetAuth.LastRowNum+1);
           rowAuth.CreateCell(0).SetCellValue(sheetAuth.LastRowNum);
           rowAuth.CreateCell(1).SetCellValue(Desc);
    	   rowAuth.CreateCell(2).SetCellValue(Status);
		}
    }
}
