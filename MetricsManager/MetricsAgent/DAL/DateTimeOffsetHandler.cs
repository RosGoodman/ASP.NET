using Dapper;
using System;
using System.Data;

namespace MetricsAgent.DAL
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
            => DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(value));

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value.ToUnixTimeSeconds();
    }
}
