using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Shared.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddDevelopmentEnvFile(this IHostApplicationBuilder applicationBuilder, string filePath="./dev.env")
    {
        if (!applicationBuilder.Environment.IsDevelopment())
            return applicationBuilder;

        filePath = Path.GetFullPath(filePath);

        if (!File.Exists(filePath))
            throw new FileLoadException($"File with path: {filePath} doesn't exist");

        foreach (string line in File.ReadAllLines(filePath))
        {
            // Skip comments and empty line
            if (string.IsNullOrEmpty(line) || (line.Length > 0 && line[0] == '#'))
                continue;

            // Separate NAME=VALUE
            string[] parts = line.Split('=', 2);

            if (parts.Length != 2)
            {
                Console.WriteLine($"WARN: Skip line '{parts[0]}' in ENV file");
                continue;
            }

            string varName = parts[0];
            string varValue = _getEnvVariablesFromString(parts[1].Trim('"'));

            Environment.SetEnvironmentVariable(varName, varValue);
        }

        Console.WriteLine($"info: Development ENV file loaded ({filePath})");

        return applicationBuilder;
    }

    private static string _getEnvVariablesFromString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        string pattern = @"\$\{([^\}]+)\}";

        foreach (Match match in Regex.Matches(input, pattern))
        {
            var variableName = match.Groups[1].Value;
            var variableValue = Environment.GetEnvironmentVariable(variableName) ?? string.Empty;

            input = input.Replace(match.Value, variableValue);
        }

        return input.ToString();
    }
}
