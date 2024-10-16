using System.Runtime.Serialization;
using Intersect.Plugins;

namespace Rare_Item_Drop_Notifier.Configuration;

public class PluginSettings: PluginConfiguration
{
    public static PluginSettings Settings { get; set; }
    
    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
    }
}