using AutoMapper;
using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Application.AutoMappers
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
