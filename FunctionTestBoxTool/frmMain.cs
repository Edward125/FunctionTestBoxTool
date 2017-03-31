using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using Edward;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Media;




//=====================================================
//Copyright (C) 2015 Edward Song
//create:2015/09
//version:1.0.1.0
//Mail:Edward_Song@yeah.net
//QQ:153437627
//======================================================
//                   _ooOoo_ 
//                  o8888888o 
//                  88" . "88 
//                  (| -_- |)
//                  O\  =  /O
//               ____/`---'\____
//             .'  \\|     |//  `.
//            /  \\|||  :  |||//  \
//           /  _||||| -:- |||||-  \
//           |   | \\\  -  /// |   |
//           | \_|  ''\---/''  |   |
//           \  .-\__  `-`  ___/-. /
//         ___`. .'  /--.--\  `. . __
//      ."" '<  `.___\_<|>_/___.'  >'"".
//     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//     \  \ `-.   \_ __\ /__ _/   .-` /  /
//======`-.____`-.___\_____/___.-`____.-'======
//                   `=---='
//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//         佛祖保佑       永无BUG
//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//======================================================

namespace FunctionTestBoxTool
{
    public partial class frmMain : Form
    {

        #region variables

        public int intTestSPK;   //test speaker number
        public int intTestAudio; 
        TestItem Vibrator = new TestItem();
        TestItem Speaker = new TestItem();
        TestItem Keyboard = new TestItem();
        TestItem Audio = new TestItem();
        TestItem USB30 = new TestItem();
        TestItem HDMI = new TestItem();
        TestItem DCIN = new TestItem();
        TestItem[] AllItems;
        //TestItem 
        //
        public int iTotal = 0;
        public int iFail = 0;
        public string nowTesting = string.Empty;
        //
        Process p = new Process();
        //
        SoundPlayer simpleSound; //= new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_2"));
        //
        bool insertDock = false;//
        bool bReady = false;//

        //
        string portString = string.Empty; // comport receive data
        //-------------remark----------------
        //PC->PLC:A   start test vibrator
        //PLC->PC:B   vibrator test ok
        //PLC->PC:C   vibrator test ng
        //PC->PLC:D   DCIN,charge,connect 19V
        //PLC->PC:E   charge test ok
        //PLC->PC:F   charge test ng
        //PC->PLC:G   DCIN,discharge,disconnect 19V
        //PLC->PC:H   discharge test ok
        //PLC->PC:I   discharge test ng
        #endregion

        #region form 

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            AllItems = new TestItem[7] { Vibrator, Speaker, Keyboard, Audio, USB30, HDMI, DCIN };


            //this.Text = "Function Test Box Tool,Ver.:" + Application.ProductVersion + ",Kunshan KaiYao,Author:Edward Song";
            this.Text = "Function Test Box Tool,Ver.:" + Application.ProductVersion + ",Author:Edward Song";
            //
            //

            SubFunction.updateMessage(lstStatus ,"Start the tool.");
            SubFunction.SaveLog(Param.LogType.SysLog, "Start the tool.");
            IniFile.IniFilePath = Param.iniFilePath;
            IniFile.CreateIniFile();
            SubFunction.updateMessage(lstStatus, "Create ini file.");
            SubFunction.SaveLog(Param.LogType.SysLog, "Create ini file.");
            init();
            //


        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Form f = new frmSetting();
            f.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(GenerateCheckCode());
            //MessageBox.Show(CreateRandNum(0, 4).ToString());
            //int i = CreateRandNum(1, 4);
            //intTestSPK = i;
            //SpeechSynthesizer speak = new SpeechSynthesizer();
            //speak.Speak("干啥子呢？這是?");
            //speak.Speak("Number" + i + "，Number" + i);
            //speak.Speak("One");
            //speak.Dispose();
            //lblNote.Text = "Press which number you heard";
            //string s = @"D:\EdwardCode\FunctionTestBoxTool\FunctionTestBoxTool\bin\Debug\USB3014Tester";
            //MessageBox.Show(SubFunction.getFolerPath(s));

            //fileSW.Filter = "*.txt";
            //fileSW.IncludeSubdirectories = false;
            //fileSW.Path = @"D:\EdwardCode\FunctionTestBoxTool\FunctionTestBoxTool\bin\Debug\USB3014Tester";
            //fileSW.EnableRaisingEvents = true;


            //clear

            //Param.ItemSequence = new Dictionary<int, string>();
            //string st;
            //foreach (int i in Param.ItemSequence.Keys)
            //{
            //    Param.ItemSequence.TryGetValue(i, out st);
            //    MessageBox.Show(st);
            //}

            // load sequnce
            SubFunction.loadSequnce();
            loadTestItemStatus();
            loadItemsToButtons ();
            foreach (TestItem it in AllItems)
            {
                // MessageBox.Show(it.TestItemStatus.ToString());
                chekcButtonUI(btnTestItem1, it);
                chekcButtonUI(btnTestItem2, it);
                chekcButtonUI(btnTestItem3, it);
                chekcButtonUI(btnTestItem4, it);
                chekcButtonUI(btnTestItem5, it);
                chekcButtonUI(btnTestItem6, it);
                chekcButtonUI(btnTestItem7, it);
            }


            if (string.IsNullOrEmpty(Param.portName))
            {
                MessageBox.Show("Comport name cam't be empty!");
                SubFunction.updateMessage(lstStatus, "Comport name cam't be empty!");
                SubFunction.SaveLog(Param.LogType.SysLog, "Comport name cam't be empty!");
                comboSp.Focus();
                return;
            }

            if (openSerialPort(serialPort1, Param.portName))
            {
                //
                timerTest.Enabled = true;
                timerTest.Start();
                //
                btnStart.Enabled = false;
                btnStart.BackColor = Color.Red;
                btnStop.Enabled = true;
                btnStop.BackColor = Color.FromArgb(255, 51, 153, 255);

                btnSetting.Enabled = false;
                btnSetting.BackColor = Color.Red;
                btnReStart.Enabled = false;
                btnReStart.BackColor = Color.Red;

                comboSp.Enabled = false;
                btnRefresh.Enabled = false;
                btnRefresh.BackColor = Color.Red;
                
            }
            //else
            //{
            //}

    

        }

        private void btbStop_Click(object sender, EventArgs e)
        {
            //USB30.TestResult = Param.TestResult.PASS;
            //if (USB30.TestResult == Param.TestResult.WAIT)
            //{
            //    MessageBox.Show("OK");
            //}        
            //Process p = null;
            //p = new Process();
            //p.StartInfo.FileName = @Param.USB30ToolPath;
            //p.Start();

            try
            {
                if (p != null)
                    p.Kill();

              
            }
            catch (Exception)
            {                
             
            }
            closeSerialPort(serialPort1);
            timerTest.Stop();
            //
            btnStart.Enabled = true;
            btnStart.BackColor = Color.FromArgb(255, 51, 153, 255);
            btnStop.Enabled = false ;
            btnStop.BackColor = Color.Red;

            btnSetting.Enabled = true;
            btnSetting.BackColor = Color.FromArgb(255, 51, 153, 255);
            btnReStart.Enabled = true;
            btnReStart.BackColor = Color.FromArgb(255, 51, 153, 255);

            comboSp.Enabled = true;
            btnRefresh.Enabled = true;
            btnRefresh.BackColor = Color.FromArgb(255, 51, 153, 255);
      
            
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {            
           // e.Handled = true;
           // MessageBox.Show(e.KeyChar.ToString());

            //if (e.KeyChar == 27)
            //{
            //    MessageBox.Show("ESC");
            //}

            if (nowTesting == Speaker.ItemName)
            {
                lblChar2.Text = e.KeyChar.ToString();
                lblChar2.ForeColor = Color.Red;

                if (e.KeyChar.ToString() == intTestSPK.ToString())
                {
                    lblChar2.ForeColor = Color.Green;
                    Speaker.TestResult = Param.TestResult.PASS;
                    this.KeyPreview = false;
                    return;
                }
            }

            if (nowTesting == Keyboard.ItemName)
            {

                if (lblChar1.Text == (e.KeyChar.ToString().ToUpper ()))
                    lblChar1.ForeColor = Color.Green;
                //check
                if (checkKeyboardResult())
                {
                    Keyboard.TestResult = Param.TestResult.PASS;
                    this.KeyPreview  = false;
                    return;
                }

                if (lblChar2.Text == (e.KeyChar.ToString().ToUpper ()))
                    lblChar2.ForeColor = Color.Green;
                //check
                if (checkKeyboardResult())
                {
                    Keyboard.TestResult = Param.TestResult.PASS;
                    this.KeyPreview = false;
                    return;
                }
                if (lblChar3.Text == (e.KeyChar.ToString().ToUpper()))
                    lblChar3.ForeColor = Color.Green;
                //check
                if (checkKeyboardResult())
                {
                    Keyboard.TestResult = Param.TestResult.PASS;
                    this.KeyPreview = false;
                    return;
                }
                if (lblChar4.Text == (e.KeyChar.ToString().ToUpper()))
                    lblChar4.ForeColor = Color.Green;
                //check
                if (checkKeyboardResult())
                {
                    Keyboard.TestResult = Param.TestResult.PASS;
                    this.KeyPreview = false;
                    return;
                }   
            }

            if (nowTesting == Audio.ItemName)
            {
                lblChar2.Text = e.KeyChar.ToString();
                lblChar2.ForeColor = Color.Red;

                if (e.KeyChar.ToString() == intTestAudio.ToString())
                {
                    lblChar2.ForeColor = Color.Green;
                    Audio.TestResult = Param.TestResult.PASS;
                    this.KeyPreview = false;
                    return;
                }
            }
        }

        private void timerTest_Tick(object sender, EventArgs e)
        {
            timerTest.Stop();
            TestStartInit();
            //
            SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
            //input SN           
            string SN = Microsoft.VisualBasic.Interaction.InputBox("Input the SN,if entry a endless loop,input 'E.S.C' to quit.", "Input SN");
            if (string.IsNullOrEmpty(SN.Trim()))
            {
                MessageBox.Show("Error,SN may be empty or null,but SN Can't be empty or null,try agian.", "Input SN");
                SubFunction.updateMessage(lstStatus, "Error,SN may be empty or null,but SN Can't be empty or null,try agian.");
                SubFunction.SaveLog(Param.LogType.SysLog, "Error,SN may be empty or null,but SN Can't be empty or null,try agian.");
                timerTest.Start();
                return;
            }
            else if (SN.Trim().ToUpper() == "E.S.C")
            {
                SubFunction.updateMessage(lstStatus, "Input 'E.S.C', quit the test!");
                SubFunction.SaveLog(Param.LogType.SysLog, "Input 'E.S.C', quit the test!");
                return;
            }
            else
            {
                if (checkBarcodeLength(SN, Convert.ToInt16(Param.barcodeLength)) &&checkBarcodeStart(SN, Param.barcodeStart))
                {
                    grbTestInfo.Text = "SN:" + SN.Trim().ToUpper();
                    iTotal += 1;
                    txtTotal.Text = iTotal.ToString();
                    SubFunction.updateMessage(lstStatus, "SN:" + SN.Trim().ToUpper());
                    SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN.Trim().ToUpper());
                    SubFunction.SaveLog(Param.LogType.ScanLog, SN.Trim().ToUpper());
                }
                else
                {
                    timerTest.Start();
                    return;
                }

            }
            Application.DoEvents();
            Delay(100);

            while ( !bReady )
            {
                Delay(500);
                Application.DoEvents();
                lblNote.Text = "确认可以开始测试时，点击确认开始测试";
                lblMegTail.Visible = false;
                lblMsgHead.Visible = false;
                lblChar1.Visible = false;
                lblChar2.Visible = false;
                lblChar3.Visible = false;
                lblChar4.Visible = false;
                lblTime.Visible = false;
                btnReady.Visible = true;
               // btnDocking.Visible = true;
                
                
            }
            //
            foreach (int i in Param.ItemSequence.Keys) //by sequence
            {
                //
                btnReady.Visible = false;
                //

                string st;
                Param.ItemSequence.TryGetValue(i, out st);
                //
                SubFunction.updateMessage(lstStatus, "Now check Test item " + i + "->" + st);
                SubFunction.SaveLog(Param.LogType.SysLog, "Now check Test item " + i + "->" + st);

                if (checkItemStatus(st))
                {
                    SubFunction.updateMessage(lstStatus, "Item " + i + "->" + st + " test status = YES,ready to test!");
                    SubFunction.SaveLog(Param.LogType.SysLog, "Item " + i + "->" + st + " test status = YES,ready to test!");
                    //item by item
                    //=======Vibrator=======
                    if (st == Vibrator.ItemName)
                    {
                        //UI change
                        lblNote.Visible = true;
                        lblNote.Text = @"Waite " + st + " test result";
                        lblChar1.Visible = false;
                        lblChar2.Visible = true;
                        lblChar2.Text = " ";
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        btnDocking.Visible = false;
                        //
                        sendData(serialPort1,"A");
                        //

                        int iVibrator = 0;  
                        while ( Vibrator.TestResult == Param.TestResult.WAIT)
                        {
                            Delay(1000);
                            lblTime.Text = (iVibrator ++).ToString();
                        }

                        if (Vibrator.TestResult == Param.TestResult.PASS)
                        {
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                            loadItemsResultToButton(st, Param.TestResult.PASS);
                            checkBarcodeTestResult(i, st, SN);
                        }
                        if (Vibrator.TestResult == Param.TestResult.FAIL)
                        {
                            //Vibrator.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            //
                            SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            //
                            iFail++;
                            SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            timerTest.Start();
                            return;
                        }
                    }
                SpeakrStart:                   
                    //=======Speaker========
                    if (st == "Speaker")
                    {
                        //
                        //spk UI change
                        lblNote.Visible = true;
                        lblNote.Text = @"Press which number you heard.";
                        lblChar1.Visible = false;
                        lblChar2.Visible = true;
                        lblChar2.Text = " ";
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        lblTime.Visible = true;
                        lblMsgHead.Visible = true;
                        lblMegTail.Visible = true;
                        btnDocking.Visible = false;
                        //
                        nowTesting = st;
                        lblNote.Text = "Press  which number you heard";
                        intTestSPK = CreateRandNum(1, 4);
                        SubFunction.SaveLog(Param.LogType.SysLog, "Speaker,random create number:" + intTestSPK);
                        //SpeechSynthesizer speak = new SpeechSynthesizer();
                        //speak.Speak("數字" + intTestSPK + "，數字" + intTestSPK + ",請按下數字" + intTestSPK);
                        //speak.Dispose();
                        selectVoice(intTestSPK, ref simpleSound);
                        simpleSound.Play();

                        this.KeyPreview = true; // enable main form can accept key press
                        for (int j = 1; j < 11; j++)
                        {
                            lblTime.Text = (11 - j).ToString();
                            Delay(1000);
                            if (Speaker.TestResult == Param.TestResult.PASS)
                            {
                                SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                                loadItemsResultToButton(st, Param.TestResult.PASS);
                                checkBarcodeTestResult(i, st, SN);
                                break;
                            }
                        }

#if DEBUG
                        SubFunction.updateMessage(lstStatus, "IM here");
                        Delay(2000);
                        SubFunction.updateMessage(lstStatus, "IM here");

#endif
                        // time up,check result
                        if (!(Speaker.TestResult == Param.TestResult.PASS))
                        {
                            Speaker.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            ////
                            //SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            //SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            //SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            ////
                            //iFail++;
                            //SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            //timerTest.Start();
                            //return;

                            if (MessageBox.Show(st + " 測試失敗，是否需要再次測試?", "ReTest Item", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                goto SpeakrStart;
                            }
                            else
                            {
                                //
                                SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                                SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                                //
                                iFail++;
                                SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                                timerTest.Start();
                                return;
                            }
                        }

                        if (Speaker.TestResult == Param.TestResult.PASS)
                        {
                            checkBarcodeTestResult(i, st, SN);
                            while (!insertDock)
                            {
                                Delay(500);
                                Application.DoEvents();
                                lblNote.Text = "请插入Dock,插入后点击确定";
                                lblMegTail.Visible = false;
                                lblMsgHead.Visible = false;
                                lblChar1.Visible = false;
                                lblChar2.Visible = false;
                                lblChar3.Visible = false;
                                lblChar4.Visible = false;
                                lblTime.Visible = false;
                                btnDocking.Visible = true;
                                //btnDocking.Enabled = true;                             
                            }
                        }                        
                    }

                    //=======Keyboard=======
                    Application.DoEvents();
                    // Delay(100);
                KeyboardStart:
                    if (st == "Keyboard")
                    {
                        // keyboar UI change     
                        string temp = GenerateCheckCode();
                        lblNote.Visible = true;
                        lblNote.Text = @"Press the below charactors,ignor Minuscule or Majuscule.";
                        lblChar1.Visible = true;
                        lblChar1.ForeColor = Color.Black;
                        lblChar1.Text = temp.Substring(0, 1);
                        lblChar2.Visible = true;
                        lblChar2.ForeColor = Color.Black;
                        lblChar2.Text = temp.Substring(1, 1);
                        lblChar3.Visible = true;
                        lblChar3.ForeColor = Color.Black;
                        lblChar3.Text = temp.Substring(2, 1);
                        lblChar4.Visible = true;
                        lblChar4.ForeColor = Color.Black;
                        lblChar4.Text = temp.Substring(3, 1);
                        nowTesting = st;
                        btnDocking.Visible = false;
                        //
                        lblMegTail.Visible = true;
                        lblTime.Visible = true;
                        lblMsgHead.Visible = true;
                        //
                        this.KeyPreview = true; // enable main form can accept key press
                        for (int j = 1; j < 11; j++)
                        {
                            lblTime.Text = (11 - j).ToString();
                            Delay(1000);
                            if (Keyboard.TestResult == Param.TestResult.PASS)
                            {
                                SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                                Keyboard.TestResult = Param.TestResult.PASS;
                                loadItemsResultToButton(st, Param.TestResult.PASS);
                                checkBarcodeTestResult(i, st, SN);
                                break;
                            }
                        }
                        // time up,check result
                        if (!(Keyboard.TestResult == Param.TestResult.PASS))
                        {
                            Keyboard.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            ////
                            //SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            //SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            //SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            ////
                            //iFail++;
                            //SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            //timerTest.Start();
                            //return;

                            if (MessageBox.Show(st + " 測試失敗，是否需要再次測試?", "ReTest Item", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                goto KeyboardStart;
                            }
                            else
                            {
                                //
                                SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                                SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                                //
                                iFail++;
                                SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                                timerTest.Start();
                                return;
                            }
                        }
                    }

                    //=======Audio==========
                    Application.DoEvents();
                    // Delay(100);
                AudioStart:
                    if (st == "Audio")
                    {
                        //spk UI change
                        lblNote.Visible = true;
                        lblNote.Text = @"Press which number you heard.";
                        lblChar1.Visible = false;
                        lblChar2.Visible = true;
                        lblChar2.Text = " ";
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        btnDocking.Visible = false;
                       
                        //
                        nowTesting = st;
                        lblNote.Text = "Press  which number you heard";
                        intTestAudio = CreateRandNum(1, 9);
                        SubFunction.SaveLog(Param.LogType.SysLog, "Audio,random create number:" + intTestAudio);
                        //SpeechSynthesizer speak = new SpeechSynthesizer();
                        //speak.Speak("數字" + intTestAudio + "，數字" + intTestAudio + ",請按下數字" + intTestAudio);
                        //speak.Dispose();
                        selectVoice(intTestAudio, ref simpleSound);
                        simpleSound.Play();
                        //
                        lblMegTail.Visible = true;
                        lblTime.Visible = true;
                        lblMsgHead.Visible = true;
                        //
                        this.KeyPreview = true; // enable main form can accept key press
                        for (int j = 1; j < 11; j++)
                        {
                            lblTime.Text = (11 - j).ToString();
                            Delay(1000);
                            if (Audio.TestResult == Param.TestResult.PASS)
                            {
                                SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                                SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                                loadItemsResultToButton(st, Param.TestResult.PASS);
                                checkBarcodeTestResult(i, st, SN);
                                break;
                            }
                        }
                        // time up,check result
                        if (!(Audio.TestResult == Param.TestResult.PASS))
                        {
                            Speaker.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            ////
                            //SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            //SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            //SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            ////
                            //iFail++;
                            //SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            //timerTest.Start();
                            //return;
                            if (MessageBox.Show(st + " 測試失敗，是否需要再次測試?", "ReTest Item", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                goto AudioStart;
                            }
                            else
                            {
                                //
                                SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                                SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                                SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                                //
                                iFail++;
                                SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                                timerTest.Start();
                                return;
                            }
                        }
 
                    }

                    //=======USB30==========
                    Application.DoEvents();
                    //Delay(100);
                    if (st == "USB30")
                    {
                        //
                        //UI change
                        lblNote.Visible = true;
                        lblNote.ForeColor = Color.Black;
                        lblNote.Text = "Wait USB30 tool test result...";
                        lblChar1.Visible = false;
                        lblChar2.Visible = false;
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        lblMegTail.Visible = false;
                        lblMsgHead.Visible = false;
                        lblTime.Text = "0";
                        nowTesting = st;
                        btnDocking.Visible = false;
                        //
                        p.StartInfo.FileName = @Param.USB30ToolPath;
                        p.Start();
                        //
                        //start monitor
                        fileSW.Filter = "*.txt";
                        fileSW.IncludeSubdirectories = false;
                        fileSW.Path = @SubFunction.getFolerPath(Param.USB30ToolPath);
                        fileSW.EnableRaisingEvents = true;
                        int iUSB = 0;
                        while (USB30.TestResult == Param.TestResult.WAIT)
                        {
                            Delay(1000);
                            lblTime.Text = (iUSB++).ToString();
#if DEBUG
                            // SubFunction.updateMessage(lstStatus, USB30.TestResult.ToString());
                            //SubFunction.updateMessage(lstStatus, usbResult.ToString());
#endif
                        }
                        //kill process
                        p.Kill();
                        // check result,save log or restart
                        if (USB30.TestResult == Param.TestResult.PASS)
                        {
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                            loadItemsResultToButton(st, Param.TestResult.PASS);
                            checkBarcodeTestResult(i, st, SN);
                        }
                        if (USB30.TestResult == Param.TestResult.FAIL)
                        {
                            USB30.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            //
                            SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            //
                            iFail++;
                            SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            timerTest.Start();
                            return;
                        }
                    }

                    //=======HDMI===========
                    Application.DoEvents();
                    // Delay(100);
                    if (st == "HDMI")
                    {
                        //
                        //UI change
                        lblNote.Visible = true;
                        lblNote.ForeColor = Color.Black;
                        lblNote.Text = "Wait HDMI tool test result...";
                        lblChar1.Visible = false;
                        lblChar2.Visible = false;
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        lblMegTail.Visible = false;
                        lblMsgHead.Visible = false;
                        lblTime.Text = "0";
                        nowTesting = st;
                        btnDocking.Visible = false;
                        //
                        p.StartInfo.FileName = @Param.HDMIVGAToolPath;
                        p.Start();
                        //
                        //start monitor
                        fileSW.Filter = "*.txt";
                        fileSW.IncludeSubdirectories = false;
                        fileSW.Path = @SubFunction.getFolerPath(Param.HDMIVGAToolPath);
                        fileSW.EnableRaisingEvents = true;
                        int iHDMI = 0;
                        while (HDMI.TestResult == Param.TestResult.WAIT)
                        {
                            Delay(1000);
                            lblTime.Text = (iHDMI++).ToString();
#if DEBUG
                            // SubFunction.updateMessage(lstStatus, HDMI.TestResult.ToString());
                            //SubFunction.updateMessage(lstStatus, usbResult.ToString());
#endif
                        }
                        //kill process
                        p.Kill();
                        // check result,save log or restart
                        if (HDMI.TestResult == Param.TestResult.PASS)
                        {
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                            loadItemsResultToButton(st, Param.TestResult.PASS);
                            checkBarcodeTestResult(i, st, SN);
                        }
                        if (HDMI.TestResult == Param.TestResult.FAIL)
                        {
                            HDMI.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            //
                            SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            //
                            iFail++;
                            SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            timerTest.Start();
                            return;
                        }
                    }
                    //=======DCIN=========== 
                    if (st == DCIN.ItemName)
                    {
                        //UI change
                        lblNote.Visible = true;
                        lblNote.Text = @"Waite " + st + " test result";
                        lblChar1.Visible = false;
                        lblChar2.Visible = true;
                        lblChar2.Text = " ";
                        lblChar3.Visible = false;
                        lblChar4.Visible = false;
                        lblTime.Visible = true;
                        btnDocking.Visible = false;
                        //
                        sendData(serialPort1, "D");
                        SubFunction.updateMessage(lstStatus, "Begin test DCIN charge");
                        SubFunction.SaveLog (Param.LogType.SysLog, "Begin test DCIN charge");
                        //
                        int iDCIN = 0;
                        //while ((portString != "E") || (portString != "F"))
                        while ( portString != "E" && portString !="F")
                        {
#if DEBUG
                            SubFunction.updateMessage(lstStatus,(((portString != "E") | (portString != "F")).ToString()));
#endif
                            Delay(1000);
                            lblTime.Text = (iDCIN++).ToString();
                            
                        }
                        SubFunction.updateMessage(lstStatus, "Chaget test OK,begin test discharge.");
                        SubFunction.SaveLog(Param.LogType.SysLog, "Chaget test OK,begin test discharge.");
                        sendData(serialPort1, "G");
                        iDCIN = 0;

                        while (DCIN.TestResult == Param.TestResult.WAIT)
                        {
                            Delay(1000);
                            lblTime.Text = (iDCIN++).ToString();
                        }

                        if (DCIN.TestResult == Param.TestResult.PASS)
                        {
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test PASS.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":PASS");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":PASS", SN);
                            loadItemsResultToButton(st, Param.TestResult.PASS);
                            checkBarcodeTestResult(i, st, SN);
                        }
                        if (DCIN.TestResult == Param.TestResult.FAIL)
                        {
                            //Vibrator.TestResult = Param.TestResult.FAIL;
                            loadItemsResultToButton(st, Param.TestResult.FAIL);
                            SubFunction.updateMessage(lstStatus, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.SysLog, "Item " + st + " test FAIL.");
                            SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":FAIL", SN);
                            //
                            SubFunction.updateMessage(lstStatus, SN + " Test FAIL");
                            SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + SN + ",Result:FAIL");
                            SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + ",Result:FAIL", SN);
                            //
                            iFail++;
                            SubFunction.updateFPY(txtTotal, txtFail, txtFPY, iTotal, iFail);
                            timerTest.Start();
                            return;
                        }
                    }
                }
                else
                {
                    SubFunction.updateMessage(lstStatus, "Item " + i + "->" + st + " test status = NO,skip!");
                    SubFunction.SaveLog(Param.LogType.SysLog, "Item " + i + "->" + st + " test status = NO,skip!");
                    SubFunction.SaveLog(Param.LogType.TestLog, "SN:" + SN + "," + st + ":SKIP", SN);
                    SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + SN + "," + st + ":SKIP", SN);
                    checkBarcodeTestResult(i, st, SN);
                }

                //
                ////check sn result
                //if (i == 7)
                //{
                //    MessageBox.Show("OK");
                //}
            }
            timerTest.Start();
        }

        private void btnReStart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void fileSW_Changed(object sender, FileSystemEventArgs e)
        {
            fileSW.EnableRaisingEvents = false;

#if DEBUG
            SubFunction.updateMessage(lstStatus, "In file watcher");
#endif


            if (nowTesting == USB30.ItemName)
            {
                try
                {
                    string[] lines = File.ReadAllLines(Param.USB30ToolTestResultFile);
                    if (!string.IsNullOrEmpty(lines[0].Trim()))
                    {
                        if (lines[0].Trim() == "PASS")
                        {
                            if (nowTesting == USB30.ItemName)
                            {
                                USB30.TestResult = Param.TestResult.PASS;
                            }
                        }

                        if (lines[0].Trim() == "FAIL")
                        {
                            if (nowTesting == USB30.ItemName)
                            {
                                USB30.TestResult = Param.TestResult.FAIL;
                            }
                        }
                    }

#if DEBUG
                    SubFunction.updateMessage(lstStatus, "USB30:" + Param.USB30ToolTestResultFile);
                    SubFunction.updateMessage(lstStatus, "USB30:" + lines[0]);
#endif
                }
                catch (Exception ex)
                {

                    SubFunction.SaveLog(Param.LogType.SysLog, "Read Result File," + ex.Message);
                }
            }

            if (nowTesting == HDMI.ItemName)
            {
                try
                {
                    string[] lines = File.ReadAllLines(Param.HDMIVGAToolTestResultFile);
                    if (!string.IsNullOrEmpty(lines[0].Trim()))
                    {
                        if (lines[0].Trim() == "PASS")
                        {
                            if (nowTesting == HDMI.ItemName)
                            {
                                HDMI.TestResult = Param.TestResult.PASS;
                            }
                        }

                        if (lines[0].Trim() == "FAIL")
                        {
                            if (nowTesting == HDMI.ItemName)
                            {
                                HDMI.TestResult = Param.TestResult.FAIL;
                            }
                        }
                    }

#if DEBUG
                    SubFunction.updateMessage(lstStatus, "HDMI:" + Param.USB30ToolTestResultFile);
                    SubFunction.updateMessage(lstStatus, "HDMI:" + lines[0]);
#endif
                }
                catch (Exception ex)
                {

                    SubFunction.SaveLog(Param.LogType.SysLog, "Read Result File," + ex.Message);
                }
            }


            //if ( !string.IsNullOrEmpty(lines[0].Trim ()))
            //{
            //    if (lines[0].Trim() == "PASS")
            //    {
            //        if (nowTesting == USB30.ItemName )
            //        {
            //            USB30.TestResult = Param.TestResult.PASS;
            //        }

            //        if (nowTesting == HDMI.ItemName)
            //        {
            //            HDMI.TestResult = Param.TestResult.PASS;
            //        }
            //    }

            //    if (lines[0].Trim() == "FAIL")
            //    {
            //        if (nowTesting == USB30.ItemName)
            //        {
            //            USB30.TestResult = Param.TestResult.FAIL;
            //        }
            //        if (nowTesting == HDMI.ItemName)
            //        {
            //            HDMI.TestResult = Param.TestResult.FAIL;
            //        }
            //    }
            //}

            fileSW.EnableRaisingEvents = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getSerialPort(comboSp);
        }

        #endregion

        #region serial port

                /// <summary>
        /// 獲取串口 
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

        /// <summary>
        /// open serial port
        /// </summary>
        /// <param name="sp">serial port</param>
        /// <param name="portname">comport name</param>
        /// <returns>OK=true,NG=false</returns>
        private bool openSerialPort(SerialPort sp, string portname)
        {
            // bool result = true;
            sp.PortName = portname;
            if (!sp.IsOpen)
            {
                try
                {
                    sp.Open();
                    SubFunction.updateMessage(lstStatus, "Open SerialPort=" + portname + " success.");//Message:" + e.Message);
                    SubFunction.SaveLog(Param.LogType.SysLog , "Open SerialPort=" + portname + " success.");//Message:" + e.Message + "\r\n");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can't open SerialPort=" + portname + ",Message:" + e.Message);
                    SubFunction.updateMessage(lstStatus , "Can't open SerialPort=" + portname + ",Message:" + e.Message);
                    SubFunction.SaveLog(Param.LogType.SysLog , "Can't open SerialPort=" + portname + ",Message:" + e.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// close serial port
        /// </summary>
        /// <param name="sp">OK=true,NG=false</param>
        /// <returns></returns>
        private bool closeSerialPort(SerialPort sp)
        {
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can't close SerialPort=" + sp.PortName.ToString() + ",Message:" + e.Message);
                    SubFunction.updateMessage(lstStatus , "Can't close SerialPort=" + sp.PortName.ToString() + ",Message:" + e.Message);
                    SubFunction.SaveLog(Param.LogType.SysLog , "Can't close SerialPort=" + sp.PortName.ToString() + ",Message:" + e.Message );
                }
            }
            return true;
        }

        /// <summary>
        /// Send string data to serial port
        /// </summary>
        /// <param name="spport">serial port</param>
        /// <param name="strdata">string data</param>
        private void sendData(SerialPort spport, string strdata)
        {
            try
            {
                spport.Write(strdata);
                SubFunction.updateMessage(lstStatus, "Send " + spport.PortName + " " + strdata);
                SubFunction.SaveLog(Param.LogType.SysLog, "Send " + spport.PortName + " " + strdata);
            }
            catch (Exception e)
            {
                SubFunction.updateMessage(lstStatus , "Send " + spport.PortName + " " + strdata + "fail");
                SubFunction.updateMessage(lstStatus , e.Message);
                SubFunction.SaveLog(Param.LogType.SysLog , "Send " + spport.PortName + " " + strdata + "fail," + e.Message);
            }
        }
        #endregion

        #region create random string
        /// <summary>
        /// create random string ,test key board
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                number = rand.Next();
                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                }
                checkCode += code.ToString();
            }
            // Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
            return checkCode;
        }
        #endregion

        #region create ranom nunmber

        /// <summary>
        /// create a random number between min ~ max
        /// </summary>
        /// <param name="min">min</param>
        /// <param name="max">max</param>
        /// <returns></returns>
        public int CreateRandNum(int min, int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(min, max);
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            //初始化测试项目          
            Vibrator.ItemName = "Vibrator";
            Vibrator.TestResult = Param.TestResult.WAIT;
            Speaker.ItemName = "Speaker";
            Speaker.TestResult = Param.TestResult.WAIT;
            Keyboard.ItemName = "Keyboard";
            Keyboard.TestResult  = Param.TestResult.WAIT;
            Audio.ItemName = "Audio";
            Audio.TestResult = Param.TestResult.WAIT;
            USB30 .ItemName = "USB30";
            USB30.TestResult = Param.TestResult.WAIT;
            HDMI.ItemName = "HDMI";
            HDMI.TestResult  = Param.TestResult.WAIT;
            DCIN.ItemName = "DCIN";
            DCIN.TestResult = Param.TestResult.WAIT;
            //初始化ini配置档
            initIniFile();
            //read INI value 
            loadIniFileValue();
            // load sequnce
            SubFunction.loadSequnce();  
            //
            loadItemsToButtons();
            //
            loadTestItemStatus();
            //
            foreach (TestItem it in AllItems)
            {
                // MessageBox.Show(it.TestItemStatus.ToString());
                chekcButtonUI(btnTestItem1, it);
                chekcButtonUI(btnTestItem2, it);
                chekcButtonUI(btnTestItem3, it);
                chekcButtonUI(btnTestItem4, it);
                chekcButtonUI(btnTestItem5, it);
                chekcButtonUI(btnTestItem6, it);
                chekcButtonUI(btnTestItem7, it);
            }
            //
            getSerialPort(comboSp);

        }

        /// <summary>
        /// 初始化INI配置档案
        /// </summary>
        public  void initIniFile()
        {
            //新建的INI文档为空，如果为空，则需要初始化写入默认值
            string st = File.ReadAllText(IniFile.IniFilePath);
            if (string.IsNullOrEmpty (st.Trim ()))
            {
                //sysconfig
                IniFile.IniWriteValue("SysConfig", "COM", string.Empty); //comport 
                IniFile.IniWriteValue("SysConfig", "USB30ToolPath", Param.USB30ToolPath);
                IniFile.IniWriteValue("SysConfig", "USB30ToolTestResultFile", Param.USB30ToolTestResultFile);
                IniFile.IniWriteValue("SysConfig", "HDMIVGAToolPath", Param.HDMIVGAToolPath);
                IniFile.IniWriteValue("SysConfig", "HDMIVGAToolTestResultFile", Param.HDMIVGAToolTestResultFile);
                //testime
                IniFile.IniWriteValue("TestItem", Vibrator.ItemName, Param.TestItemStatus.YES.ToString ());
                IniFile.IniWriteValue("TestItem", Speaker.ItemName, Param.TestItemStatus.YES.ToString());
                IniFile.IniWriteValue("TestItem", Keyboard.ItemName, Param.TestItemStatus.YES.ToString());
                IniFile.IniWriteValue("TestItem", Audio.ItemName, Param.TestItemStatus.YES.ToString());
                IniFile.IniWriteValue("TestItem", USB30.ItemName, Param.TestItemStatus.YES.ToString());
                IniFile.IniWriteValue("TestItem", HDMI.ItemName, Param.TestItemStatus.YES.ToString());
                IniFile.IniWriteValue("TestItem", DCIN.ItemName, Param.TestItemStatus.YES.ToString());
                //sequence
                IniFile.IniWriteValue("Sequence", "1", Vibrator.ItemName);
                IniFile.IniWriteValue("Sequence", "2", Speaker.ItemName);
                IniFile.IniWriteValue("Sequence", "3", Keyboard.ItemName);
                IniFile.IniWriteValue("Sequence", "4", Audio.ItemName);
                IniFile.IniWriteValue("Sequence", "5", USB30.ItemName);
                IniFile.IniWriteValue("Sequence", "6", HDMI.ItemName);
                IniFile.IniWriteValue("Sequence", "7", DCIN.ItemName);
                //Barcode
                IniFile.IniWriteValue("Barcode", "BarcodeLength", "23");
                IniFile.IniWriteValue("Barcode", "BarcodeStart", "SC50F54313");

            }          
        }

        /// <summary>
        /// 读取INI配置档案中的值
        /// </summary>
        public void loadIniFileValue()
        {

            Param.portName = IniFile.IniReadValue("SysConfig", "COM");
          //  MessageBox.Show(IniFile.IniReadValue("SysConfig", "HDMIVGAToolPath"));
            Param.HDMIVGAToolPath = IniFile.IniReadValue("SysConfig", "HDMIVGAToolPath");
            Param.HDMIVGAToolTestResultFile = IniFile.IniReadValue("SysConfig", "HDMIVGAToolTestResultFile");
            Param.USB30ToolPath = IniFile.IniReadValue("SysConfig", "USB30ToolPath");
            Param.USB30ToolTestResultFile = IniFile.IniReadValue("SysConfig", "USB30ToolTestResultFile");

            //

            Param.barcodeLength = IniFile.IniReadValue("Barcode", "BarcodeLength");
            Param.barcodeStart = IniFile.IniReadValue("Barcode", "BarcodeStart");

        }

        /// <summary>
        /// load all test items  to the checked listbox
        /// </summary>
        private void loadTestItemStatus()
        {
            loadIniTestItemStatus(Vibrator , "Vibrator");
            loadIniTestItemStatus(Speaker , "Speaker");
            loadIniTestItemStatus( Keyboard, "Keyboard");
            loadIniTestItemStatus(Audio, "Audio");
            loadIniTestItemStatus(USB30, "USB30");
            loadIniTestItemStatus(HDMI,"HDMI");
            loadIniTestItemStatus(DCIN,"DCIN");
        }

        /// <summary>
        /// load single test item from the ini file
        /// </summary>
        /// <param name="key">test item name,the ini file key </param>
        private void loadIniTestItemStatus(TestItem it, string key)
        {
            //checklistTestItem.Items.Add(key);

            if (IniFile.IniReadValue("TestItem", key) == Param.TestItemStatus.YES.ToString())
            {
                it.TestItemStatus = Param.TestItemStatus.YES;
            }
            if (IniFile.IniReadValue("TestItem", key) == Param.TestItemStatus.NO.ToString())
            {
                it.TestItemStatus = Param.TestItemStatus.NO;
            }
        }

        /// <summary>
        /// load test item name to the button
        /// </summary>
        private  void loadItemsToButtons()
        {
            foreach (int i in Param.ItemSequence.Keys)
            {
                //TestItem st = new TestItem();
                string st;
                Param.ItemSequence.TryGetValue(i, out st);
                if (i == 1)
                    //btnTestItem1.Text = st.ItemName;
                    btnTestItem1.Text = st;
                if (i == 2)
                    //btnTestItem2.Text = st.ItemName;
                    btnTestItem2.Text = st;
                if (i == 3)
                   // btnTestItem3.Text = st.ItemName;
                    btnTestItem3.Text = st;
                if (i == 4)
                   // btnTestItem4.Text = st.ItemName;
                    btnTestItem4.Text = st;
                if (i == 5)
                    //btnTestItem5.Text = st.ItemName;
                    btnTestItem5.Text = st;
                if (i == 6)
                   // btnTestItem6.Text = st.ItemName;
                    btnTestItem6.Text = st;
                if (i == 7)
                   // btnTestItem7.Text = st.ItemName;
                    btnTestItem7.Text = st;
            }
            
        }

        /// <summary>
        /// if result.PASS,button is green,if result.FAIL,button is red      
        /// </summary>
        private void loadItemsResultToButton(string st,Param.TestResult re)
        {
            if (checkSingeButtonByResult(btnTestItem1, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem2, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem3, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem4, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem5, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem6, st, re))
                return;
            if (checkSingeButtonByResult(btnTestItem7, st, re))
                return;

        }

        /// <summary>
        /// check singe button,set ok =true,not find = false
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="st"></param>
        /// <param name="re"></param>
        /// <returns></returns>
        private bool checkSingeButtonByResult(Button btn, string st,Param.TestResult re)
        {
            //if (btnTestItem1.Text == st)
            //{
            //    if (re == Param.TestResult.PASS)
            //    {
            //        btnTestItem1.BackColor = Color.Green;
            //        return;
            //    }
            //    if (re == Param.TestResult.FAIL)
            //    {
            //        btnTestItem1.BackColor = Color.Red;
            //        return;
            //    }
            //}
            if (btn.Text == st)
            {
                if (re == Param.TestResult.PASS)
                {
                    btn.BackColor = Color.Green;
                }
                if (re == Param.TestResult.FAIL)
                {
                    btn.BackColor = Color.Red;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// if it status = YES,yellow and show,=NO,not show
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="it"></param>
        private void chekcButtonUI(Button btn, TestItem it)
        {
            if (btn.Text.Trim() == it.ItemName.Trim())
            {
                if (it.TestItemStatus == Param.TestItemStatus.YES)
                {
                    btn.BackColor = Color.Yellow;
                    btn.ForeColor = Color.Black;
                    btn.Visible = true;
                }
                if (it.TestItemStatus == Param.TestItemStatus.NO)
                {
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.White;
                    btn.Visible = true;
                }

            }
        }

        /// <summary>
        /// test status = YES,true;= NO,false
        /// </summary>
        /// <param name="st">item name</param>
        private bool  checkItemStatus( string st)
        {
            foreach (TestItem  it in AllItems )
            {
                if (st == it.ItemName)
                {
                    if (it.TestItemStatus == Param.TestItemStatus.YES)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 延時子程序
        /// </summary>
        /// <param name="interval">延時的時間，單位毫秒</param>
        private void Delay(double interval)
        {
            DateTime time = DateTime.Now;
            double span = interval * 10000;
            while (DateTime.Now.Ticks - time.Ticks < span)
            {
                Application.DoEvents();
            }

        }

        /// <summary>
        /// keyboard ,PASS= true
        /// </summary>
        /// <returns></returns>
        private bool  checkKeyboardResult()
        {
            if ((lblChar1.ForeColor == Color.Green) && (lblChar2.ForeColor == Color.Green) && (lblChar3.ForeColor == Color.Green) && (lblChar4.ForeColor == Color.Green))
            {
                return true;
            }
            else
            {
                return false;
            }

          
        }

        /// <summary>
        /// When start test,init 
        /// </summary>
        private void TestStartInit()
        {
            ///
            //Vibrator.ItemName = "Vibrator";
            Vibrator.TestResult = Param.TestResult.WAIT;
           // Speaker.ItemName = "Speaker";
            Speaker.TestResult = Param.TestResult.WAIT;
           // Keyboard.ItemName = "Keyboard";
            Keyboard.TestResult = Param.TestResult.WAIT;
           // Audio.ItemName = "Audio";
            Audio.TestResult = Param.TestResult.WAIT;
           // USB30.ItemName = "USB30";
            USB30.TestResult = Param.TestResult.WAIT;
           //HDMI.ItemName = "HDMI";
            HDMI.TestResult = Param.TestResult.WAIT;
           // DCIN.ItemName = "DCIN";
            DCIN.TestResult = Param.TestResult.WAIT;
            ////////
            lblNote.Text = string.Empty;
            lblChar1.Visible = false;
            lblChar2.Visible = false;
            lblChar3.Visible = false;
            lblChar4.Visible = false;
            //
            nowTesting = string.Empty;
            //
            insertDock = false;
            bReady = false;
            
            //
            foreach (TestItem it in AllItems)
            {
                // MessageBox.Show(it.TestItemStatus.ToString());
                chekcButtonUI(btnTestItem1, it);
                chekcButtonUI(btnTestItem2, it);
                chekcButtonUI(btnTestItem3, it);
                chekcButtonUI(btnTestItem4, it);
                chekcButtonUI(btnTestItem5, it);
                chekcButtonUI(btnTestItem6, it);
                chekcButtonUI(btnTestItem7, it);
            }
        }

        private void comboSp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Param.portName = comboSp.SelectedItem.ToString();

            IniFile.IniWriteValue("SysConfig", "COM", Param.portName);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Delay(50);
            if (serialPort1.ReadBufferSize < 0)
                return;
            portString = serialPort1.ReadExisting();

            switch (portString )
            {
                case "B":
                    Vibrator.TestResult = Param.TestResult.PASS;
                    break ;
                case "C":
                    Vibrator.TestResult = Param.TestResult.FAIL;
                    break;
                case "E":                    
                    break;
                case "F":
                    DCIN.TestResult = Param.TestResult.FAIL;
                    break;
                case "H":
                    DCIN.TestResult = Param.TestResult.PASS;//
                    break;
                case "I":
                    DCIN.TestResult = Param.TestResult.FAIL;
                    break;
                default:
                    break;
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void frmMain_DoubleClick(object sender, EventArgs e)
        {
            //SoundPlayer sp = new SoundPlayer(Properties.Resources._1);
            //sp.Play();


        }


        /// <summary>
        /// according to the number,select which number sound to play
        /// </summary>
        /// <param name="i">number</param>
        /// <param name="simplesound">soud</param>
        private void selectVoice(int i, ref SoundPlayer simplesound)
        {

            switch (i)
            {
                case 1:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_1"));
                    break;
                case 2:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_2"));
                    break;
                case 3:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_3"));
                    break;
                case 4:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_4"));
                    break;
                case 5:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_5"));
                    break;
                case 6:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_6"));
                    break;
                case 7:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_7"));
                    break;
                case 8:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_8"));
                    break;
                case 9:
                    simplesound = new SoundPlayer(Properties.Resources.ResourceManager.GetStream("_9"));
                    break;
                default:
                    break;
            }


        }

        private void btnDocking_Click(object sender, EventArgs e)
        {
            insertDock = true;
           // btnDocking.Enabled = false;
            btnDocking.Visible = false;
           // btnDocking.BackColor = Color.Green;
            Application.DoEvents();
            SubFunction.SaveLog(Param.LogType.SysLog, "confirmed dock insert");
            SubFunction.updateMessage(lstStatus, "confirmed dock insert");
           
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            bReady  = true;
            // btnDocking.Enabled = false;
            btnReady.Visible = false;
            // btnDocking.BackColor = Color.Green;
            Application.DoEvents();
            SubFunction.SaveLog(Param.LogType.SysLog, "confirmed start test");
            SubFunction.updateMessage(lstStatus, "confirmed start test");

        }



        /// <summary>
        /// check barcode length,success = true,fail = false
        /// </summary>
        /// <param name="barcode">barcode</param>
        /// <param name="length">barcode length</param>
        /// <returns>success = ture,fail = false </returns>
        private  bool checkBarcodeLength(string barcode, int length)
        {
            if (string.IsNullOrEmpty(barcode.Trim()))
            {
                SubFunction.updateMessage(lstStatus, "Check Barcode Length Error,barcode is empty or null.");
                SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Length Error,barcode is empty or null.");
                return false;
            }

            if (length == 0)
            {
                SubFunction.updateMessage(lstStatus, "Check Barcode Length SKIP,Length is 0.");
                SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Length SKIP,Length is 0.");
                return true;
            }
            else
            {
                if (barcode.Trim().Length == length)
                {
                    SubFunction.updateMessage(lstStatus, "Check Barcode Length Success,Length is " + length);
                    SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Length Success,Length is " + length);
                    return true;
                }
                else
                {
                    SubFunction.updateMessage(lstStatus, "Check Barcode Length Fail,Length is " + length + ",Actual length is " + barcode.Length );
                    SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Length Fail,Length is " + length + ",Actual length is " + barcode.Length);
                    return false;
                }
            }

        }

        /// <summary>
        /// check barocde start with string,success = true, fail = false
        /// </summary>
        /// <param name="barcode">barcode</param>
        /// <param name="startwith">start with string</param>
        /// <returns>success = true,fail = false</returns>
        private  bool checkBarcodeStart(string barcode, string startwith)
        {
            if (string.IsNullOrEmpty(barcode.Trim()))
            {
                SubFunction.updateMessage(lstStatus, "Check Barcode Start Whit Error,barcode is empty or null.");
                SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Start Whit Error,barcode is empty or null.");
                
                return false;
            }

            if (string.IsNullOrEmpty(startwith.Trim()))
            {
                SubFunction.updateMessage(lstStatus, "Check Barcode Start Whit SKIP,start with is empty or null.");
                SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Start Whit SKIP,start with is empty or null.");
               
                return true;
            }
            else
            {

                if (barcode.ToUpper().StartsWith(startwith.ToUpper()))
                {
                    SubFunction.updateMessage(lstStatus, "Check Barcode Start Whit Success,start with is " + startwith);
                    SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Start Whit Success,start with is " + startwith);
                    
                    return true;
                }
                else
                {
                    SubFunction.updateMessage(lstStatus, "Check Barcode Start Whit Fail,start with is " + startwith);
                    SubFunction.SaveLog(Param.LogType.SysLog, "Check Barcode Start Whit Fail,start with is " + startwith);
                 
                    return false;
                }
            }

        }


        /// <summary>
        /// 檢測條碼的測試結果
        /// </summary>
        /// <param name="isequence">當前項目的測試順序號</param>
        /// <param name="itemname">item的名字</param>
        /// <param name="barcode"></param>
        private void checkBarcodeTestResult(int isequence ,string itemname, string barcode)
        {
            if (isequence == 7)
            {
                foreach (TestItem it in AllItems)
                {
                    if (checkItemResult(it, itemname, barcode))
                        break;
                }              
            }          

        }


        /// <summary>
        /// 檢查單個item的測試結果，檢測到結果不是FAIL=true，
        /// </summary>
        /// <param name="it">testitem</param>
        /// <param name="itname">item name </param>
        /// <param name="barcode">barcode </param>
        /// <returns></returns>
        private bool  checkItemResult(TestItem it, string itname,string barcode)
        {
            if (itname == it.ItemName)
            {
                if (it.TestResult != Param.TestResult.FAIL )
                {
                    SubFunction.updateMessage(lstStatus, barcode + " Test PASS");
                    SubFunction.SaveLog(Param.LogType.SysLog, "SN:" + barcode + ",Result:PASS");
                   // MessageBox.Show("ok");
                    SubFunction.SaveLog(Param.LogType.BarLog, "SN:" + barcode + ",Result:PASS", barcode);
                    return true;
                }
            }
            return false;
        }


    }
}
