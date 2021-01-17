using AutoMapper;
using CteleportTechAssignment.Core.dtos;
using CteleportTechAssignment.Core.Model;

namespace CteleportTechAssignment.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Mapping from Entities to DTOs
            CreateMap<Airport, AirportDto>();            
            #endregion

            ForAllMaps((maps, expression) => maps.PreserveReferences = true);

            #region Mapping from DTOs to Entities
           
            CreateMap<AirportDto, Airport>();
           
            #endregion

        }
    }
}
