using GameShop.Cart.Dtos;

namespace GameShop.Cart.Client{
    public class GameClient{
        public readonly HttpClient _httpClient;

        public GameClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IReadOnlyCollection<CatalogItemReponse>> GetCartItemsAsync(){
            var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemReponse>>("games");
            return items;
        }
    }
}