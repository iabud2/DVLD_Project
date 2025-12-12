using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DVLD_BusinessLayer.GeneralClasses
{
    static public class clsGlobal
    {
        static public clsUsers CurrentUserLogedin;

        [Obsolete("This Function will be replaced to 'StoreLoginInfo'.")]
        public static bool RememberLoginInfo(string username, string password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\LoginInfo.txt";

                if (username == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return false;
                }

                string LoginData = username + "#//#" + password;

                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine(LoginData);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!, Can't save login Info! : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        [Obsolete("This Function will be replaced to 'GetLoginInfo'.")]
        static public bool RestoreLoginInfo(ref string username, ref string password)
        {
            try
            {

                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string FilePath = CurrentDirectory + "\\LoginInfo.txt";

                if(File.Exists(FilePath)) 
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string DataLine;

                        while ((DataLine = reader.ReadLine()) != null)
                        {
                            string[] Data = DataLine.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            username = Data[0];
                            password = Data[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error!, Can't retrieve Login Info! :" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        static public void StoreLoginInfo(string username, string password, ref string ErrorMessage)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginInfo";
            try
            {
                Registry.SetValue(KeyPath, "UserName", username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", password, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        static public bool GetLoginInfo(ref string username, ref string password, ref string ErrorMessage)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginInfo";
            try
            {
                username = Registry.GetValue(KeyPath, "UserName", RegistryValueKind.String) as string;
                password = Registry.GetValue(KeyPath, "Password", RegistryValueKind.String) as string;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }


        static public void DeleteLoginInfo()
        {
            string RegistryPath = @"SOFTWARE\DVLD_LoginInfo";
            Registry.CurrentUser.DeleteSubKey(RegistryPath, false);
        }
    }
}
