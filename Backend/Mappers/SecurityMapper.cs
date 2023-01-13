using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class SecurityMapper : IModelMapper
    {

        public SecurityMapper() { }

        public SecurityModel? Map(Security? security)
        {
            return security == null 
                ? null 
                : new SecurityModel(security.Tiket, security.Name, security.CurrentPrice, security.ChangePerDay);
        }

        public Security? Map(SecurityModel? security)
        {
            return security == null
                ? null
                : new Security(security.Tiket, security.Name, security.CurrentPrice, security.ChangePerDay);
        }
    }
}
