using System;

namespace NameGenerator;

public class NameGeneratorData
{
    public string? Adjective { get; set; }
    public string? Writer { get; set; }
    public string? City { get; set; }
    public string UniqueNameSmall => $"{Adjective}-{Writer}";
    public string UniqueNameLong => $"{Writer} is feeling {Adjective} in {City}";
    public string UniqueName => $"{Adjective}-{Writer}-{City}";
    public static NameGeneratorData Generate()
    {
        var rnd = new Random(DateTime.Now.Second);
        var writer = Writers.WritersWithNobel[rnd.Next(Writers.WritersWithNobel.Length)];
        var city = Cities.NationalCapitals[rnd.Next(Cities.NationalCapitals.Length)];
        var adjective = Adjectives.AdjectiveList[rnd.Next(Adjectives.AdjectiveList.Length)];
        var name = new NameGeneratorData
        {
            Adjective = adjective,
            Writer = writer,
            City = city
        };
        return name;
    }
}