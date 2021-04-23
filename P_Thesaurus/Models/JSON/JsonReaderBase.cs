using System;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics.Contracts;
using P_Thesaurus.AppBusiness.HistoryReader;

namespace P_Thesaurus.Models.JSON
{
    /// <summary>
    /// JsonReaderBase class
    /// </summary>
    public class JsonReaderBase
    {
        /// <summary>
        /// Lit le fichier
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="path">path</param>
        /// <returns>objet désérialisé</returns>
        public static T DeserializeFile<T>(string path)
        {
            if (path == null || path.Length <= 0)
            {
                throw new ArgumentNullException(path);
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File is non exsitent");
            }

            T obj;

            JsonSerializer deserializer = new JsonSerializer(); // y'a même pas IDisposable, honteux

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (JsonReader jr = new JsonTextReader(sr))
                    {
                        obj = deserializer.Deserialize<T>(jr);
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Serialize file
        /// </summary>
        /// <typeparam name="T">typeof object</typeparam>
        /// <param name="obj">object</param>
        /// <param name="path">path</param>
        public static bool SerializeFile<T>(T obj, string path)
        {
            string[] infos = path.Split(new char['\\']);
            string file = '\\' + infos[infos.Length - 1];
            path = path.Replace(file, "");

            // check if need to crypt
            if (obj is ISecurityCritical)
            {
                // Converting into the interface type to crypt the password
                // this is a pretty bad hack but it work
                ISecurityCritical buffer = (ISecurityCritical)Convert.ChangeType(obj, typeof(ISecurityCritical));

                // crypting
                buffer.Password = PasswordManager.CryptPassword(buffer.Password, null, null);

                // changing type
                obj = (T)Convert.ChangeType(buffer, typeof(T));
            }

            FileStream fs = new FileStream(path, FileMode.Create);

            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(fs))
            {
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jw, obj);
                }
            }

            fs.Close();
            fs.Dispose();

            return true;
        }

        /// <summary>
        /// Convert byte to base 64
        /// </summary>
        /// <param name="array">array</param>
        /// <returns>string</returns>
        public static string ByteToBase64(byte[] array)
        {
            return Convert.ToBase64String(array);
        }

        /// <summary>
        /// Password manager class
        /// </summary>
        [System.Security.SecurityCritical]
        private static class PasswordManager
        {
            /// <summary>
            /// Key
            /// </summary>
            [System.Security.SecurityCritical]
            private static readonly byte[] _KEY = new byte[32] { 53, 243, 196, 42, 231, 133, 171, 31, 196, 95, 162, 164, 142, 216, 71, 121, 72, 107, 226, 70, 151, 208, 221, 26, 173, 145, 128, 238, 15, 76, 49, 174 };

            /// <summary>
            /// IV Vector
            /// </summary>
            [System.Security.SecurityCritical]
            private static readonly byte[] _IV = new byte[16] { 45, 112, 32, 33, 4, 40, 205, 130, 72, 219, 105, 19, 4, 125, 59, 226 };

            /// <summary>
            /// Crypt password
            /// </summary>
            /// <param name="plainPassword">plain password</param>
            /// <param name="key">key</param>
            /// <param name="IV">IV vector</param>
            /// <returns>the crypted password</returns>
            [System.Security.SecurityCritical]
            public static string CryptPassword(string plainPassword, byte[] key, byte[] IV)
            {
                if (plainPassword == null || plainPassword.Length <= 0)
                {
                    throw new ArgumentNullException("plainPassword");
                }

                if (key == null)
                {
                    key = _KEY;
                }

                if (IV == null)
                {
                    IV = _IV;
                }
                Contract.EndContractBlock();

                byte[] encrypted;

                // auto dispose
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = IV;

                    ICryptoTransform cryptoTransform = aes.CreateEncryptor();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainPassword);
                            }

                            encrypted = ms.ToArray();
                        }
                    }
                }

                return ByteToBase64(encrypted);
            }

            /// <summary>
            /// Decrypt the password
            /// </summary>
            /// <param name="cryptedPassword">crypted password</param>
            /// <param name="key">key</param>
            /// <param name="IV">IV</param>
            /// <returns>decrypted password</returns>
            [System.Security.SecurityCritical]
            public static string DecryptPassword(string cryptedPassword, byte[] key, byte[] IV)
            {
                if (cryptedPassword == null)
                {
                    throw new ArgumentNullException("cryptedPassword");
                }

                if (key == null)
                {
                    key = _KEY;
                }

                if (IV == null)
                {
                    IV = _IV;
                }
                Contract.EndContractBlock();

                byte[] passwordByte = Convert.FromBase64String(cryptedPassword);

                string pwd;

                // auto dispose
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = IV;

                    ICryptoTransform cryptoTransform = aes.CreateDecryptor();

                    using (MemoryStream ms = new MemoryStream(passwordByte))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                pwd = sr.ReadToEnd();
                            }
                        }
                    }
                }

                return pwd;
            }
        }
    }
}
