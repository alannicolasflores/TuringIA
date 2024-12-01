namespace TuringIA.Server.DTO
{
    public class TokenDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int ExpiraEn { get; set; }
    }
}
