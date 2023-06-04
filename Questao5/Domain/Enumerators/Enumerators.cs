namespace Questao5.Domain.Enumerators
{
    public class Enumerators
    {
        /// <summary>
        /// Erro de parâmetro
        /// </summary>
        public enum ENUM_CUSTOM_ERROR
        {
            /// <summary>
            /// Conta inválida (não existe)
            /// </summary>
            INVALID_ACCOUNT,
            /// <summary>
            /// Conta está inativa
            /// </summary>
            INACTIVE_ACCOUNT,
            /// <summary>
            /// Valor passado pelo request é inválido
            /// </summary>
            INVALID_VALUE,
            /// <summary>
            /// Apenas os tipos “débito” ou “crédito” podem ser aceitos
            /// </summary>
            INVALID_TYPE
        }
    }
}
