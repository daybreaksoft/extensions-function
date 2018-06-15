using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Extensions.Functions.Tests.Requires
{
    public class MethodTest
    {
        public int Count { get; set; }

        public void Add(int number)
        {
            Count += number;
        }

        public async Task AddAsync(int number)
        {
            await Task.Run(() => { Count += number; });
        }
    }
}