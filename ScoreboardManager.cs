using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace spacewar;

class ScoreboardManager
{
    public ScoreboardManager()
    {
        if (!File.Exists("scoreboard.json"))
        {
            var fileStream = File.Create("scoreboard.json");
            fileStream.Close();
            using (StreamWriter outputFile = new StreamWriter("scoreboard.json"))
            {
                outputFile.WriteLine("[]");
            }
        }
    }

    public void Add(string username, int score)
    {
        var playerScore = new PlayerScore();
        playerScore.Username = username;
        playerScore.Score = score;
        List<PlayerScore> destination = this.GetSorted();
        destination.Add(playerScore);
        string jsonString = JsonSerializer.Serialize(destination, new JsonSerializerOptions() { WriteIndented = true });
        using (StreamWriter outputFile = new StreamWriter("scoreboard.json"))
        {
            outputFile.WriteLine(jsonString);
        }
    }


    public List<PlayerScore> GetSorted()
    {
        using (StreamReader r = new StreamReader("scoreboard.json"))
        {
            string json = r.ReadToEnd();
            var source = JsonSerializer.Deserialize<List<PlayerScore>>(json);
            source.Sort((x, y) => y.Score - x.Score);
            return source;
        }
    }


}
public class PlayerScore
{
    public string Username { get; set; }
    public int Score { get; set; }
}
