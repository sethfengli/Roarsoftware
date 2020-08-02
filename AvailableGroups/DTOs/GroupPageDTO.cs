using System.Collections.Generic;

namespace AvailableGroups.DTOs
{
    public class GroupPageDTO
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public int TotalPages { get; set; }

        public IList<GroupDTO> List { get; set; }
    }
}
