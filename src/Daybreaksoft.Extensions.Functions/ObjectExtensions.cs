using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Daybreaksoft.Extensions.Functions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Copy current object values to target object
        /// </summary>
        public static void CopyValueTo(this object obj,
            object target,
            string[] ignorePropertyNames = null,
            Dictionary<string, string> propertyMap = null,
            bool ignoreRefType = true,
            StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (target == null) throw new ArgumentNullException("Target object cannot be null.");

            // Get type
            var currentObjType = obj.GetType();
            var targetObjType = target.GetType();

            // Get properties
            var currentObjProperties = currentObjType.GetProperties();
            var targetObjProperties = targetObjType.GetProperties();

            // Copy value
            foreach (var cp in currentObjProperties)
            {
                // Try to find target property using selected method
                var tp = FindPropertyUsingPropertyName(cp, targetObjProperties, ignorePropertyNames, propertyMap, ignoreRefType, stringComparison);

                // Try to set value
                if (tp != null)
                {
                    try
                    {
                        tp.SetValue(target, cp.GetValue(obj));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidTypeConvertedException($"Set value of Property {cp.Name} failed. {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Find property using same name or alias
        /// </summary>
        private static PropertyInfo FindPropertyUsingPropertyName(
            PropertyInfo property,
            PropertyInfo[] targetProperties,
            string[] ignorePropertyNames,
            Dictionary<string, string> propertyMap,
            bool ignoreRefType,
            StringComparison stringComparison)
        {
            var name = property.Name;
            var alias = string.Empty;

            // Direct return null if the type name wihtin ginore property names list
            if (ignorePropertyNames != null && ignorePropertyNames.Any(p => p.Equals(name, stringComparison)))
            {
                return null;
            }

            // If property map is not null, change name value within map.
            if (propertyMap != null)
            {
                if (propertyMap.ContainsKey(name))
                {
                    name = propertyMap[name];
                }
            }

            // Try to using propery name to find same property
            var tps = targetProperties.Where(p => p.Name.Equals(name, stringComparison));

            if (ignoreRefType)
            {

#if !NetStandard13
                tps = tps.Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string));
#else
                throw new Exception("Can not support to ignore ref type in netstandard1.3.");
#endif
            }

            // Verify whether only have one property
            if (tps.Any())
            {
                if (tps.Count() > 1)
                {
                    throw new MultipleResultException($"Thre are more than one propery named {name}{(string.IsNullOrEmpty(alias) ? "" : $" or {alias}")}");
                }
            }

            return tps.FirstOrDefault();
        }
    }
}
