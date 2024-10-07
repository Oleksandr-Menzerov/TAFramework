using Core.CustomAttributes;

namespace Core
{
    public static partial class Enums
    {
        public enum AnimalSubTypes
        {
            [StringValue("Німецька вівчарка"), ParentType("Собаки")]
            GermanShepherd = 26,
            [StringValue("Лабрадор-ретрівер"), ParentType("Собаки")]
            LabradorRetriever = 27,
            [StringValue("Йоркширський тер'єр"), ParentType("Собаки")]
            YorkshireTerrier = 28,
            [StringValue("Мопс"), ParentType("Собаки")]
            Pug = 29,
            [StringValue("Золотистий ретрівер"), ParentType("Собаки")]
            GoldenRetriever = 30,
            [StringValue("Французький бульдог"), ParentType("Собаки")]  
            FrenchBulldog = 31,
            [StringValue("Бігль"), ParentType("Собаки")]
            Beagle = 32,
            [StringValue("Чихуахуа"), ParentType("Собаки")]
            Chihuahua = 33,
            [StringValue("Такса"), ParentType("Собаки")]
            Dachshund = 34,
            [StringValue("Сибірський хаскі"), ParentType("Собаки")]
            SiberianHusky = 35,
            [StringValue("Американський стаффордширський тер'єр"), ParentType("Собаки")]
            AmericanStaffordshireTerrier = 36,
            [StringValue("Кокер-спанієль"), ParentType("Собаки")]
            CockerSpaniel = 37,
            [StringValue("Джек-рассел-тер'єр"), ParentType("Собаки")]
            JackRussellTerrier = 38,
            [StringValue("Ши-тцу"), ParentType("Собаки")]
            ShihTzu = 39,
            [StringValue("Вест-хайленд-уайт-тер'єр"), ParentType("Собаки")]
            WestHighlandWhiteTerrier = 40,
            [StringValue("Мальтійська болонка"), ParentType("Собаки")]
            MalteseDog = 41,
            [StringValue("Боксер"), ParentType("Собаки")]
            Boxer = 42,
            [StringValue("Доберман"), ParentType("Собаки")]
            Doberman = 43,
            [StringValue("Шарпей"), ParentType("Собаки")]
            SharPei = 44,
            [StringValue("Акіта-іну"), ParentType("Собаки")]
            AkitaInu = 45,
            [StringValue("Британська короткошерста"), ParentType("Коти")]
            BritishShorthair = 46,
            [StringValue("Шотландська висловуха"), ParentType("Коти")]
            ScottishFold = 47,
            [StringValue("Мейн-кун"), ParentType("Коти")]
            MaineCoon = 48,
            [StringValue("Сфінкс"), ParentType("Коти")]
            Sphynx = 49,
            [StringValue("Перська"), ParentType("Коти")]
            Persian = 50,
            [StringValue("Сіамська"), ParentType("Коти")]
            Siamese = 51,
            [StringValue("Європейська короткошерста"), ParentType("Коти")]
            EuropeanShorthair = 52,
            [StringValue("Бенгальська"), ParentType("Коти")]
            Bengal = 53,
            [StringValue("Регдолл"), ParentType("Коти")]
            Ragdoll = 54,
            [StringValue("Турецька ангора"), ParentType("Коти")]
            TurkishAngora = 55,
            [StringValue("Американська короткошерста"), ParentType("Коти")]
            AmericanShorthair = 56,
            [StringValue("Невська маскарадна"), ParentType("Коти")]
            NevaMasquerade = 57,
            [StringValue("Норвезька лісова"), ParentType("Коти")]
            NorwegianForest = 58,
            [StringValue("Сомалійська"), ParentType("Коти")]
            Somali = 59,
            [StringValue("Абіссинська"), ParentType("Коти")]
            Abyssinian = 60,
            [StringValue("Орієнтальна"), ParentType("Коти")]
            Oriental = 61,
            [StringValue("Екзотична короткошерста"), ParentType("Коти")]
            ExoticShorthair = 62,
            [StringValue("Девон-рекс"), ParentType("Коти")]
            DevonRex = 63,
            [StringValue("Саванна"), ParentType("Коти")]
            Savannah = 64,
            [StringValue("Бомбейська"), ParentType("Коти")]
            Bombay = 65,
            [StringValue("Тонкінська"), ParentType("Коти")]
            Tonkinese = 66,
            [StringValue("Хом'як"), ParentType("Гризуни")]
            Hamster = 83,
            [StringValue("Миша"), ParentType("Гризуни")]
            Mouse = 84,
            [StringValue("Морська свинка"), ParentType("Гризуни")]
            GuineaPig = 85,
            [StringValue("Шиншила"), ParentType("Гризуни")]
            Chinchilla = 86,
            [StringValue("Щур"), ParentType("Гризуни")]
            Rat = 87,
            [StringValue("Кролик"), ParentType("Гризуни")]
            Rabbit = 88,
            [StringValue("Заєць"), ParentType("Гризуни")]
            Hare = 89,
            [StringValue("Капібара"), ParentType("Гризуни")]
            Capybara = 90,
            [StringValue("Песець"), ParentType("Гризуни")]
            ArcticFox = 91,
            [StringValue("Білка"), ParentType("Гризуни")]
            Squirrel = 92,
            [StringValue("Золота рибка"), ParentType("Риби")]
            Goldfish = 93,
            [StringValue("Гупі"), ParentType("Риби")]
            Guppy = 94,
            [StringValue("Тернеція"), ParentType("Риби")]
            BlackSkirt = 95,
            [StringValue("Скалярія"), ParentType("Риби")]
            Angelfish = 96,
            [StringValue("Молінезія"), ParentType("Риби")]
            Molly = 97,
            [StringValue("Сом-панда"), ParentType("Риби")]
            PandaCory = 98,
            [StringValue("Барбус"), ParentType("Риби")]
            Barb = 99,
            [StringValue("Ампулярія"), ParentType("Риби")]
            AppleSnail = 100,
            [StringValue("Коридорас"), ParentType("Риби")]
            Corydoras = 101,
            [StringValue("Торакатум"), ParentType("Риби")]
            Thoracatum = 102,
            [StringValue("Пецилія"), ParentType("Риби")]
            Poecilia = 103,
            [StringValue("Тетра"), ParentType("Риби")]
            Tetra = 104,
            [StringValue("Неон"), ParentType("Риби")]
            Neon = 105,
            [StringValue("Мечоносець"), ParentType("Риби")]
            Swordtail = 106,
            [StringValue("Лабео"), ParentType("Риби")]
            Labeo = 107,
            [StringValue("Рамірезі"), ParentType("Риби")]
            RamsCichlid = 108,
            [StringValue("Даніо"), ParentType("Риби")]
            Danio = 109,
            [StringValue("Морський коник"), ParentType("Риби")]
            Seahorse = 110,
            [StringValue("Хвилястий папуга"), ParentType("Птахи")]
            Budgerigar = 111,
            [StringValue("Корелла"), ParentType("Птахи")]
            Cockatiel = 112,
            [StringValue("Жако"), ParentType("Птахи")]
            AfricanGrey = 113,
            [StringValue("Какаду"), ParentType("Птахи")]
            Cockatoo = 114,
            [StringValue("Амадін"), ParentType("Птахи")]
            Amadina = 115,
            [StringValue("Нерозлучники"), ParentType("Птахи")]
            Lovebird = 116,
            [StringValue("Ара"), ParentType("Птахи")]
            Macaw = 117,
            [StringValue("Лорікет"), ParentType("Птахи")]
            Lorikeet = 118,
            [StringValue("Канарка"), ParentType("Птахи")]
            Canary = 119,
            [StringValue("Голуб"), ParentType("Птахи")]
            Pigeon = 120,
            [StringValue("Павлін"), ParentType("Птахи")]
            Peacock = 121,
            [StringValue("Фазан"), ParentType("Птахи")]
            Pheasant = 122,
            [StringValue("Індик"), ParentType("Птахи")]
            Turkey = 123,
            [StringValue("Перепілка"), ParentType("Птахи")]
            Quail = 124,
            [StringValue("Качка"), ParentType("Птахи")]
            Duck = 125,
            [StringValue("Гуска"), ParentType("Птахи")]
            Goose = 126,
            [StringValue("Півень"), ParentType("Птахи")]
            Rooster = 127,
            [StringValue("Курка"), ParentType("Птахи")]
            Chicken = 128,
            [StringValue("Пелікан"), ParentType("Птахи")]
            Pelican = 129,
            [StringValue("Страус"), ParentType("Птахи")]
            Ostrich = 130,
            [StringValue("Ящірка"), ParentType("Рептилії")]
            Lizard = 131,
            [StringValue("Гекон"), ParentType("Рептилії")]
            Gecko = 132,
            [StringValue("Ігуана"), ParentType("Рептилії")]
            Iguana = 133,
            [StringValue("Змія"), ParentType("Рептилії")]
            Snake = 134,
            [StringValue("Сухопутна черепаха"), ParentType("Рептилії")]
            LandTurtle = 135,
            [StringValue("Морська черепаха"), ParentType("Рептилії")]
            SeaTurtle = 136,
            [StringValue("Водна черепаха"), ParentType("Рептилії")]
            WaterTurtle = 137,
            [StringValue("Хамелеон"), ParentType("Рептилії")]
            Chameleon = 138,
            [StringValue("Тритон"), ParentType("Інші")]
            Newt = 139,
            [StringValue("Павук"), ParentType("Інші")]
            Spider = 140,
            [StringValue("Лемур"), ParentType("Інші")]
            Lemur = 141,
            [StringValue("Скорпіон"), ParentType("Інші")]
            Scorpion = 142,
            [StringValue("Саламандра"), ParentType("Інші")]
            Salamander = 143,
            [StringValue("Єнот"), ParentType("Інші")]
            Raccoon = 144,
            [StringValue("Альпака"), ParentType("Інші")]
            Alpaca = 145,
            [StringValue("Кінь"), ParentType("Інші")]
            Horse = 146,
            [StringValue("Лама"), ParentType("Інші")]
            Llama = 147,
            [StringValue("Дикобраз"), ParentType("Інші")]
            Porcupine = 148,
            [StringValue("Їжак"), ParentType("Інші")]
            Hedgehog = 149,
            [StringValue("Осел"), ParentType("Інші")]
            Donkey = 150,
            [StringValue("Коза"), ParentType("Інші")]
            Goat = 151,
            [StringValue("Корова"), ParentType("Інші")]
            Cow = 152,
            [StringValue("Свиня"), ParentType("Інші")]
            Pig = 153,
            [StringValue("Інше"), ParentType("Собаки")]
            OtherDog = 156,
            [StringValue("Інше"), ParentType("Коти")]
            OtherCat = 157,
            [StringValue("Інше"), ParentType("Гризуни")]
            OtherRodent = 158,
            [StringValue("Інше"), ParentType("Риби")]
            OtherFish = 159,
            [StringValue("Інше"), ParentType("Птахи")]
            OtherBird = 160,
            [StringValue("Інше"), ParentType("Рептилії")]
            OtherReptile = 161,
            [StringValue("Інше"), ParentType("Інші")]
            OtherAnimal = 162
        }
    }
}
