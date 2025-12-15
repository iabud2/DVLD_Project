using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace DVLD_BusinessLayer.GeneralClasses
{
    public static class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            //at the first we want to check if the Directory Exists or not:
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo Fi = new FileInfo(FileName);
            string ext = Fi.Extension;
            return GenerateGUID() + ext;
        }

        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = @"C:\DVLD_People_Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = DestinationFile;
            return true;
        }
    }

    
    public static class clsPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] key;
            using(var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                key = pbkdf2.GetBytes(KeySize);
            }

            string result = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(key);

            return result;
        }
  
    
        public static bool VerifyPassword(string Password, string StoredPassword)
        {
            var parts = StoredPassword.Split(':');
            if (parts.Length != 2)
            {
                return false; // Invalid stored password format
            }
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedKey = Convert.FromBase64String(parts[1]);


            byte[] key;
            using (var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                key = pbkdf2.GetBytes(KeySize);
            }
            
            return FixedTimeEquals(key, storedKey);
        }


        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            int diff = 0;

            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }
    
    
    }
    


}

