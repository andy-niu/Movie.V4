using System;
using System.Collections.Generic;
using System.Text;

namespace M.Common
{
    public static class ClassExtension
    {
        /// <summary>
        /// whether object is null 
        /// null: return true
        /// not null: return false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T t) where T : class
        {
            if (t == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Serialize Reference Object to json
        /// if object is null then return null
        /// </summary>
        /// <typeparam name="T">the type of object</typeparam>
        /// <param name="t"></param>
        /// <returns>json string</returns>
        public static string ToJson<T>(this T t) where T : class
        {
            if (t.IsNull<T>())
            {
                return null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }
    }
}
