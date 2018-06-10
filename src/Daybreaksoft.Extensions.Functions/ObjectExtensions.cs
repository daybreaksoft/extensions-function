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
        public static void CopyValueTo(this object obj, object target, CopyValueMethod method = CopyValueMethod.UsingPropertyNameOrAlias, bool ingoreConvertTypeFailed = false, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (target == null) throw new ArgumentNullException("Target object cannot be null.");

            // Get type
            var currentObjType = obj.GetType();
            var targetObjType = target.GetType();

            // Get properties
            PropertyInfo[] currentObjProperties, targetObjProperties;

#if !NetStandard13
            currentObjProperties = currentObjType.GetProperties();
            targetObjProperties = targetObjType.GetProperties();
#else
            currentObjProperties = System.Reflection.TypeExtensions.GetProperties(currentObjType);
            targetObjProperties = System.Reflection.TypeExtensions.GetProperties(targetObjType);
#endif

            // Copy value
            foreach (var cp in currentObjProperties)
            {
                PropertyInfo tp = null;

                // Try to find target property using selected method
                if (method == CopyValueMethod.UsingPropertyNameOrAlias)
                {
                    tp = FindPropertyUsingPropertyNameOrAlias(cp, targetObjProperties, stringComparison);
                }

                // Try to set value
                if (tp != null)
                {
                    try
                    {
                        tp.SetValue(target, cp.GetValue(obj));
                    }
                    catch (Exception ex)
                    {
                        // Sometimes two properties have different type, it will cause convert failed.
                        // If ingore this issue, it will skip, otherwise throw exception
                        if (!ingoreConvertTypeFailed)
                        {
                            throw new InvalidTypeConvertedException($"Set value of Property {cp.Name} failed. {ex.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find property using same name or alias
        /// </summary>
        private static PropertyInfo FindPropertyUsingPropertyNameOrAlias(PropertyInfo property, PropertyInfo[] targetProperties, StringComparison stringComparison)
        {
            var name = property.Name;
            var alias = string.Empty;

            // Try to using propery name to find same property
            var tps = targetProperties.Where(p => p.Name.Equals(name, stringComparison));

            if (!tps.Any())
            {
                // Try to get alias
                var aliasAttr = property.GetCustomAttribute<AliasAttribute>();

                // Try to using alias to find same property
                if (aliasAttr != null && !string.IsNullOrEmpty(aliasAttr.Name))
                {
                    alias = aliasAttr.Name;

                    tps = targetProperties.Where(p => p.Name.Equals(alias, stringComparison));
                }
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
