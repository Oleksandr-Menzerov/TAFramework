using Core.CustomAttributes;

namespace Core
{
    public static partial class Enums
    {
        public enum PropertyDefinitions
        {
            [StringValue("Вид тварини")]
            Specie = 1,

            [StringValue("Підвид")]
            Breed = 2,

            [StringValue("Стать")]
            Gender = 3
        }
    }
}
