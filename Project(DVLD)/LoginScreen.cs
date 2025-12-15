using DVLD_BusinessLayer;
using DVLD_BusinessLayer.GeneralClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Project_DVLD_
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (cbRememberMe.Checked == false)
                clsGlobal.DeleteLoginInfo();
            else
                StoreLoginInfo();
            
                this.Close();
        }

        private bool Login()
        {
            clsUsers UserLog = clsUsers.FindUser(tbUserName.Text);
            if (UserLog == null)
            {
                MessageBox.Show("UserName/Password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (tbPassword.Text != UserLog.Password)
            if (!clsPasswordHasher.VerifyPassword(tbPassword.Text, UserLog.Password))
            {
                MessageBox.Show("UserName/Password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!UserLog.IsActive)
            {
                MessageBox.Show("This Account is deactivated Contact With Your Manager");
                return false;
            }
            clsGlobal.CurrentUserLogedin = UserLog;
            this.Hide();
            Form frmMain = new frmMainScreen(this);
            frmMain.ShowDialog();
            return true;
        }


        private void StoreLoginInfo()
        {
            string ErrorMessage = "";   
            clsGlobal.StoreLoginInfo(tbUserName.Text.Trim(), tbPassword.Text.Trim(), ref ErrorMessage);
            if (ErrorMessage != "")
            {
                MessageBox.Show("Error!, Can't save login Info! : " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RetrieveLoginInfo(string username, string password)
        {
            string ErrorMessage = "";
            bool LoginInfo = clsGlobal.GetLoginInfo(ref username, ref password, ref ErrorMessage);
            if (!LoginInfo)
            {
                MessageBox.Show("Error!, Can't retrieve login Info! : " + ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            tbUserName.Text = username;
            tbPassword.Text = password;
            return true;
        }


        private void Record_A_Login()
        {
            string SourceName = "DVLD_LoginScreen";
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, "Application");
            }
            EventLog.WriteEntry(SourceName, "User " + tbUserName.Text + " logged in at " + DateTime.Now.ToString(), EventLogEntryType.Information);
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                if (cbRememberMe.Checked == true)
                {
                    StoreLoginInfo();
                  //  clsGlobal.RememberLoginInfo(tbUserName.Text.Trim(), tbPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.DeleteLoginInfo();
                }
                Record_A_Login();
            }
            else
            {
                return;
            }
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            string username = "", password = "";
            cbRememberMe.Checked = RetrieveLoginInfo(username, password); 

            /*/
            if(clsGlobal.RestoreLoginInfo(ref username, ref password))
            {
                tbUserName.Text = username;
                tbPassword.Text = password; 
                cbRememberMe.Checked = true;
            }
            else
            {
                cbRememberMe.Checked = false;
            }
            /*/
        }
    }
}
