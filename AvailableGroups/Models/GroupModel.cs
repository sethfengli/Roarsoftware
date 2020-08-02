using System;
using System.Linq;

namespace AvailableGroups.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string LogoNameExtension => String.IsNullOrWhiteSpace(LogoUrl) ? String.Empty : GetFileExtensionFromUrl(LogoUrl);

        public static readonly string NoLogoImageUrl = "https://www.worldloppet.com/wp-content/uploads/2018/10/no-img-placeholder.png";

        public static string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            return url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
        }
    }

 

}
