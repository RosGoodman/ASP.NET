﻿using System;

namespace MetricsManager.Models
{
    public class HddMetricsModel
    {
        public int AgentId { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
