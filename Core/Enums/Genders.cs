using Core.CustomAttributes;

namespace Core
{
    public static partial class Enums
    {
        public enum Genders
        {
            [StringValue("Хлопчик")]
            Male = 8,

            [StringValue("Дівчинка")]
            Female = 9,

            [StringValue("Невідомо")]
            Unknown = 154
        }
    }
}
