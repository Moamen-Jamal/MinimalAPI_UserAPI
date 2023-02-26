namespace SAQAYA.UserAPI.Entities.Entities
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool MarketingConsent { get; set; }
    }
}
