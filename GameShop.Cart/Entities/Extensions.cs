namespace GamesShop.Cart.Entities{
    public static class Extension{
        public static CartItemReponse MapCartResponse(this CartItem item){
            return new CartItemReponse(item.GameId, item.Quantity);
        }
    }
}