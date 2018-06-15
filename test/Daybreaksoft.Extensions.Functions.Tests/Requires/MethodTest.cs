using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Extensions.Functions.Tests.Requires
{
    public class MethodTest
    {
        public int Count { get; set; }

        public void Add(int number)
        {
            Count += number;
        }
    }
}