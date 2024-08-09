using Microsoft.Extensions.Options;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Ward;
using System.Net.Http.Json;

namespace Reneee.Infrastructure.GHN
{
    public class GhnApiService(HttpClient httpClient, IOptions<GhnSettings> ghnSettings) : IGhnService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly GhnSettings _ghnSettings = ghnSettings.Value;
        private const string API_ADDRESS = "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data";

        public async Task<List<DistrictDto>> GetDisctrictByPronvinceId(int pronvinceId)
        {
            _httpClient.DefaultRequestHeaders.Add("Token", _ghnSettings.ApiToken);
            var requestBody = new
            {
                province_id = pronvinceId
            };
            var response = await _httpClient.PostAsJsonAsync($"{API_ADDRESS}/district", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error fetching districts from GHN");
            }
            var districtResponse = await response.Content.ReadFromJsonAsync<GhnDistrictResponse>();

            if (districtResponse == null || districtResponse.Code != 200)
            {
                throw new Exception("Error fetching districts from GHN");
            }

            return districtResponse.Data;
        }

        public async Task<List<ProvinceDto>> GetProvinces()
        {
            _httpClient.DefaultRequestHeaders.Add("Token", _ghnSettings.ApiToken);
            var response = await _httpClient.GetFromJsonAsync<GhnProvinceResponse>($"{API_ADDRESS}/province");

            if (response == null || response.Code != 200)
            {
                throw new Exception("Error fetching provinces from GHN");
            }
            return response.Data;
        }

        public async Task<List<WardDto>> GetWardByDistrictId(int disctrictId)
        {
            _httpClient.DefaultRequestHeaders.Add("Token", _ghnSettings.ApiToken);
            var requestBody = new
            {
                district_id = disctrictId
            };
            var response = await _httpClient.PostAsJsonAsync($"{API_ADDRESS}/ward?district_id", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error fetching wards from GHN");
            }
            var wardResponse = await response.Content.ReadFromJsonAsync<GhnWardResponse>();

            if (wardResponse == null || wardResponse.Code != 200)
            {
                throw new Exception("Error fetching wards from GHN");
            }

            return wardResponse.Data;
        }

        public class GhnProvinceResponse
        {
            public int Code { get; set; }
            public List<ProvinceDto> Data { get; set; }
        }

        public class GhnDistrictResponse
        {
            public int Code { get; set; }
            public List<DistrictDto> Data { get; set; }
        }

        public class GhnWardResponse
        {
            public int Code { get; set; }
            public List<WardDto> Data { get; set; }
        }
    }
}
