using AutoMapper;
using JSandwiches.MVC.IRespository;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace JSandwiches.MVC.Respository
{
    public class GenConsumRespo<T> : IGenConsumRespo<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly IMapper _mapper;

        public GenConsumRespo(string endpoint, IMapper mapper)
        {
            _httpClient = new HttpClient();
            _endpoint = endpoint;
            _httpClient.BaseAddress = new Uri(endpoint);
            _mapper = mapper;
        }

        public async Task<bool> Create(T entity)
        {
            var createDtoTypeName = $"Create{entity.GetType().Name}";
            var createDtoType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.Name == createDtoTypeName);
            var mappedEntity = _mapper.Map(entity, typeof(T), createDtoType);

            var sEntity = JsonConvert.SerializeObject(mappedEntity);

            var content = new StringContent(sEntity, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<List<T>?> GetAll()
        {
            var list = new List<T>();
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<T>>(data);
                return list;
            }
            return list;
        }

        public async Task<(T?, string?)> GetById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var entity = JsonConvert.DeserializeObject<T>(data);
                if (entity != null)
                    return (entity, null);
            }
            return (null, response.StatusCode.ToString());
        }

        public async Task<bool> Update(T entity, int id)
        {
            var sEntity = JsonConvert.SerializeObject(entity);
            var content = new StringContent(sEntity, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/{id}", content);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
