using System;
using System.Collections.Generic;
using System.Reflection;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley.Tools;

namespace QuickShop.Framework.Gui
{
    public class QuickShopScreen : ScreenGui
    {
        private List<QuickShopData> _quickShopData;

        public QuickShopScreen()
        {
            _quickShopData = new List<QuickShopData>();
            _quickShopData.Add(new QuickShopData("pierreShop",
                new ShopMenu(new List<ISalable>(Utility.getShopStock(true)), who: "Pierre")));
            _quickShopData.Add(new QuickShopData("harveyShop", new ShopMenu(Utility.getHospitalStock())));
            _quickShopData.Add(new QuickShopData("gusShop", new ShopMenu(Utility.getSaloonStock(), who: "Gus")));
            _quickShopData.Add(new QuickShopData("robinShop", new ShopMenu(Utility.getCarpenterStock(), who: "Robin")));
            _quickShopData.Add(new QuickShopData("carpenter", new CarpenterMenu()));
            _quickShopData.Add(new QuickShopData("willyShop",
                new ShopMenu(Utility.getFishShopStock(Game1.player), who: "Willy")));
            _quickShopData.Add(new QuickShopData("krobusShop",
                new ShopMenu(new Sewer().getShadowShopStock(), who: "Krobus")));
            _quickShopData.Add(new QuickShopData("marnieShop",
                new ShopMenu(Utility.getAnimalShopStock(), who: "Marnie")));
            _quickShopData.Add(new QuickShopData("animalShop",
                new PurchaseAnimalsMenu(Utility.getPurchaseAnimalStock())));
            _quickShopData.Add(new QuickShopData("merchantShop",
                new ShopMenu(
                    Utility.getTravelingMerchantStock((int) ((long) Game1.uniqueIDForThisGame +
                                                             Game1.stats.DaysPlayed)),
                    who: "TravelerNightMarket", on_purchase: Utility.onTravelingMerchantShopPurchase)));
            _quickShopData.Add(new QuickShopData("clintShop",
                new ShopMenu(Utility.getBlacksmithStock(), who: "Clint")));
            _quickShopData.Add(new QuickShopData("upgrade",
                new ShopMenu(Utility.getBlacksmithUpgradeStock(Game1.player))));
            _quickShopData.Add(new QuickShopData("geode", new GeodeMenu()));
            _quickShopData.Add(new QuickShopData("morrisShop", new ShopMenu(Utility.getJojaStock())));
            _quickShopData.Add(new QuickShopData("dwarfShop", new ShopMenu(Utility.getDwarfShopStock(), who: "Dwarf")));
            _quickShopData.Add(new QuickShopData("marlonShop",
                new ShopMenu(Utility.getAdventureShopStock(), who: "Marlon")));
            _quickShopData.Add(new QuickShopData("hatShop", new ShopMenu(Utility.getHatStock())));
            _quickShopData.Add(new QuickShopData("qiShop", new ShopMenu(Utility.getQiShopStock())));
            _quickShopData.Add(new QuickShopData("sandyShop",
                new ShopMenu(new List<ISalable>(Utility.getShopStock(false)), who: "Sandy")));
            _quickShopData.Add(new QuickShopData("desertShop",
                new ShopMenu((Desert.getDesertMerchantTradeStock(Game1.player)), who: "Sandy")));

            if (Game1.player.mailReceived.Contains("JojaMember"))
            {
                _quickShopData.Add(new QuickShopData("joJaCD",
                    new JojaCDMenu(Game1.temporaryContent.Load<Texture2D>("LooseSprites\\JojaCDForm"))));
            }

            _quickShopData.Add(new QuickShopData("wizard", new CarpenterMenu(true)));
            if (!Game1.player.mailReceived.Contains("JojaMember"))
            {
                _quickShopData.Add(new QuickShopData("bundles", new JunimoNoteMenu(true, 1, true)));
            }

            _quickShopData.Add(new QuickShopData("sewing", new TailoringMenu()));
            _quickShopData.Add(new QuickShopData("dye", new DyeMenu()));
            _quickShopData.Add(new QuickShopData("mines", new MineElevatorMenu()));
            _quickShopData.Add(new QuickShopData("ship", ShippingBin()));

            foreach (var variable in _quickShopData)
            {
                AddElement(new Button(
                    GetTranslation($"quickShop.button") + GetTranslation($"quickShop.button.{variable.Name}"),
                    GetTranslation($"quickShop.button") + GetTranslation($"quickShop.button.{variable.Name}"))
                {
                    OnLeftClicked = () => { Game1.activeClickableMenu = variable.Shop; }
                });
            }

            if (Game1.player.toolBeingUpgraded.Value != null && Game1.player.daysLeftForToolUpgrade <= 0)
            {
                AddElement(new Button(GetTranslation("quickShop.button.getUpgradedTool"),
                    GetTranslation("quickShop.button.getUpgradedTool"))
                {
                    OnLeftClicked = () =>
                    {
                        if (Game1.player.freeSpotsInInventory() > 0 ||
                            Game1.player.toolBeingUpgraded.Value is GenericTool)
                        {
                            Tool tool = Game1.player.toolBeingUpgraded.Value;
                            Game1.player.toolBeingUpgraded.Value = null;
                            Game1.player.hasReceivedToolUpgradeMessageYet = false;
                            Game1.player.holdUpItemThenMessage(tool);
                            if (tool is GenericTool)
                            {
                                tool.actionWhenClaimed();
                            }
                            else
                            {
                                Game1.player.addItemToInventoryBool(tool);
                            }

                            Game1.exitActiveMenu();
                        }
                        else
                        {
                            Game1.drawDialogue(Game1.getCharacterFromName("Clint"),
                                Game1.content.LoadString("Data\\ExtraDialogue:Clint_NoInventorySpace",
                                    Game1.player.toolBeingUpgraded.Value.DisplayName));
                        }
                    }
                });
            }

            if (Game1.player.maxItems < 36)
            {
                AddElement(new Button(GetTranslation("quickShop.button.backpackUpgrade"),
                    GetTranslation("quickShop.button.backpackUpgrade"))
                {
                    OnLeftClicked = () =>
                    {
                        if (Game1.player.maxItems == 12 && Game1.player.Money >= 2000)
                        {
                            Game1.player.Money -= 2000;
                            Game1.player.maxItems.Value += 12;
                            for (int index = 0;
                                index < Game1.player.maxItems;
                                ++index)
                            {
                                if (Game1.player.items.Count <= index)
                                    Game1.player.items.Add(null);
                            }

                            Game1.player.holdUpItemThenMessage(new SpecialItem(99,
                                Game1.content.LoadString("Strings\\StringsFromCSFiles:GameLocation.cs.8708")));
                            Game1.exitActiveMenu();
                        }
                        else if (Game1.player.maxItems < 36 && Game1.player.Money >= 10000)
                        {
                            Game1.player.Money -= 10000;
                            Game1.player.maxItems.Value += 12;
                            Game1.player.holdUpItemThenMessage(new SpecialItem(99,
                                Game1.content.LoadString("Strings\\StringsFromCSFiles:GameLocation.cs.8709")));
                            for (int index = 0;
                                index < Game1.player.maxItems;
                                ++index)
                            {
                                if (Game1.player.items.Count <= index)
                                    Game1.player.items.Add(null);
                            }

                            Game1.exitActiveMenu();
                        }
                        else if (Game1.player.maxItems != 36)
                        {
                            Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney2"));
                        }
                    }
                });
            }

            if (Game1.player.HouseUpgradeLevel < 3 && Game1.player.daysUntilHouseUpgrade < 0 &&
                !Game1.getFarm().isThereABuildingUnderConstruction())
            {
                AddElement(new Button(GetTranslation("quickShop.button.houseUpgrade"),
                    GetTranslation("quickShop.button.houseUpgrade"))
                {
                    OnLeftClicked = () =>
                    {
                        switch (Game1.player.houseUpgradeLevel)
                        {
                            case 0:
                                if (Game1.player.Money >= 10000 && Game1.player.hasItemInInventory(388, 450))
                                {
                                    Game1.player.daysUntilHouseUpgrade.Value = 3;
                                    Game1.player.Money -= 10000;
                                    Game1.player.removeItemsFromInventory(388, 450);
                                    Game1.getCharacterFromName("Robin").setNewDialogue(
                                        Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
                                    Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                    break;
                                }

                                if (Game1.player.Money < 10000)
                                {
                                    Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                                    break;
                                }

                                Game1.drawObjectDialogue(
                                    Game1.content.LoadString(
                                        "Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood1"));
                                break;
                            case 1:
                                if (Game1.player.Money >= 50000 && Game1.player.hasItemInInventory(709, 150))
                                {
                                    Game1.player.daysUntilHouseUpgrade.Value = 3;
                                    Game1.player.Money -= 50000;
                                    Game1.player.removeItemsFromInventory(709, 150);
                                    Game1.getCharacterFromName("Robin").setNewDialogue(
                                        Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
                                    Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                    break;
                                }

                                if (Game1.player.Money < 50000)
                                {
                                    Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                                    break;
                                }

                                Game1.drawObjectDialogue(
                                    Game1.content.LoadString(
                                        "Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood2"));
                                break;
                            case 2:
                                if (Game1.player.Money >= 100000)
                                {
                                    Game1.player.daysUntilHouseUpgrade.Value = 3;
                                    Game1.player.Money -= 100000;
                                    Game1.getCharacterFromName("Robin").setNewDialogue(
                                        Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
                                    Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                    break;
                                }

                                if (Game1.player.Money >= 100000)
                                    break;
                                Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                                break;
                        }
                    }
                });
            }
        }

        private string GetTranslation(string key)
        {
            return ModEntry.GetInstance().Helper.Translation.Get(key);
        }

        private ItemGrabMenu ShippingBin()
        {
            MethodInfo method = typeof(Farm).GetMethod("shipItem", BindingFlags.Instance | BindingFlags.NonPublic);
            ItemGrabMenu.behaviorOnItemSelect behaviorOnItemSelectFunction =
                (ItemGrabMenu.behaviorOnItemSelect) Delegate.CreateDelegate(typeof(ItemGrabMenu.behaviorOnItemSelect),
                    Game1.getFarm(), method);
            ItemGrabMenu itemGrabMenu = new ItemGrabMenu(null, true, false,
                Utility.highlightShippableObjects, behaviorOnItemSelectFunction,
                "", null, true, true, false);
            itemGrabMenu.initializeUpperRightCloseButton();
            itemGrabMenu.setBackgroundTransparency(false);
            itemGrabMenu.setDestroyItemOnClick(true);
            itemGrabMenu.initializeShippingBin();
            return itemGrabMenu;
        }
    }

    public class QuickShopData
    {
        public string Name { get; }
        public IClickableMenu Shop { get; }

        public QuickShopData(string name, IClickableMenu shop)
        {
            Name = name;
            Shop = shop;
        }
    }
}