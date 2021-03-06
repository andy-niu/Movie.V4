﻿using System;
using System.Collections.Generic;
using System.Text;

namespace M.Common
{
    public class Base64Helper
    {
        /// <summary>  
        　/// Base64加密  
        　/// </summary>  
        　/// <param name="Message"></param>  
        　/// <returns></returns>  
        public static string Base64Encode(string Message)
        {
            byte[] bytes = Encoding.Default.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>  
        　/// Base64解密  
        　/// </summary>  
        　/// <param name="Message"></param>  
        　/// <returns></returns>  
        public static string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.Default.GetString(bytes);
        }
    }
}
