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
   public class ExcelReporter
    {
       private static IWorkbook workbook ;
       private static IRow row;
       private static ISheet workSheet;
       private string testCaseName;
       public ExcelReporter(string testCaseName)
       {
           this.testCaseName = testCaseName;
       }
       public ExcelReporter()
       {
           
       }
        public void CreateReportHeader()
        {
            workbook = new XSSFWorkbook();
            workSheet = workbook.CreateSheet("Report");
            row = workSheet.CreateRow(0);    
            row.CreateCell(0).SetCellValue("Step No");
            row.CreateCell(1).SetCellValue("Test Description");
            row.CreateCell(2).SetCellValue("Test Status");
        }

        public void CreateReportHeader(string testCaseName)
        {
            workbook = new XSSFWorkbook();
            workSheet = workbook.CreateSheet("Report");
            row = workSheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Step No");
            row.CreateCell(1).SetCellValue("Test Description");
            row.CreateCell(2).SetCellValue("Test Status");
        }
        // Write to the Excel Report file
        public void FlushWorkbook(String testCaseName)
        {
            try
            {
                FileStream sw = File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\report\\" + testCaseName + ".xlsx");
                workbook.Write(sw);
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
           row = workSheet.CreateRow(workSheet.LastRowNum+1);
           row.CreateCell(0).SetCellValue(workSheet.LastRowNum);
           row.CreateCell(1).SetCellValue(Desc);
    	   row.CreateCell(2).SetCellValue(Status);
		}

        public void ReportStep(String testCaseName,String Desc, String Status)
        {
            row = workSheet.CreateRow(workSheet.LastRowNum + 1);
            row.CreateCell(0).SetCellValue(workSheet.LastRowNum);
            row.CreateCell(1).SetCellValue(Desc);
            row.CreateCell(2).SetCellValue(Status);
        }
    }
}
