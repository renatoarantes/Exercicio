using static Questao5.Domain.Enumerators.Enumerators;

namespace Questao5.Domain.Shared
{
    public class CustomException : Exception
    {
        public CustomException(ENUM_CUSTOM_ERROR enumCustomErro)
            : base(enumCustomErro.ToString())
        {
        }
    }
}
