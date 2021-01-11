using AutoMapper;
using ShoppingBasket.Entities;
using ShoppingBasket.Models;

namespace ShoppingBasket.Profiles
{
    public class BasketChangeProfile: Profile
    {
        public BasketChangeProfile()
        {
            CreateMap<BasketChange, BasketChangeForPublication>().ReverseMap();
        }
    }
}
