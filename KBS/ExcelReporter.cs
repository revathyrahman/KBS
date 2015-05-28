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

namespace KBS
{
   public class ExcelReporter
    {
       private static IWorkbook workbook ;
       private static IRow row;
       private static ISheet sheet;
        public void createReportHeader()
        {
            workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet("Report");
            row = sheet.CreateRow(0);    
            row.CreateCell(0).SetCellValue("Step No");
            row.CreateCell(1).SetCellValue("Test Description");
            row.CreateCell(2).SetCellValue("Test Status");
        }
        // Write to the Excel Report file
        public void FlushWorkbook(String TestCaseName)
        {
            try
            {
                FileStream sw = File.Create(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()) + "\\report\\" + TestCaseName + ".xlsx");
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
           row = sheet.CreateRow(sheet.LastRowNum+1);
           row.CreateCell(0).SetCellValue(sheet.LastRowNum);
           row.CreateCell(1).SetCellValue(Desc);
    	   row.CreateCell(2).SetCellValue(Status);
		}
    }
}
