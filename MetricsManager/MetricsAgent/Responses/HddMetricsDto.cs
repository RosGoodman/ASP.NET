﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class HddMetricsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllHddMetricsResponse
    {
        public List<HddMetricsDto> Metrics { get; set; }
    }
}
