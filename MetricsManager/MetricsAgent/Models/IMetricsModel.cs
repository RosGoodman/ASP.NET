using System;
using System.ComponentModel.DataAnnotations;

namespace MetricsAgent.Models
{
    public class IMetricsModel <T>
    {
        public int ID { get; set; }

        [Required]
        public T Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
