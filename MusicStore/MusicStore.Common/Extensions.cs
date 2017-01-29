using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Common
{
    public static class Extensions
    {
        /// <summary>
        /// MatchWord returns collection where a word is exact match in the name or surname
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="fieldName">Name of field with fullname value</param>
        /// <param name="word">Word searching for</param>
        /// <returns></returns>
        public static IEnumerable<T> MatchWord<T>(this IEnumerable<T> source,string fieldName, string word)
        {
            List<T> list = new List<T>();
            
            foreach (T item in source)
            {
                //use reflection to get field and then compare field to value
                foreach (var field in item.GetType().GetProperties())
                {
                    if (field.Name == fieldName)
                    {
                        string[] stringSeparators = new string[] { " " };
                        string fullName = (string)field.GetValue(item);
                        if (!string.IsNullOrEmpty(fullName))
                        {
                            var names = fullName.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                           if(names.Any(c=>c.Equals(word))){
                                list.Add(item);
                                break;
                            }
                            break;
                        }                       
                    }
                }
            }

            return list;
        }  
    }
}
