﻿using System;
using System.Linq;
using System.Reflection;

namespace Daybreaksoft.Extensions.Functions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Find properties by custom attributes
        /// </summary>
        public static PropertyInfo[] FindProperties<TAttrubite>(this Type type) where TAttrubite : Attribute
        {
            var properties = type.GetProperties();

            return properties.Where(p => p.GetCustomAttributes<TAttrubite>().Any()).ToArray();
        }

        /// <summary>
        /// Find only one property by custom attribute
        /// It will throw exception if there are more than one property 
        /// </summary>
        public static PropertyInfo FindProperty<TAttrubite>(this Type type) where TAttrubite : Attribute
        {
            var properties = type.FindProperties<TAttrubite>();

            // verify whether there are more than one property
            if (properties.Count() > 1)
            {
                throw new MultipleResultException($"Found more then one key property within {type.FullName}");
            }

            return properties.FirstOrDefault();
        }

        /// <summary>
        /// Invoke method
        /// </summary>
        public static object InvokeMethod(this Type type, string methodName, object obj, params object[] parameters)
        {
            var method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new NullReferenceException($"Cannot found method {methodName} in {type.Namespace}.");
            }

            return method.Invoke(obj, parameters);
        }
    }
}
