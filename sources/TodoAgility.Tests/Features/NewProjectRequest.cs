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

using System;
using System.Globalization;
using System.Threading;
using TodoAgility.Domain.BusinessObjects;
using Xunit;
using Xunit.Gherkin.Quick;


namespace TodoAgility.Tests
{
    [FeatureFile("./Features/NewProjectRequest.feature")]
    public sealed class NewProjectRequest:Feature
    {
        private ProjectName _projectName;
        private ProjectCode _projectCode;
        private DateAndTime _startDate;
        private Project _project;
        
        public NewProjectRequest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        
        [Given(@"Name ([\w\s]+), code (\w+) and start date ([\d\/]+)")]
        public void The_project_parameters_request(string name, string code, DateTime date)
        {
            _projectName = ProjectName.From(name);
            _projectCode = ProjectCode.From(code);
            _startDate= DateAndTime.From(date);
        }
        
        [When(@"The client request a project")]
        public void The_client_request_a_project()
        {
            _project = Project.From(_projectName, _projectCode, _startDate);
        }

        [Then(@"The client see a project request created equals (\w+)")]
        public void The_client_see_a_project_request_created(bool created)
        {
            Assert.Equal(created, _project.ValidationResults.IsValid);
        }
    }
}