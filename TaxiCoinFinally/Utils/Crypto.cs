﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TokenAPI;

namespace TaxiCoinFinally.Utils
{
    public class Crypto
    {
        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;

        //Encrypt
        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public static ContractFunctions GetContractFunctions()
        {
            return Globals.GetInstance().ContractFunctions;
        }

        public static void DecryptTwoStringsAndGetContractFunctions(out string first, string cipherFirst, out string second, string cipherSecond, string passPhrase, out ContractFunctions contractFunctions)
        {
            first = DecryptString(cipherFirst, passPhrase);
            second = DecryptString(cipherSecond, passPhrase);
            contractFunctions = Globals.GetInstance().ContractFunctions;
        }

        public static void EncryptTwoStrings(out string first, string plainFirst, out string second, string plainSecond, string passPhrase)
        {
            first = EncryptString(plainFirst, passPhrase);
            second = EncryptString(plainSecond, passPhrase);
        }

        public static void DecryptTwoStrings(out string first, string cipherFirst, out string second, string cipherSecond, string passPhrase)
        {
            first = DecryptString(cipherFirst, passPhrase);
            second = DecryptString(cipherSecond, passPhrase);
        }
    }
}