﻿using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Services;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class HourlyDisplayStrategy : IDisplayStrategy
    {
        public void Display(IEnumerable<EventEntry> events)
        {
            var grouping = events.GroupBy(x => x.OccurredAt.Hour);

            foreach (var group in grouping)
            {
                foreach (var item in group)
                {
                    Console.WriteLine(item.OccurredAt.Hour);
                }
            }
        }
    }
}