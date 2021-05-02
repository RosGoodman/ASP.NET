using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Models
{
    public class CpuMetricsModel : IMetricsModel<int>
    {
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
