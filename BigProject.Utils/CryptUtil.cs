using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BigProject.Utils
{
    public class CryptUtil
    {
        private readonly static string iv4DES = "GEAPI@#$MorningStar!@#";

        public static string DESEncrypt(string EncryptString, string EncryptKey)
        {
            string encryptedString;

            using (SymmetricAlgorithm des3 = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;
                des3.Key = Encoding.ASCII.GetBytes(MD5Encrypt(EncryptKey).Substring(0, 24));
                des3.IV = Encoding.ASCII.GetBytes(MD5Encrypt(iv4DES).Substring(0, 8));
                des3.Mode = System.Security.Cryptography.CipherMode.ECB;
                des3.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = des3.CreateEncryptor(des3.Key, des3.IV);
                byt = Encoding.UTF8.GetBytes(EncryptString);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                encryptedString = HttpServerUtility.UrlTokenEncode(ms.ToArray());
            }

            return encryptedString;
        }

        public static string DESDecrypt(string DecryptString, string DecryptKey)
        {
            string decryptedString;
            byte[] byt = HttpServerUtility.UrlTokenDecode(DecryptString);
            using (SymmetricAlgorithm des3 = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                des3.Key = Encoding.ASCII.GetBytes(MD5Encrypt(DecryptKey).Substring(0, 24));
                des3.IV = Encoding.ASCII.GetBytes(MD5Encrypt(iv4DES).Substring(0, 8));
                des3.Mode = System.Security.Cryptography.CipherMode.ECB;
                des3.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = des3.CreateDecryptor(des3.Key, des3.IV);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                decryptedString = Encoding.UTF8.GetString(ms.ToArray());
            }

            return decryptedString;

        }


        public static string MD5Encrypt(string EncryptString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            string strEncrypt = "";

            try
            {
                strEncrypt = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(EncryptString))).Replace("-", "");
            }
            catch (ArgumentException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { md5.Clear(); }

            return strEncrypt;
        }

    }
}
