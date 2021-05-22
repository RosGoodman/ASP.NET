using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers.Requests
{
    public record CpuMetricsRequest
    (
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime
    );
}
