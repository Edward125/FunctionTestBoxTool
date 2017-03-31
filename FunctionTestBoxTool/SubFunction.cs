using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edward;
using System.IO;
using System.Windows.Forms;

namespace FunctionTestBoxTool
{
    /// <summary>
    /// sub and function
    /// </summary>
    class SubFunction
    {



        #region SerialPort       

        ///<summary>
        /// get serial port name
        /// </summary>
        /// <param name="combox"></param>
        private void getSerialPort(ComboBox combox)
        {
            combox.Items.Clear();
            combox.Text = string.Empty;
            foreach (string sp in System.IO.Ports.SerialPort.GetPortNames())
            {
                combox.Items.Add(sp);
            }

            if (combox.Items.Count > 0)
            {
                if (string.IsNullOrEmpty(Param.portName))
                    combox.SelectedIndex = 0;
                else
                    combox.Text = Param.portName;
            }

        }




        #endregion

        #region INI File
        

        



        #endregion


        #region SaveLog

        /// <summary>
        /// save log by log type
        /// </summary>
        /// <param name="logtype">log type</param>
        /// <param name="logContent">log content</param>
        public static void SaveLog(Param.LogType logtype, string logContent)
        {

            //1,check folder exits
            //2,check file exits
            //3,write log content

            string logFile = string.Empty;
            string logContents = DateTime.Now.ToString("yyyyMMdHHmmss") + "->" + @logContent + "\r\n";

            switch (logtype)
            {
                case Param.LogType.SysLog:
                    logFile = Param.sysLogFolder + @"\SysLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                    //check folder
                    if (!Directory.Exists(@Param.sysLogFolder))
                    {
                        Directory.CreateDirectory(@Param.sysLogFolder);
                    }
                    //check file
                    if (!File.Exists(logFile))
                    {
                        FileStream fs = File.Create(logFile);
                        fs.Close();
                    }
                    //write log
                    try
                    {
                        File.AppendAllText(logFile, logContents);
                    }
                    catch (Exception)
                    {
                        
                       // throw;
                    }
                    break;
                case Param.LogType.TestLog:
                    logFile = Param.testLogFolder  + @"\TestLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                    //check folder
                    if (!Directory.Exists(@Param.testLogFolder)) 
                    {
                        Directory.CreateDirectory(@Param.testLogFolder);
                    }
                    //check file
                    if (!File.Exists(logFile))
                    {
                        FileStream fs = File.Create(logFile);
                        fs.Close();
                    }
                    //write log
                    try
                    {
                        File.AppendAllText(logFile, logContents);
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                    break;
                case Param.LogType.ScanLog:
                    logFile = Param.scanLogFolder   + @"\ScanLog_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                    //check folder
                    if (!Directory.Exists(@Param.scanLogFolder))
                    {
                        Directory.CreateDirectory(@Param.scanLogFolder);
                    }
                    //check file
                    if (!File.Exists(logFile))
                    {
                        FileStream fs = File.Create(logFile);
                        fs.Close();
                    }
                    //write log
                    try
                    {
                        File.AppendAllText(logFile, logContents);
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                    break;
                default:
                    break;
            }
        }




        /// <summary>
        /// save log by log type
        /// </summary>
        /// <param name="logtype">log type</param>
        /// <param name="logContent">log content</param>
        public static void SaveLog(Param.LogType logtype, string logContent,string sn)
        {

            //1,check folder exits
            //2,check file exits
            //3,write log content

            string logFile = string.Empty;
            string logContents = DateTime.Now.ToString("yyyyMMdHHmmss") + "->" + @logContent + "\r\n";

            switch (logtype)
            {
                case Param.LogType.BarLog :
                    logFile = Param.barLogFolder  +@"\"+ @sn + ".log";
                    //check folder
                    if (!Directory.Exists(@Param.barLogFolder ))
                    {
                        Directory.CreateDirectory(@Param.barLogFolder );
                    }
                    //check file
                    if (!File.Exists(logFile))
                    {
                        FileStream fs = File.Create(logFile);
                        fs.Close();
                    }
                    //write log
                    try
                    {
                        File.AppendAllText(logFile, logContents);
                    }
                    catch (Exception)
                    {

                        // throw;
                    }
                    break;  
                default:
                    break;
            }
        }


        #endregion

        #region updatemessage

        /// <summary>
        /// update message to list box
        /// </summary>
        /// <param name="listbox">list box name</param>
        /// <param name="message">message</param>
        public static void updateMessage(ListBox  listbox, string message)
        {
            if (listbox.Items.Count > 100)
                listbox.Items.Clear();
            string item = DateTime.Now.ToString("HH:mm:ss") + "->" + @message;
            listbox.Items.Add(item);
            if (listbox.Items.Count > 1)
            {
                listbox.TopIndex = listbox.Items.Count - 1;
                listbox.SetSelected(listbox.Items.Count - 1, true);
            }
        }
        #endregion


        /// <summary>
        /// get the directory path
        /// </summary>
        /// <param name="filefullpath">file full path</param>
        /// <returns></returns>
        public static string  getFolerPath(string filefullpath)
        {
            string path = string.Empty;
            path = filefullpath.Substring(0, filefullpath.LastIndexOf(@"\"));
            return path;
        }



        /// <summary>
        /// update FPY
        /// </summary>
        /// <param name="txttotal">display total</param>
        /// <param name="txtfail">display fail</param>
        /// <param name="txtfpy">display fpt</param>
        /// <param name="itotal"> total </param>
        /// <param name="ifail">fail</param>
        public static void updateFPY(TextBox  txttotal,TextBox txtfail,TextBox txtfpy,int itotal, int ifail)
        {
            int ipass = itotal - ifail;
            txttotal.Text = itotal.ToString();
            txtfail.Text = ifail.ToString();

            if (itotal == 0)
                txtfpy.Text = "0%";
            else
            {
                Int32 temp = Convert.ToInt32((((itotal-ifail ) / itotal) * 10000));
                int dfpy = temp / 100;
                txtfpy.Text = dfpy.ToString() + "%";
            }


        }

        /// <summary>
        /// load test item sequence from the ini file
        /// </summary>
        public static  void loadSequnce()
        {
            //Param.ItemSequence = new Dictionary<int, TestItem>();
            Param.ItemSequence = new Dictionary<int, string>();


            for (int i = 1; i < 8; i++)
            {
               object  st = IniFile.IniReadValue("Sequence", i.ToString());
               //(TestItem)st;
              // Param.ItemSequence.Add(i, (TestItem)st);
              Param.ItemSequence.Add(i,  IniFile.IniReadValue("Sequence", i.ToString()));
            }
        }

        /// <summary>
        /// 只能輸入數字和退格
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">KeyPressEventArgs</param>
        public static void onlyInputNumber(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                e.Handled = true;
        }

    }
}
