using System.Collections.Generic;

namespace AvailableGroups.Models
{
    public class GroupPageModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public int TotalPages { get; set; }

        public IList<GroupModel> List { get; set; }


        public int NextPage =>  Page + 1 >TotalPages ? 1 : Page + 1;
       
        public int PrePage =>  Page - 1 < 1 ? TotalPages : Page - 1;

        public bool EnablePrevious => Page > 1;

        public bool EnableNext => Page < TotalPages;

        public static readonly string GroupListAPIUri = "https://auth-dev.roarsoftware.com.au/api/v1.0/simpledata/groups";

        public static readonly string defaultPage = "1";
        
        public static readonly string defaultPageSize = "8";


    }
}
