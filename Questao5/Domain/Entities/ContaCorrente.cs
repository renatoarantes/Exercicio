using static Questao5.Domain.Enumerators.Enumerators;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; }

        public Int64 Numero { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }   
    }
}
