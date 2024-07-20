using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace NoFishingQuests;

internal class NoFishingQuests : Mod
{
	public const string ShopName = "Terraria/Angler/Shop";
	public const string DecorationShopName = "Terraria/Angler/Decor";
	
	public override void Load()
	{
		On_Player.GetAnglerReward += AddAnglerCoinToQuestReward;
		
		base.Load();
	}

	private static void AddAnglerCoinToQuestReward(On_Player.orig_GetAnglerReward orig, Player self, NPC angler, int questItemType)
	{
		orig(self, angler, questItemType);

		var config = ModContent.GetInstance<Config>();
		if (!config.useCustomCurrency) {
			return;
		}

		int type = ModContent.ItemType<AnglerCoinItem>();
		int stack = Main.rand.Next(config.minAnglerCoins, config.maxAnglerCoins);
		int index = Item.NewItem(new EntitySource_Gift(angler), (int)self.position.X, (int)self.position.Y, self.width, self.height, type, stack, false, 0, true);

		if (Main.netMode == NetmodeID.MultiplayerClient) {
			NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f);
		}
	}
}