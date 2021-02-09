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

using System.Collections.Generic;
using AppFabric.Domain.BusinessObjects.Validations;
using AppFabric.Domain.Framework.BusinessObjects;
using AppFabric.Domain.Framework.Validation;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class Project : ValidationStatus
    {
        private Project(EntityId id, ProjectName name, ProjectCode code, DateAndTime startDate
            , Money budget, EntityId clientId, Email owner, ProjectStatus status
            , ServiceOrderNumber orderNumber, Version currentVersion)
        {
            Id = id;
            Name = name;
            Code = code;
            StartDate = startDate;
            ClientId = clientId;
            Budget = budget;
            Status = status;
            OrderNumber = orderNumber;
            Owner = owner;
            Version = currentVersion;
        }
        public EntityId Id { get; }
        
        public ProjectName Name { get; }
        public ProjectCode Code { get; }
        
        public EntityId ClientId { get; }

        public DateAndTime StartDate { get; }
                
        public Money Budget { get; }
                
        public Email Owner { get; }
                
        public ProjectStatus Status { get; }
        
        public ServiceOrderNumber OrderNumber { get; }
        
        public Version Version { get; }

        public bool IsNew() => Version.Value == 0; 
        
        public static Project From(EntityId id, ProjectName name, ProjectCode code, DateAndTime startDate, Money budget, EntityId clientId, Email owner, ProjectStatus status, ServiceOrderNumber orderNumber, Version version)
        {
            var project = new Project(id, name, code, startDate, budget, clientId, owner, status, orderNumber, version);
            var validator = new ProjectValidator();
            project.SetValidationResult(validator.Validate(project));
            return project;        
        }

        public static Project NewRequest(EntityId id, ProjectName name, ProjectCode code, DateAndTime startDate, Money budget, EntityId clientId)
        {
            return From(id, name, code, startDate, budget, clientId, Email.Empty(), ProjectStatus.Default(), ServiceOrderNumber.Empty(), Version.New());
        }
        
        public static Project CombineWith(Project current, ProjectDetail detail)
        {
            return From(current.Id, detail.Name, current.Code, current.StartDate, detail.Budget, current.ClientId, detail.Owner, detail.Status, detail.OrderNumber, Version.Next(current.Version));
        }
        
        public override string ToString()
        {
            return $"[PROJECT]:[ID: {Id} Code:{Code}, Name: {Name}, Budget: {Budget} Start date: {StartDate}, Owner: {Owner}, Status: {Status}, Order Number: {OrderNumber}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Code;
            yield return Name;
            yield return Budget;
            yield return ClientId;
            yield return StartDate;
            yield return Owner;
            yield return Status;
            yield return OrderNumber;
        }

        public class ProjectDetail
        {
            public ProjectDetail(ProjectName name, Money budget, Email owner, ProjectStatus status,
                ServiceOrderNumber orderNumber)
            {
                Name = name;
                Budget = budget;
                Owner = owner;
                Status = status;
                OrderNumber = orderNumber;
            }
            
            public ProjectName Name { get; }
        
            public Money Budget { get; }
                
            public Email Owner { get; }
                
            public ProjectStatus Status { get; }
        
            public ServiceOrderNumber OrderNumber { get; }
        }
    }
}