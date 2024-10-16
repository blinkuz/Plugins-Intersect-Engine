using Intersect.Logging;
using Intersect.Network;
using Intersect.Network.Packets.Client;
using Intersect.Server.Networking;

namespace Rare_Item_Drop_Notifier.Networking.Hooks;

public class PickupItemPacketPostHook : IPacketHandler<PickupItemPacket>
{
    public bool Handle(IPacketSender packetSender, PickupItemPacket packet)
    {
        if (packetSender is Client client)
        {
            Log.Info("Picked up item: " + packet.UniqueId + " by " + client.Entity.Name);
            return true;
        }

        return false;
    }

    public bool Handle(IPacketSender packetSender, IPacket packet) => Handle(packetSender, packet as PickupItemPacket);
}