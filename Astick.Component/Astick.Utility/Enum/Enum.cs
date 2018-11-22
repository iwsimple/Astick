using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astick.Utility
{
    /// <summary>
    /// 泛型枚举帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Enum<T> where T : struct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumrable()
        {
            Type t = typeof(T);

            if (!t.IsEnum)
                throw new NotSupportedException(string.Format("{0}必须为枚举类型", t));

            EnumQuery<T> query = new EnumQuery<T>();
            return query;
        }

        //类不标记修饰符则为internal类型，仅当前程序集可访问
        //成员不标记修饰符为private类型，仅当前类可访问
        class EnumQuery<T> : IEnumerable<T>
        {
            private List<T> _list;

            public IEnumerator<T> GetEnumerator()
            {
                Array values = Enum.GetValues(typeof(T));
                _list = new List<T>(values.Length);
                foreach (var value in values)
                    _list.Add((T)value);

                return _list.GetEnumerator();
            }

            /// <summary>
            /// 显示引用接口 GetEnumerator()
            /// </summary>
            /// <returns></returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
