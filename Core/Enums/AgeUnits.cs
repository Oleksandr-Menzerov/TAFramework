using Core.CustomAttributes;

namespace Core
{
    public static partial class Enums
    {
        public enum AgeUnits
        {
            [StringValue("Днів")]
            Day = 1,

            [StringValue("Тижнів")]
            Week = 7,

            [StringValue("Місяців")]
            Month = 30,

            [StringValue("Років")]
            Year = 365
        }
    }
}
