using AutoMapper;
using AvailableGroups.DTOs;
using AvailableGroups.Helpers;
using AvailableGroups.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace AvailableGroups.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;
       // private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IApiService apiService)
        {
            _logger = logger;
            _mapper = mapper;
            _apiService = apiService;
           // _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated )
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                DateTime accessTokenExpiresAt = DateTime.Parse(
                                                   await HttpContext.GetTokenAsync("expires_at"),
                                                   CultureInfo.InvariantCulture,
                                                   DateTimeStyles.RoundtripKind);

                if ( !String.IsNullOrEmpty(accessToken) && accessTokenExpiresAt > DateTime.Now)
                {
                    return RedirectToAction("GroupList", "Home");
                }
                
            }
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> GroupList([FromQuery] string page, [FromQuery] string pagesize)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var GroupPageModel = await GetGroupListAsync(page, pagesize, accessToken);
            return View(GroupPageModel);    
         }

        #nullable enable
        public async Task<GroupPageModel> GetGroupListAsync(string? page, string? pagesize, string? accessToken)
        {

            var APIUri = $"{GroupPageModel.GroupListAPIUri}?page={page ?? GroupPageModel.defaultPage}&pagesize={pagesize ?? GroupPageModel.defaultPageSize}";

            var data = await _apiService.GetApiDataAsync(APIUri, accessToken);
           
            return _mapper.Map<GroupPageModel>(JsonConvert.DeserializeObject<GroupPageDTO>(data));
           
        }

        public async Task<IActionResult> GetGroupImageFile(string logoUrl,  string logoExtension)
        {
            byte[]? logoImage = null;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    logoImage = await webClient.DownloadDataTaskAsync(logoUrl);
                }
            }
            catch
            {
                //do nothing if error
            }
            return File(logoImage, $"image/{logoExtension}");
        }
    }

}
