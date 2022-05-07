// Copyright (C) 2020  Road to Agility

////
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

using System.Collections.Generic;
using System.Collections.Immutable;
using DFlow.Domain.BusinessObjects;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class Project : BaseEntity<EntityId>
    {
        public Project(EntityId id, ProjectName name, ServiceOrder orderNumber, ProjectStatus status, ProjectCode code,
            DateAndTime startDate
            , Money budget, EntityId clientId, Email owner, VersionId version)
            : base(id, version)
        {
            Name = name;
            Code = code;
            StartDate = startDate;
            ClientId = clientId;
            Budget = budget;
            Status = status;
            OrderNumber = orderNumber;
            Owner = owner;
            
            AppendValidationResult(name.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(code.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(startDate.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(clientId.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(budget.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(status.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(orderNumber.ValidationStatus.Errors.ToImmutableList());
            AppendValidationResult(owner.ValidationStatus.Errors.ToImmutableList());
        }

        public ProjectName Name { get; }
        public ProjectCode Code { get; }

        public EntityId ClientId { get; }

        public DateAndTime StartDate { get; }

        public Money Budget { get; }

        public Email Owner { get; }

        public ProjectStatus Status { get; }

        public ServiceOrder OrderNumber { get; }

        public static Project From(EntityId id, ProjectName name, ServiceOrder serviceOrder, ProjectStatus status,
            ProjectCode code, DateAndTime startDate, Money budget, EntityId clientId, Email owner, VersionId version)
        {
            var project = new Project(id, name, serviceOrder, status, code, startDate, budget, clientId, owner,
                version);
            return project;
        }


        public static Project NewRequest(ProjectName name, ServiceOrder serviceOrder, ProjectStatus status,
            ProjectCode code, DateAndTime startDate, Money budget, EntityId clientId, Email owner)
        {
            return From(EntityId.GetNext(), name, serviceOrder, status, code, startDate,
                budget, clientId, owner, VersionId.New());
        }

        public static Project CombineWith(Project current, ProjectDetail detail)
        {
            return From(current.Identity, detail.Name, current.OrderNumber, current.Status,
                current.Code, current.StartDate, detail.Budget, current.ClientId, detail.Owner,
                VersionId.Next(current.Version));
        }


        public static Project Empty()
        {
            return From(EntityId.Empty(), ProjectName.Empty(), ServiceOrder.Empty(), ProjectStatus.Default(),
                ProjectCode.Empty(), DateAndTime.Empty(), Money.Zero(),
                EntityId.Empty(), Email.Empty(),
                VersionId.Empty());
        }

        public override string ToString()
        {
            return
                $"[PROJECT]:[ID: {Identity} Code:{Code}, Name: {Name}, Budget: {Budget} Start date: {StartDate}, Owner: {Owner}, Status: {Status}, Order Number: {OrderNumber}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
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
                ServiceOrder orderNumber)
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

            public ServiceOrder OrderNumber { get; }
        }
    }
}