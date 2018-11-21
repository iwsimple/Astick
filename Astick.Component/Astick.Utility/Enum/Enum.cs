using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astick.Utility.Enum
{
    /// <summary>
    /// 泛型枚举帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Enum<T> where T:struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumrable()
        {

        }
    }
}
