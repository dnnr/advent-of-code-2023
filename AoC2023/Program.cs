using AoC2023;

var days = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(IDay))).ToList();
days.Sort();
foreach (var day in days)
{
    var inputFile = $"inputs/{day.Name.ToLower()}.txt";
    var input = File.ReadAllText(inputFile);

    var part1 = day.GetMethod("Part1")?.Invoke(null, new object?[] { input });
    var part2 = day.GetMethod("Part2")?.Invoke(null, new object?[] { input });
    
    Console.WriteLine($"{day.ToString()} part 1: {part1}");
    Console.WriteLine($"{day.ToString()} part 1: {part2}");
}