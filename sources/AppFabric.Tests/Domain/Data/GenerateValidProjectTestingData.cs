
using System;
using System.Collections;
using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects;

namespace AppFabric.Tests.Domain.Data
{
    public class GenerateValidProjectTestingData:IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]
            {
                "S20210209O125478593",
                "doug.ramalho@gma.com",
                "PojectFake",
                DateTime.Now,
                134,
                Guid.NewGuid(), 
                "23234234",
                true,
                "ToApprove"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}