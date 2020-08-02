using AutoMapper;
using AvailableGroups.DTOs;
using AvailableGroups.Models;

namespace AvailableGroups.AutoMapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<GroupPageDTO, GroupPageModel>();
            CreateMap<GroupDTO, GroupModel>();
        }
    }
}
