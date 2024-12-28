
using AppStoregStore.WebClient.IAbstractClient;

namespace AppStoregStore.WebClient.Client
{
    public class StoregClient : IStoregClient
    {
        readonly HttpClient Client = new HttpClient();
        public async Task<bool> ExistsProsuct(int id)
        {
            using HttpResponseMessage responseMessage = await Client.GetAsync($"https://localhost:5186/Product/CheckProduct?productId={id.ToString()}");
            responseMessage.EnsureSuccessStatusCode();
            string respond = await responseMessage.Content.ReadAsStringAsync();

            if(respond == "true")
            {
                return true;
            }

            if (respond == "false") 
            {
                return false;
            }
            throw new Exception("Unknow respond");
        }
    }
}
