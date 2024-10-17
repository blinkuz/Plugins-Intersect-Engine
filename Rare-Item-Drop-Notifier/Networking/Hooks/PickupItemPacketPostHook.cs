using Intersect;
using Intersect.GameObjects;
using Intersect.Logging;
using Intersect.Network;
using Intersect.Network.Packets.Client;
using Intersect.Server.Networking;
using Rare_Item_Drop_Notifier.Configuration;

namespace Rare_Item_Drop_Notifier.Networking.Hooks;

public class PickupItemPacketPostHook : IPacketHandler<PickupItemPacket>
{
    public bool Handle(IPacketSender packetSender, PickupItemPacket packet)
    {
        if (packetSender is Client client)
        {
            Log.Info("Picked up item: " + packet.UniqueId + " by " + client.Entity.Name);
            if (ItemBase.TryGet(packet.UniqueId, out var item))
            {
                Log.Info("Item: " + item.Name);
                CustomColors.Items.Rarities.TryGetValue(item.Rarity, out var rarityColor);
                Log.Info("Rarity: " + rarityColor);
                
                var message = PluginSettings.Settings.TextMessage
                    .Replace("{player}", client.Entity.Name)
                    .Replace("{item}", item.Name);
                

                return true;
            }
            return false;
        }
        return false;
    }

    public bool Handle(IPacketSender packetSender, IPacket packet) => Handle(packetSender, packet as PickupItemPacket);
}