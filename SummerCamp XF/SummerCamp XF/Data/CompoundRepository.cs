using SummerCamp_XF.Models;
using SummerCamp_XF.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp_XF.Data
{
    public class CompoundRepository : ICompoundRepository
    {
        readonly HttpClient client = new HttpClient();

        public CompoundRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Compound>> GetCompounds()
        {
            var response = await client.GetAsync("api/Compounds");
            if (response.IsSuccessStatusCode)
            {
                List<Compound> Compounds = await response.Content.ReadAsAsync<List<Compound>>();
                return Compounds;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }

        public async Task<Compound> GetCompound(int ID)
        {
            var response = await client.GetAsync($"api/Compounds/{ID}");
            if (response.IsSuccessStatusCode)
            {
                Compound Compounds = await response.Content.ReadAsAsync<Compound>();
                return Compounds;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task AddCompound(Compound CompoundToAdd)
        {
            var response = await client.PostAsJsonAsync("api/Compounds", CompoundToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateCompound(Compound CompoundToUpdate)
        {
            var response = await client.PutAsJsonAsync($"api/Compounds/{CompoundToUpdate.ID}", CompoundToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteCompound(Compound CompoundToDelete)
        {
            var response = await client.DeleteAsync($"api/Compounds/{CompoundToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }

        }
    }
}
