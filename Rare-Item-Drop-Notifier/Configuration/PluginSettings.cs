using System.Runtime.Serialization;
using Intersect.Plugins;

namespace Rare_Item_Drop_Notifier.Configuration;

public class PluginSettings: PluginConfiguration
{
    public static PluginSettings Settings { get; set; }
    
    public List<string> RarityNotifications { get; set; } = new List<string>
    {
        @"Rare",
        @"Epic",
        @"Legendary",
    };
    
    public string TextMessage { get; set; } = "{player} has found a {item}";
    
    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
    }
}