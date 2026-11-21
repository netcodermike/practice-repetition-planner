using System.Text.Json;

try
{
    if (args.Length != 1) throw new ArgumentException("Pass one JSON input file");
    using var data = JsonDocument.Parse(File.ReadAllText(args[0]));
    Console.WriteLine(JsonSerializer.Serialize(Analyze(data.RootElement)));
    return 0;
}
catch (Exception error) when (error is IOException or JsonException or FormatException
                               or ArgumentException or InvalidOperationException
                               or KeyNotFoundException or OverflowException)
{
    Console.Error.WriteLine(error.Message);
    return 2;
}

static object Analyze(JsonElement input)
{
    int total = input.GetProperty("total").GetInt32();
    var weights = input.GetProperty("weights").EnumerateObject().ToDictionary(item => item.Name, item => item.Value.GetDecimal());
    decimal sum = weights.Values.Sum();
    if (total < 0 || sum <= 0 || weights.Values.Any(weight => weight < 0))
        throw new ArgumentException("Use a nonnegative repetition budget and positive total weight");
    var quota = weights.ToDictionary(item => item.Key, item => item.Value / sum * total);
    var plan = quota.ToDictionary(item => item.Key, item => (int)decimal.Floor(item.Value));
    int remainder = total - plan.Values.Sum();
    foreach (var item in quota.OrderByDescending(item => item.Value - plan[item.Key]).Take(remainder))
        plan[item.Key]++;
    return new { plan, total };
}
