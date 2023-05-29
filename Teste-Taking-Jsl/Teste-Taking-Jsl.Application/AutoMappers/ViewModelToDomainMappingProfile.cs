using AutoMapper;
using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Application.AutoMappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<PedidoViewModel, Pedido>();
        }
    }
}
