using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GGroup5
{
    public class HighSCorePage : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;

        private List<HighScoreEntry> highScores;

        private const string HighScoreFileName = "highscores.txt";

        public HighSCorePage(Game1 game, SpriteBatch spriteBatch, SpriteFont font) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;

            highScores = LoadHighScores();

            while (highScores.Count < 5)
            {
                highScores.Add(new HighScoreEntry { Name = "Player", Score = 0 });
            }
        }

        private List<HighScoreEntry> LoadHighScores()
        {
            if (File.Exists(HighScoreFileName))
            {
                try
                {
                    string[] lines = File.ReadAllLines(HighScoreFileName);

                    List<HighScoreEntry> loadedScores = new List<HighScoreEntry>();

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                        {
                            loadedScores.Add(new HighScoreEntry { Name = parts[0], Score = score });
                        }
                    }

                    return loadedScores;
                }
                catch (IOException)
                {
                    return new List<HighScoreEntry>();
                }
            }
            else
            {
                return new List<HighScoreEntry>();
            }
        }

        private void SaveHighScores()
        {
            try
            {
                List<string> lines = new List<string>();

                foreach (var entry in highScores)
                {
                    lines.Add($"{entry.Name}:{entry.Score}");
                }

                File.WriteAllLines(HighScoreFileName, lines);
            }
            catch (IOException)
            {
            }
        }

        public void ShowHighScore(int score)
        {
            highScores.Add(new HighScoreEntry { Name = "Player", Score = score });
            highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
            highScores = highScores.Take(5).ToList();
            SaveHighScores();
            DrawHighScores();
        }

        private void DrawHighScores()
        {
            spriteBatch.Begin();

            Vector2 position = new Vector2(50, 50);

            foreach (var entry in highScores)
            {
                string entryText = $"{entry.Name}: {entry.Score}";
                spriteBatch.DrawString(font, entryText, position, Color.White);
                position.Y += 20;
            }

            spriteBatch.End();
        }


        private class HighScoreEntry
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}
