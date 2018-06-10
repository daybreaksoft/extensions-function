using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Extensions.Functions
{
    /// <summary>
    /// Alias attribute
    /// </summary>
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
