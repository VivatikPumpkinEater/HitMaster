    using System.IO;

    public class PathConfig
    {
        public const string ScenesFolder = "Assets/Scenes/Levels";
        public const string LevelDataFolder = "Assets/Resources/LevelData";
        public const string HelpUrl = "https://docs.google.com/document/d/1FVlXZ_WHqF6OETCGdpQyFAZtjEHMy1MK-RYbONuV5Ao/edit?usp=sharing";
        
        public static string StaticData(string locationName)
        {
            var directoryPath = Path.Combine(LevelDataFolder, locationName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return Path.Combine(directoryPath, $"StaticData_{locationName}.asset");
        }
    }
