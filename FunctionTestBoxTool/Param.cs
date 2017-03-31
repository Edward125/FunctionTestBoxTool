using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FunctionTestBoxTool
{
    /// <summary>
    /// variables & parameter
    /// </summary>
    class Param
    {

        #region enum

        /// <summary>
        /// test item result:PASS or FAIL,or WAIT
        /// </summary>
        public enum TestResult
        {
            PASS,
            FAIL,
            WAIT
        }

        /// <summary>
        /// test item status,YES:Will test,NO:Won't test
        /// </summary>
        public enum TestItemStatus
        {
            YES,
            NO
        }

        /// <summary>
        /// log type
        /// </summary>

        public enum LogType
        {
            SysLog,    // all  system 操作 log
            TestLog,   //barcode & test item & test result
            ScanLog,   // scan barcode list
            BarLog     // each barcode create a file,contain the result
        }

        #endregion

        #region Variables

        public static string iniFilePath = Application.StartupPath + @"\FunctionBox.ini";   //ini file path;
        public static string USB30ToolPath = Application.StartupPath + @"\USB3014Tester\USB3014Tester.exe"; //Test USB3.0 tool path
        public static string HDMIVGAToolPath = Application.StartupPath +  @"\HVTTester\HVTTester.exe"; //test HDMI or VGA tool path     
        public static string USB30ToolTestResultFile =SubFunction.getFolerPath (Param.USB30ToolPath )+ @"\USB3014Tester_Result.txt";
        public static string HDMIVGAToolTestResultFile = SubFunction.getFolerPath(Param.HDMIVGAToolPath) + @"\HVTResult.txt";

        //log
        public static string logFolder = Application.StartupPath + @"\Log";
        public static string sysLogFolder = logFolder + @"\SysLog";
        public static string testLogFolder = logFolder + @"\TestLog";
        public static string scanLogFolder = logFolder + @"\ScanLog";
        public static string barLogFolder = logFolder +@"\"+ DateTime.Now.ToString("yyyyMMdd");
        //
        public static string portName = string.Empty; // comport name
        //
       public static Dictionary<int, string> ItemSequence = new Dictionary<int, string>();
       // public static Dictionary<int, TestItem> ItemSequence = new Dictionary<int, TestItem>();
       //
       public static string barcodeLength = "23";
       public static string barcodeStart = "SC50F54313";
        #endregion




    }
}
