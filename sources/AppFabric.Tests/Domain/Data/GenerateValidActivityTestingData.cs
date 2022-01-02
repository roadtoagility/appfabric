
using System.Collections;
using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Tests.Domain.Data
{
    public class GenerateValidActivityTestingData:IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                Activity.New(EntityId.From("e6ac4bf9-c0af-42f4-a73c-05e6d510bfb6"), Effort.From(3))
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}