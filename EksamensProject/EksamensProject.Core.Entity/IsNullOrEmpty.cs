using System;
using System.IO;
using System.Reflection;

namespace EksamensProject.Core.Entity
{
    public class IsNullOrEmpty
    {
        public static object Check(Object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        string value = (string) pi.GetValue(myObject);
                        if (string.IsNullOrEmpty(value))
                        {
                            throw new InvalidDataException(pi.Name + " is missing");
                        }
                    }
                }
            return null;
            }
        }
    }
