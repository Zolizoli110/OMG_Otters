using System.IO;
using System.Text.RegularExpressions;

namespace ColorWar.Data;

public static class FileNameParser
{
    
    private static readonly Regex FileNameRegex = new(
        @"^(?<p1>.+?)_vs_(?<p2>.+?)_" +
        @"(?<ts>\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2})\.csv$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase
    );

    public static bool TryGetPlayers(string pathOrFileName, out string player1, out string player2)
    {
        player1 = player2 = string.Empty;

        if (string.IsNullOrWhiteSpace(pathOrFileName))
            return false;

        var fileName = Path.GetFileName(pathOrFileName);

        var m = FileNameRegex.Match(fileName);
        if (!m.Success)
            return false;

        player1 = m.Groups["p1"].Value;
        player2 = m.Groups["p2"].Value;
        return true;
    }
}