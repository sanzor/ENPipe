using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Ext
    {
        public static byte[] ToBytes(this string data)
        {
            var d = Encoding.UTF8.GetBytes(data);
            return d;
        }
        
        public static string FromChars(this char[] data,int count=0)
        {
            var str = string.Join("", data);
            var final = count > 0 ?  str.Substring(0, count):str;
            return final;
        }
        public static byte[] ToBytes(this char[] chars)
        {
            var data = (from b in chars
                        select (byte)b).ToArray();
            return data;
        }
        public static char[] ToChars(this byte[] bytes,int count=0)
        {
            var data = (from b in bytes select (char)b).ToArray();
            
            return data;
        }
        public static char[] ToChars(this string data)
        {
            var d = data.ToArray();
            return d;
        }
    }
}
