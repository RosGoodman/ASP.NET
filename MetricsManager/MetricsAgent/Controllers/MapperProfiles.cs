using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов
            CreateMap<CpuMetricsModel, CpuMetricsDto>();
            CreateMap<DotNetMetricsModel, DotNetMetricsDto>();
            CreateMap<HddMetricsModel, HddMetricsDto>();
            CreateMap<NetworkMetricsModel, NetworkMetricsDto>();
            CreateMap<RamMetricsModel, RamMetricsDto>();
        }
    }
}
