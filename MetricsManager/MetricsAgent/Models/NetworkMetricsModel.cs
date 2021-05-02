using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Models
{
    public class NetworkMetricsModel : IMetricsModel<string>
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
