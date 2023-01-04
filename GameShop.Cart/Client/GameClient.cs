namespace GamesShop.Cart.Client{
    public class GameClient{
        public readonly HttpClient _httpClient;

        public GameClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}