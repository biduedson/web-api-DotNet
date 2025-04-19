// ===========================
//         IMPORTAÇÕES
// ===========================

using AutoMapper; // Biblioteca responsável pelo mapeamento entre objetos (DTOs e entidades)
using Application.DTOs.Requests; // Contém os DTOs utilizados nas requisições
using Domain.Entities; // Contém as entidades do domínio

namespace Application.Services.AutoMapper
{
    /// <summary>
    /// Classe responsável por configurar os mapeamentos entre os DTOs de entrada (Requests) e as entidades do domínio.
    /// Herda de Profile, que é a classe base do AutoMapper usada para definir os mapeamentos.
    /// Essa classe é registrada no container de injeção de dependência para que o AutoMapper saiba como realizar as conversões.
    /// </summary>
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Construtor onde são definidos os mapeamentos entre as classes.
        /// Cada CreateMap informa ao AutoMapper como transformar um tipo de objeto em outro.
        /// Essa configuração é essencial para facilitar a conversão de DTOs em entidades e vice-versa.
        /// </summary>
        public AutoMapping()
        {
            /// <summary>
            /// Mapeia automaticamente os dados de RegistraEquipamentoRequest para a entidade Equipamento.
            /// </summary>
            CreateMap<RegistraEquipamentoRequest, Equipamento>();

            /// <summary>
            /// Mapeia automaticamente os dados de RegistrarUsuarioRequest para a entidade Usuario.
            /// </summary>
            CreateMap<RegistrarUsuarioRequest, Usuario>();

            /// <summary>
            /// Mapeia automaticamente os dados de ReservaDeEquipamentoRequest para a entidade ReservaDeEquipamento.
            /// </summary>
            CreateMap<ReservaDeEquipamentoRequest, ReservaDeEquipamento>();
        }
    }
}
