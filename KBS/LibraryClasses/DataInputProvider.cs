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

namespace LCI.QualityTools.BrowserTests.MyLCI
{
    public class DataInputProvider
    {
        public List<List<String>> GetInputData(String DataSheetName)
        {

            List<List<String>> data = new List<List<String>>();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fileInputstream = new FileStream(theDirectory + "\\..\\..\\Data\\" + DataSheetName + ".xlsx", FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(fileInputstream);
            ISheet workSheet = new XSSFSheet();
            workSheet = workbook.GetSheet("Login");

            // Get the number of rows
            int rowCount = workSheet.LastRowNum;

            // Get the number of columns
            int columnCount = workSheet.GetRow(0).LastCellNum;

            // Loop through the rows
            for (int i = 1; i < rowCount + 1; i++)
            {
                try
                {
                    IRow row = workSheet.GetRow(i);
                    List<String> record = new List<String>();

                    for (int j = 0; j < columnCount; j++)
                    { // loop through the columns
                        try
                        {
                            record.Add(row.GetCell(j).StringCellValue); // add to the record
                        }
                        catch (Exception e)
                        {
                            e.StackTrace.ToString();
                        }
                     }
                    data.Add(record);
                  }
                catch (Exception e)
                {
                    e.StackTrace.ToString();                   
                }
              fileInputstream.Close();
           }
          return data;
       }
    }
}
