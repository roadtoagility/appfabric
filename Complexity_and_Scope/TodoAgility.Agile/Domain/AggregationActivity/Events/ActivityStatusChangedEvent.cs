using System;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;

namespace TodoAgility.Agile.Domain.AggregationActivity.Events
{
    public class ActivityStatusChangedEvent : DomainEvent
    {
        private ActivityStatusChangedEvent(Activity activity)
            : base(DateTime.Now)
        {
            Activity = activity;
        }

        public Activity Activity { get; }

        public static ActivityStatusChangedEvent For(Activity activity)
        {
            return new ActivityStatusChangedEvent(activity);
        }
    }
}