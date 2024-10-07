using Core.CustomAttributes;

namespace Core
{
    public static partial class Enums
    {
        public enum AnimalTypes
        {
            [StringValue("Собаки")]
            Dogs = 1,

            [StringValue("Коти")]
            Cats = 2,

            [StringValue("Гризуни")]
            Rodents = 3,

            [StringValue("Риби")]
            Fish = 4,

            [StringValue("Птахи")]
            Birds = 5,

            [StringValue("Рептилії")]
            Reptiles = 6,

            [StringValue("Інші")]
            Others = 7
        }
    }
}
