using Newtonsoft.Json;
using UnityEngine;

public static class JsonConfig
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.None,
        TypeNameHandling = TypeNameHandling.Objects
    };
    
    /// <summary> Загрузить статические данные локации </summary>
    public static LocationStaticData LoadStaticData(TextAsset staticDataAsset)
    {
        var staticData = JsonConvert.DeserializeObject<LocationStaticData>(staticDataAsset.text, Settings);
        if (staticData != null)
            return staticData;

        Debug.LogError("Location Static Data no found");
        return null;
    }
}