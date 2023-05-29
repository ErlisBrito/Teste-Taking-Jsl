namespace Teste_Taking_Jsl.Domain.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
        public virtual List<Pedido> Pedido { get; set; }
    }
}
