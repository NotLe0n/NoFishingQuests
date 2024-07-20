using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.UI;

namespace NoFishingQuests;

public class AnglerCoinItem : ModItem
{
	public override void SetStaticDefaults()
	{
		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;

		Item.rare = -11;
		Item.maxStack = 999;
		Item.value = Item.buyPrice(gold: 1);
	}

	public override void Load()
	{
		On_ItemSlot.PickItemMovementAction += AllowCoinSlotPlacement;
	}

	private static int AllowCoinSlotPlacement(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
	{
		if (context == 1 && checkItem.type == ModContent.ItemType<AnglerCoinItem>()) {
			return 0;
		}

		return orig(inv, context, slot, checkItem);
	}
}