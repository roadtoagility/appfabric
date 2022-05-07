
using System.Collections;
using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Tests.Domain.Data
{
    public class GenerateReleaseValidTestingData:IEnumerable<object[]>
    {
        private static readonly EntityId ClientId = EntityId.GetNext();
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                ClientId,
                Release.NewRequest(ClientId),
                Release.NewRequest(ClientId)
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}