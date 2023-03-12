using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.Game
{
    public record GameRequest(
        [Required] string Name,
        string ImagePath,
        string Platform,
        DateTime DateRelease,
        [Range(0, 10000000)] decimal BasePrice,
        [Range(0, 10000000)] decimal CurrentPrice,
        List<string> Genre,
        Dictionary<string, string> SystemRequirement,
        List<string> Assets, //list of images and videos urls
        double Rating,
        string Publisher,
        string Developer
    );
}

