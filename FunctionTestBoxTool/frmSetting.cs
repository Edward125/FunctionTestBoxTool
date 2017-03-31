using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edward;
using System.IO;

namespace FunctionTestBoxTool
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private void ftmSetting_Load(object sender, EventArgs e)
        {
            this.Text = "Setting ,Ver.:" + Application.ProductVersion;
            //load old sequence
            loadTestItemOldSequence();
            //load item status
            loadTestItemStatus();
            //load tool file path
            txtHDMIToolPath.Text = Param.HDMIVGAToolPath;
            txtHDMIResultFile.Text = Param.HDMIVGAToolTestResultFile;
            txtUSB30ToolPath.Text = Param.USB30ToolPath;
            txtUSB30ResultFile.Text = Param.USB30ToolTestResultFile;
            txtHDMIToolPath.SetWatermark("Double Click to select the HDMI tool(HVTTester.exe)");
            txtHDMIResultFile.SetWatermark("Double Click to select the HDMI result file(HVTResult.txt)");
            txtUSB30ToolPath.SetWatermark("Double Clikc to select the USB30 tool(USB3014Tester.exe)");
            txtUSB30ResultFile.SetWatermark("Double Click to select the USB30 result file(USB3014Tester_Result.txt)");
            checkFileExits(txtHDMIToolPath, Param.HDMIVGAToolPath);
            checkFileExits(txtHDMIResultFile, Param.HDMIVGAToolTestResultFile);
            checkFileExits(txtUSB30ToolPath, Param.USB30ToolPath);
            checkFileExits(txtUSB30ResultFile, Param.USB30ToolTestResultFile);

            //debug
           // checklistTestItem.SetItemCheckState(4, CheckState.Checked);


            txtBarcodeLength.Text = Param.barcodeLength;
            txtBarcodeStart.Text = Param.barcodeStart.ToUpper();
           
        }

        private void btnSaveSequence_Click(object sender, EventArgs e)
        {
            if (lstNewSequnce.Items.Count != 7)
            {
                MessageBox.Show("Please edit all the item.");
            }
            SaveSequenceToIniFile();
            MessageBox.Show("Save the sequence complete.");                  

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lstNewSequnce.Items.Clear();
        }


        /// <summary>
        /// load old test sequence to the list box 
        /// </summary>
        private void loadTestItemOldSequence()
        {
            for (int i = 1; i < 8; i++)
            {
                lstOldSequence.Items.Add(IniFile.IniReadValue("Sequence", i.ToString()));
            }
        }



        /// <summary>
        /// load all test items  to the checked listbox
        /// </summary>
        private void loadTestItemStatus()
        {

            loadIniTestItemStatus("Vibrator");
            loadIniTestItemStatus("Speaker");
            loadIniTestItemStatus("Keyboard");
            loadIniTestItemStatus("Audio");
            loadIniTestItemStatus("USB30");
            loadIniTestItemStatus("HDMI");
            loadIniTestItemStatus("DCIN");


        }


        /// <summary>
        /// load single test item from the ini file
        /// </summary>
        /// <param name="key">test item name,the ini file key </param>
        private void loadIniTestItemStatus(string key)
        {
               checklistTestItem.Items.Add(key);
            if (IniFile.IniReadValue("TestItem", key) == Param.TestItemStatus.YES.ToString()) 
            {
                //checklistTestItem.Items.Add(key);
               // checklistTestItem.SetItemChecked(checklistTestItem.Items.Count-1, true);
                checklistTestItem.SetItemCheckState(checklistTestItem.Items.Count - 1, CheckState.Checked);
            }
            if (IniFile.IniReadValue("TestItem", key) == Param.TestItemStatus.NO.ToString()) 
            {
               // checklistTestItem.Items.Add(key);
                //checklistTestItem.SetItemChecked(checklistTestItem.Items.Count-1 , false);
                checklistTestItem.SetItemCheckState(checklistTestItem.Items.Count - 1, CheckState.Unchecked);
            }
        }


        /// <summary>
        /// check file exits,if exit,text box is green,if not exit,text box is red
        /// </summary>
        /// <param name="textbox"></param>
        /// <param name="filepath"></param>
        private void checkFileExits(TextBox textbox, string filepath)
        {

            if (File.Exists(@filepath))
                textbox.BackColor = Color.LimeGreen;
            else
                textbox.BackColor = Color.Red;

        }

        /// <summary>
        /// select file ,input the file path to  textbox
        /// </summary>
        /// <param name="textbox">textbox</param>
        private void selectfile(TextBox textbox)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "exe file|*.exe|txt file|*.txt|all files|*.*";
            if (openfiledialog.ShowDialog() == DialogResult.OK)
            {
                textbox.Text = openfiledialog.FileName;
            }            
        }

        private void txtHDMIToolPath_DoubleClick(object sender, EventArgs e)
        {
            selectfile(txtHDMIToolPath);
            checkFileExits(txtHDMIToolPath, txtHDMIToolPath.Text.Trim());
        }

        private void txtHDMIToolPath_TextChanged(object sender, EventArgs e)
        {
            Param.HDMIVGAToolPath = @txtHDMIToolPath.Text.Trim();
            checkFileExits(txtHDMIToolPath, Param.HDMIVGAToolPath);
            IniFile.IniWriteValue("SysConfig", "HDMIVGAToolPath", Param.HDMIVGAToolPath);
        }

        private void txtHDMIResultFile_DoubleClick(object sender, EventArgs e)
        {
            selectfile(txtHDMIResultFile );
            checkFileExits(txtHDMIResultFile ,txtHDMIResultFile.Text.Trim());
        }

        private void txtHDMIResultFile_TextChanged(object sender, EventArgs e)
        {
            Param.HDMIVGAToolTestResultFile = @txtHDMIResultFile.Text.Trim();
            checkFileExits (txtHDMIResultFile,Param.HDMIVGAToolTestResultFile);
            IniFile.IniWriteValue("SysConfig", "HDMIVGAToolTestResultFile", Param.HDMIVGAToolTestResultFile);
        }

        private void txtUSB30ToolPath_DoubleClick(object sender, EventArgs e)
        {
            selectfile(txtUSB30ToolPath );
            checkFileExits(txtUSB30ToolPath , txtUSB30ToolPath.Text.Trim());
        }

        private void txtUSB30ToolPath_TextChanged(object sender, EventArgs e)
        {
            Param.USB30ToolPath = @txtUSB30ToolPath.Text.Trim();
            checkFileExits(txtUSB30ToolPath, Param.USB30ToolPath);
            IniFile.IniWriteValue("SysConfig", "USB30ToolPath", Param.USB30ToolPath);
        }

        private void txtUSB30ResultFile_DoubleClick(object sender, EventArgs e)
        {
            selectfile(txtUSB30ResultFile);
            checkFileExits(txtUSB30ResultFile, txtUSB30ResultFile.Text.Trim());
        }

        private void txtUSB30ResultFile_TextChanged(object sender, EventArgs e)
        {
            Param.USB30ToolTestResultFile = @txtUSB30ResultFile.Text.Trim();
            checkFileExits(txtUSB30ResultFile, Param.USB30ToolTestResultFile);
            IniFile.IniWriteValue("SysConfig", "USB30ToolTestResultFile", Param.USB30ToolTestResultFile);
        }

        private void checklistTestItem_Click(object sender, EventArgs e)
        {
            //this.Enabled = false;
            // MessageBox.Show(checklistTestItem.SelectedItem.ToString());
            if (checklistTestItem.GetItemChecked(checklistTestItem.SelectedIndex))
            {
                // MessageBox.Show("OK");

                checklistTestItem.SetItemCheckState(checklistTestItem.SelectedIndex, CheckState.Unchecked);

                IniFile.IniWriteValue("TestItem", checklistTestItem.SelectedItem.ToString(), Param.TestItemStatus.NO.ToString());
                return;
            }
            else
            {
                // MessageBox.Show("NG");
                checklistTestItem.SetItemCheckState(checklistTestItem.SelectedIndex, CheckState.Checked);
                IniFile.IniWriteValue("TestItem", checklistTestItem.SelectedItem.ToString(), Param.TestItemStatus.YES.ToString());
                return;
            }
            //this.Enabled = true;
        }

        /// <summary>
        /// save test items status to the ini file
        /// </summary>
        private void SaveTestItemStatusToIniFile()
        {
            for (int i = 0; i < checklistTestItem.Items.Count; i++)
            {
                if (checklistTestItem.GetItemChecked (i))
                {
                   // MessageBox.Show(i.ToString() + "-> checked");
                    IniFile.IniWriteValue("TestItem", checklistTestItem.Items[i].ToString (), Param.TestItemStatus.YES.ToString());
                   //return;
                }


                if (!checklistTestItem.GetItemChecked(i))
                {
                   // MessageBox.Show(i.ToString() + "-> unchecked");
                    IniFile.IniWriteValue("TestItem", checklistTestItem.Items[i].ToString(), Param.TestItemStatus.NO.ToString());
                   // return;
                }

            }
        }

        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTestItemStatusToIniFile();
            if (lstNewSequnce.Items.Count != 7)
            {
                //MessageBox.Show("Please edit all the item.");
                lstNewSequnce.Items.Clear();
            }
            else
            {
                SaveSequenceToIniFile();
            }
       
        }

        private void lstOldSequence_DoubleClick(object sender, EventArgs e)
        {
            if (lstNewSequnce.Items.IndexOf(lstOldSequence.SelectedItem) == -1)
                lstNewSequnce.Items.Add(lstOldSequence.SelectedItem);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstOldSequence.SelectedIndex == -1)
                return;

            if (lstNewSequnce.Items.IndexOf(lstOldSequence.SelectedItem) == -1)
                lstNewSequnce.Items.Add(lstOldSequence.SelectedItem);
        }

        private void lstNewSequnce_DoubleClick(object sender, EventArgs e)
        {
            if (lstNewSequnce.SelectedIndex == -1)
                return;
            lstNewSequnce.Items.RemoveAt(lstNewSequnce.SelectedIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstNewSequnce.SelectedIndex == -1)
                return;
            lstNewSequnce.Items.RemoveAt(lstNewSequnce.SelectedIndex);
        }

        /// <summary>
        /// save the test items test sequence to the ini file
        /// </summary>
        private void SaveSequenceToIniFile()
        {
            for (int i = 0; i < lstNewSequnce.Items.Count; i++)
            {
                IniFile.IniWriteValue("Sequence", (i + 1).ToString(), lstNewSequnce.Items[i].ToString());
            }
        }

        private void txtBarcodeLength_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcodeLength.Text.Trim()))
            {
                IniFile.IniWriteValue("Barcode", "BarcodeLength", "0");
                Param.barcodeLength = "0";
                //txtBarcodeLength.Text = "0";
            }
            else
            {
                IniFile.IniWriteValue("Barcode", "BarcodeLength", txtBarcodeLength.Text.Trim());
                Param.barcodeLength = txtBarcodeLength.Text.Trim(); 
            }

                     

        }

        private void txtBarcodeLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            SubFunction.onlyInputNumber(sender, e);
        }

        private void txtBarcodeStart_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBarcodeStart.Text.Trim () ))
            {
                IniFile.IniWriteValue("Barcode", "BarcodeStart", "");
                Param.barcodeStart = "";
            }

            IniFile.IniWriteValue("Barcode", "BarcodeStart", txtBarcodeStart.Text.Trim());
            Param.barcodeStart  = txtBarcodeStart.Text.Trim();
        }
        

    }
}
