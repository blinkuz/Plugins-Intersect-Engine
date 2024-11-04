using Intersect;
using Intersect.Plugins;
using Intersect.Server.Entities;
using Intersect.Server.Framework.Items;
using Intersect.Server.Networking;
using Intersect.Server.Plugins;
using Rare_Item_Drop_Notifier.Configuration;
using Rare_Item_Drop_Notifier.Logging;

namespace Rare_Item_Drop_Notifier
{
    public class PluginEntry : ServerPluginEntry
    {
        public override void OnBootstrap(IPluginBootstrapContext context)
        {
            base.OnBootstrap(context);
            PluginSettings.Settings = context.GetTypedConfiguration<PluginSettings>();
            Logger.Context = context;
        }

        public override void OnStart(IServerPluginContext context)
        {
            Logger.Write(LogLevel.Info, "Rare Item Drop Notifier loaded.");
            context.MapHelper.ItemAdded += OnItemDrop;
        }

        private void OnItemDrop(IItemSource? source, IItem item)
        {
            if (PluginSettings.Settings.RarityNotifications.Contains(Options.Instance.Items.RarityTiers[item.Descriptor.Rarity]))
            {
                if (source is EntityItemSource { EntityReference: { } } entitySource &&
                    entitySource.EntityReference.TryGetTarget(out var entity) && entity is Entity entityInstance)
                {
                    var mapName = entityInstance.MapName;
                    var itemName = item.ItemName;
                    var rarityName = Options.Instance.Items.RarityTiers[item.Descriptor.Rarity];
                    var dropChance = item.DropChance;

                    var messageTemplate = PluginSettings.Settings.TextMessage;
                    var message = messageTemplate
                        .Replace("{item}", itemName)
                        .Replace("{rarity}", rarityName)
                        .Replace("{dropChance}", dropChance.ToString())
                        .Replace("{entity}", entity.Name ?? "Unknown")
                        .Replace("{map}", mapName);

                    var color = Color.FromString(PluginSettings.Settings.NotificationColor, Color.Green);
                    PacketSender.SendGlobalMsg(message, color, string.Empty);
                }
            }
        }

        public override void OnStop(IServerPluginContext context)
        {
        }
    }
}