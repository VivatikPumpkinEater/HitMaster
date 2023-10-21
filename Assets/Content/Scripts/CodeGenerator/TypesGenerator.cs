using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

public static class TypesGenerator
{
    private const string FolderPath = "Assets/Content/Scripts/CodeGenerator";
    
    public static void Generate(bool assetDatabaseRefresh, string className, List<string> data)
    {
        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        File.WriteAllText($"{FolderPath}/{className}.cs", Create(className, data));

        if (assetDatabaseRefresh)
            AssetDatabase.Refresh();
    }
       
    private static string Create(string className, List<string> list)
    {
        var stb = new StringBuilder();
        stb.Append($"public enum {className}\n");
        stb.Append("{\n");

        foreach (var value in list)
            stb.Append($"\t {value},\n");
            
        stb.Append("}\n");

        return stb.ToString();
    }
}