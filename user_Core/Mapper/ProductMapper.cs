using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace user_Core.Mapper
{
    public class ProductMapper
    {
        public static void ProductMappings()
        {
            TypeAdapterConfig<user_Core.entity.User, user_Core.DTO.UserResponseDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
        }
    }
}
