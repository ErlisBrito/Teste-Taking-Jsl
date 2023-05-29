namespace Teste_Taking_Jsl.Application.Models
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public virtual string Cliente { get; set; }
        public virtual string Produto { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
        public string Status { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataAlteracao { get; set; }

    }
}
