using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов
            CreateMap<AgentModel, AgentsDto>();
            CreateMap<CpuMetricsModel, CpuMetricsDto>();
            CreateMap<DotNetMetricsModel, DotNetMetricsDto>();
            CreateMap<HddMetricsModel, HddMetricsDto>();
            CreateMap<NetworkMetricsModel, NetworkMetricsDto>();
            CreateMap<RamMetricsModel, RamMetricsDto>();
        }
    }
}
