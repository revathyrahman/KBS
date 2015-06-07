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
    public class DataInputProvider
    {
        public List<List<String>> GetInputData(String DataSheetName)
        {

            List<List<String>> data = new List<List<String>>();
            string theDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fis = new FileStream(theDirectory + "\\..\\..\\Data\\" + DataSheetName + ".xlsx", FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(fis);
            ISheet sheet = new XSSFSheet();
            sheet = workbook.GetSheet("Login");

            // Get the number of rows
            int rowCount = sheet.LastRowNum;

            // Get the number of columns
            int columnCount = sheet.GetRow(0).LastCellNum;

            // Loop through the rows
            for (int i = 1; i < rowCount + 1; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
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
              fis.Close();
           }
          return data;
       }
    }
}
