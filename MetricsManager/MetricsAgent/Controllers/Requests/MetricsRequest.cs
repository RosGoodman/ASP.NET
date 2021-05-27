using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers.Requests
{
    public record MetricsRequest
    (
        [FromRoute] DateTimeOffset FromTime,
        [FromRoute] DateTimeOffset ToTime
    );
}
