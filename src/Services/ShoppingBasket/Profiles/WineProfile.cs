using AutoMapper;

namespace ShoppingBasket.Profiles
{
    public class WineProfile: Profile
    {
        public WineProfile()
        {
            CreateMap<Entities.Wine, Models.Wine>().ReverseMap();
        }
    }
}
