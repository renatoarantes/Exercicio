using Questao5.Domain.Entities;
using static Questao5.Domain.Enumerators.Enumerators;

namespace Questao5.Domain.Shared
{
    public static class Utils
    {
        public static void VerifyContaCorrente(ContaCorrente contaCorrente)
        {
            if (contaCorrente == null || contaCorrente.Numero == 0)
            {
                throw new CustomException(ENUM_CUSTOM_ERROR.INVALID_ACCOUNT);
            }

            if (!contaCorrente.Ativo)
            {
                throw new CustomException(ENUM_CUSTOM_ERROR.INACTIVE_ACCOUNT);
            }
        }
    }
}
