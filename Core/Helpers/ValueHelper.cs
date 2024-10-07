using Bogus;
using Core.Configuration;
using Core.CustomAttributes;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.Helpers
{
    /// <summary>
    /// A static helper class for retrieving application settings and generating random values based on specific instructions.
    /// </summary>
    public static class ValueHelper
    {
        private static readonly Faker faker = new();

        /// <summary>
        /// Retrieves a value from the application settings based on the specified instruction.
        /// </summary>
        /// <param name="instruction">The instruction to specify which app setting to retrieve (e.g., "AppSettings[SettingName]").</param>
        /// <returns>
        /// The string value of the specified app setting, or an empty string if not found.
        /// </returns>
        public static string GetAppSettingsValue(string instruction)
        {
            var appSettings = ConfigurationManager.AppSettings;

            // Extract the setting name from the instruction
            var settingName = instruction.Replace("AppSettings[", "").Replace("]", "");

            // Get the property information for the specified setting name
            var property = typeof(AppSettings).GetProperty(settingName, BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                var value = property.GetValue(appSettings)?.ToString();
                return value ?? string.Empty;  // Return the value or empty string if null
            }

            return string.Empty;  // Return empty if property not found
        }

        /// <summary>
        /// Generates a random value based on the specified random instruction.
        /// </summary>
        /// <param name="randomInstruction">The instruction indicating the type of random value to generate (e.g., "Random[Int(1,100)]").</param>
        /// <returns>
        /// A string representation of the generated random value, or the original instruction if not valid.
        /// </returns>
        public static string GenerateRandomValue(string randomInstruction)
        {
            // Regex pattern to match the random instruction
            var randomInstructionRegex = new Regex(@"Random\[(?<type>[^\(\]]+)(?:\((?<params>[^\)]*)\))?\]");
            var match = randomInstructionRegex.Match(randomInstruction);
            if (!match.Success)
                return randomInstruction;  // Return original instruction if regex does not match

            var generatorType = match.Groups["type"].Value;
            var parameters = match.Groups["params"].Value.Split(',').Select(p => p.Trim()).ToArray();

            // Generate the random value based on the type specified
            return generatorType switch
            {
                "Int" => faker.Random.Int(GetIntParameter(parameters, 0), GetIntParameter(parameters, 1, 100)).ToString(),
                "Double" => faker.Random.Double(GetDoubleParameter(parameters, 0), GetDoubleParameter(parameters, 1, 100.0)).ToString("F2"),
                "FirstName" => faker.Name.FirstName(),
                "LastName" => faker.Name.LastName(),
                "City" => faker.Address.City(),
                "Sentence" => faker.Lorem.Sentence(),
                "Gender" => GetGender(),
                "Specie,Breed" => GetSpecieAndBreed(),
                "AgeUnits" => GetAgeUnits(),
                _ => faker.Random.Word(),  // Default to a random word
            };
        }

        // Generates a random age unit and returns it as a string
        private static string GetAgeUnits()
        {
            var randomAgeUnits = Enums.GetRandomValue<Enums.AgeUnits>();
            return ((int)randomAgeUnits).ToString();
        }

        // Generates a random specie and breed string
        private static string GetSpecieAndBreed()
        {
            var randomSpecie = Enums.GetRandomValue<Enums.AnimalSubTypes>();
            var parentType = Enums.GetAttributeValue(randomSpecie, typeof(ParentTypeAttribute));
            var stringValue = Enums.GetAttributeValue(randomSpecie, typeof(StringValueAttribute));
            return $"{parentType},{stringValue}";  // Return formatted string
        }

        // Gets a random gender string
        private static string GetGender()
        {
            var randomGender = Enums.GetRandomValue<Enums.Genders>();
            var stringValue = Enums.GetAttributeValue(randomGender, typeof(StringValueAttribute));
            return stringValue;
        }

        // Gets an integer parameter from the provided parameters array, with a default value
        private static int GetIntParameter(string[] parameters, int index, int defaultValue = 1)
        {
            return parameters.Length > index && int.TryParse(parameters[index], out var value) ? value : defaultValue;
        }

        // Gets a double parameter from the provided parameters array, with a default value
        private static double GetDoubleParameter(string[] parameters, int index, double defaultValue = 1.0)
        {
            return parameters.Length > index && double.TryParse(parameters[index], out var value) ? value : defaultValue;
        }
    }
}
