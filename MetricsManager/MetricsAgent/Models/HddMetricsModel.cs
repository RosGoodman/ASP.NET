using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Models
{
    public class HddMetricsModel : IMetricsModel<double>
    {
        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
