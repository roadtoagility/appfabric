// Copyright (C) 2020  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//


using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsAgileValidationBusinessObjects
    {
        #region validations framework

        [Fact]
        public void Check_BusinessObjectValidations()
        {
            //given
            var text = "";
            

            //when
            var description = Description.From(text);

            //then
            Assert.False(description.ValidationResults.IsValid);
        }
        
        [Fact]
        public void Check_AggregationValidations()
        {
            //given
            var agg = ActivityAggregationRoot.CreateFrom(Description.From(""),EntityId.GetNext(),
                Project.From(EntityId.From(1),Description.From("")));
            

            //when
            var results = agg.ValidationResults;

            //then
            Assert.False(results.IsValid);
            Assert.True(results.Errors.Count > 0);
        }

        #endregion
    }
}