﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Hashing
{
   public static class UrlHasher
    {
        //
        public static string CreateShortUrl(string longUrl)
        {
            var splittedUrl = longUrl.Split('.')[1];
            var hashedUrl = GetHashString(longUrl).Substring(0,6);

            var shortUrl = "http://"+splittedUrl + "/" + hashedUrl;

            return shortUrl;

        }

        
        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }
}
