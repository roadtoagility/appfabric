using System;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;

namespace TodoAgility.Agile.Domain.AggregationActivity.Events
{
    public class ActivityAddedEvent : DomainEvent
    {
        private ActivityAddedEvent(EntityId id, Description description, EntityId projectId, ActivityStatus status)
            : base(DateTime.Now)
        {
            Description = description;
            Id = id;
            ProjectId = projectId;
            Status = status;
        }

        public Description Description { get; }
        public EntityId Id { get; }
        public EntityId ProjectId { get; }
        public ActivityStatus Status { get; }

        public static ActivityAddedEvent For(Activity activity)
        {
            return new ActivityAddedEvent(activity.Id,activity.Description, activity.ProjectId, activity.Status);
        }
    }
}