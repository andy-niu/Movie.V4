namespace M.Models.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int? AccessExpiration { get; set; } = 120;

        public int? RefreshExpiration { get; set; } = 1440;
    }
}
