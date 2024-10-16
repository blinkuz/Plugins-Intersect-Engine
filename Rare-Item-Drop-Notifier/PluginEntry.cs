using Blinkuz.Plugins.Tools.Logging;
using Intersect.Network.Packets.Client;
using Intersect.Plugins;
using Intersect.Server.Plugins;
using Rare_Item_Drop_Notifier.Configuration;
using Rare_Item_Drop_Notifier.Networking.Hooks;

namespace Rare_Item_Drop_Notifier;

public class PluginEntry: ServerPluginEntry
{
    public override void OnBootstrap(IPluginBootstrapContext context)
    {
        base.OnBootstrap(context);
        Logger.Context = context;
        Logger.WriteToConsole = true;
        PluginSettings.Settings = context.GetTypedConfiguration<PluginSettings>();
        
        Logger.Write(LogLevel.Info, String.Format("Version : {0}", context.Manifest.Version));
        Logger.Write(LogLevel.Info, String.Format("Author  : {0}", context.Manifest.Authors));
        Logger.Write(LogLevel.Info, String.Format("Homepage: {0}", context.Manifest.Homepage));
        
        Logger.Write(LogLevel.Info, "Registering post hooks...");
        if (!context.Packet.TryRegisterPacketPostHook<PickupItemPacketPostHook, PickupItemPacket>(out _))
        {
            Logger.Write(LogLevel.Warning, $"Failed to register {nameof(PickupItemPacket)} packet post hook.");
            Environment.Exit(-5);
        }
    }
    
    public override void OnStart(IServerPluginContext context)
    {
       
    }

    public override void OnStop(IServerPluginContext context)
    {
        
    }
}