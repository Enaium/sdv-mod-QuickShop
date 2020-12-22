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
using StardewValley.Minigames;
using StardewValley.Objects;
using StardewValley.Tools;

namespace QuickShop.Framework.Gui
{
    public class QuickShopScreen : ScreenGui
    {
        public QuickShopScreen()
        {
            string pierreShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("pierreShop")}";
            AddElement(new Button(pierreShop, pierreShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu =
                        new ShopMenu(new List<ISalable>(Utility.getShopStock(true)), who: "Pierre");
                }
            });

            string harveyShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("harveyShop")}";

            AddElement(new Button(harveyShop, harveyShop)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new ShopMenu(Utility.getHospitalStock()); }
            });
            string gusShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("gusShop")}";
            AddElement(new Button(gusShop, gusShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getSaloonStock(), who: "Gus");
                }
            });

            string robinShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("robinShop")}";
            AddElement(new Button(robinShop, robinShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getCarpenterStock(), who: "Robin");
                }
            });

            string carpenter = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("carpenter")}";
            AddElement(new Button(carpenter, carpenter)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new CarpenterMenu(); }
            });

            string willyShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("willyShop")}";
            AddElement(new Button(willyShop, willyShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getFishShopStock(Game1.player), who: "Willy");
                }
            });

            string krobusShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("krobusShop")}";
            AddElement(new Button(krobusShop, krobusShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(new Sewer().getShadowShopStock(), who: "Krobus");
                }
            });

            string marnieShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("marnieShop")}";
            AddElement(new Button(marnieShop, marnieShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getAnimalShopStock(), who: "Marnie");
                }
            });

            string animalShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("animalShop")}";
            AddElement(new Button(animalShop, animalShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new PurchaseAnimalsMenu(Utility.getPurchaseAnimalStock());
                }
            });

            string merchantShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("merchantShop")}";
            AddElement(new Button(merchantShop, merchantShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(
                        Utility.getTravelingMerchantStock((int) ((long) Game1.uniqueIDForThisGame +
                                                                 Game1.stats.DaysPlayed)), who: "TravelerNightMarket",
                        on_purchase: Utility.onTravelingMerchantShopPurchase);
                }
            });

            string clintShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("clintShop")}";
            AddElement(new Button(clintShop, clintShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu =
                        new ShopMenu(Utility.getBlacksmithStock(), who: "Clint");
                }
            });

            if (Game1.player.toolBeingUpgraded.Value == null &&
                Utility.getBlacksmithUpgradeStock(Game1.player).Values.Count != 0)
            {
                string upgrade = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("upgrade")}";
                AddElement(new Button(upgrade, upgrade)
                {
                    OnLeftClicked = () =>
                    {
                        Game1.activeClickableMenu =
                            new ShopMenu(Utility.getBlacksmithUpgradeStock(Game1.player));
                    }
                });
            }

            string geode = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("geode")}";
            AddElement(new Button(geode, geode)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new GeodeMenu(); }
            });

            string morrisShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("morrisShop")}";
            AddElement(new Button(morrisShop, morrisShop)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new ShopMenu(Utility.getJojaStock()); }
            });

            string dwarfShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("dwarfShop")}";
            AddElement(new Button(dwarfShop, dwarfShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getDwarfShopStock(), who: "Dwarf");
                }
            });

            string marlonShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("marlonShop")}";
            AddElement(new Button(marlonShop, marlonShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getAdventureShopStock(), who: "Marlon");
                }
            });

            string hatShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("hatShop")}";
            AddElement(new Button(hatShop, hatShop)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new ShopMenu(Utility.getHatStock()); }
            });

            string qiShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("qiShop")}";
            AddElement(new Button(qiShop, qiShop)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new ShopMenu(Utility.getQiShopStock()); }
            });

            string sandyShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("sandyShop")}";
            AddElement(new Button(sandyShop, sandyShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu =
                        new ShopMenu(new List<ISalable>(Utility.getShopStock(false)), who: "Sandy");
                }
            });

            string desertShop = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("desertShop")}";
            AddElement(new Button(desertShop, desertShop)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu = new ShopMenu(Desert.getDesertMerchantTradeStock(Game1.player));
                }
            });


            if (Game1.player.mailReceived.Contains("JojaMember"))
            {
                AddElement(new Button(GetTranslation("quickShop.button") + GetButtonTranslation("joJaCD"), "joJaCD")
                {
                    OnLeftClicked = () =>
                    {
                        Game1.activeClickableMenu =
                            new JojaCDMenu(Game1.temporaryContent.Load<Texture2D>("LooseSprites\\JojaCDForm"));
                    }
                });
            }

            string wizard = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("wizard")}";
            AddElement(new Button(wizard, wizard)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new CarpenterMenu(true); }
            });

            if (!Game1.player.mailReceived.Contains("JojaMember"))
            {
                string bundles = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("bundles")}";
                AddElement(new Button(bundles, bundles)
                {
                    OnLeftClicked = () => { Game1.activeClickableMenu = new JunimoNoteMenu(true, 1, true); }
                });
            }

            string sewing = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("sewing")}";
            AddElement(new Button(sewing, sewing)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new TailoringMenu(); }
            });

            string dye = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("dye")}";
            AddElement(new Button(dye, dye)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new DyeMenu(); }
            });

            string mines = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("mines")}";
            AddElement(new Button(mines, mines)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new MineElevatorMenu(); }
            });

            string ship = $"{GetTranslation("quickShop.button")} {GetButtonTranslation("ship")}";
            AddElement(new Button(ship, ship)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = ShippingBin(); }
            });


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

            if (Game1.player.daysUntilHouseUpgrade < 0 &&
                !Game1.getFarm().isThereABuildingUnderConstruction())
            {
                if (Game1.player.HouseUpgradeLevel < 3)
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
                                            Game1.content.LoadString(
                                                "Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
                                        Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                        break;
                                    }

                                    if (Game1.player.Money < 10000)
                                    {
                                        Game1.drawObjectDialogue(
                                            Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
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
                                            Game1.content.LoadString(
                                                "Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
                                        Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                        break;
                                    }

                                    if (Game1.player.Money < 50000)
                                    {
                                        Game1.drawObjectDialogue(
                                            Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
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
                                            Game1.content.LoadString(
                                                "Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"));
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
                else if ((Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") ||
                          Game1.MasterPlayer.mailReceived.Contains("JojaMember") ||
                          Game1.MasterPlayer.hasCompletedCommunityCenter()) &&
                         new Town().daysUntilCommunityUpgrade <= 0 &&
                         !Game1.MasterPlayer.mailReceived.Contains("pamHouseUpgrade"))
                {
                    AddElement(new Button(GetTranslation("quickShop.button.houseUpgrade.communityUpgrade"),
                        GetTranslation("quickShop.button.houseUpgrade.communityUpgrade.description"))
                    {
                        OnLeftClicked = () =>
                        {
                            if (Game1.MasterPlayer.mailReceived.Contains("pamHouseUpgrade"))
                                return;
                            if (Game1.player.Money >= 500000 && Game1.player.hasItemInInventory(388, 950))
                            {
                                Game1.player.Money -= 500000;
                                Game1.player.removeItemsFromInventory(388, 950);
                                Game1.getCharacterFromName("Robin").setNewDialogue(
                                    Game1.content.LoadString("Data\\ExtraDialogue:Robin_PamUpgrade_Accepted"));
                                Game1.drawDialogue(Game1.getCharacterFromName("Robin"));
                                new Town().daysUntilCommunityUpgrade.Value = 3;
                            }
                            else if (Game1.player.Money < 500000)
                                Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                            else
                                Game1.drawObjectDialogue(
                                    Game1.content.LoadString(
                                        "Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood3"));
                        }
                    });
                }
            }
        }

        private string GetButtonTranslation(string key)
        {
            return ModEntry.GetInstance().Helper.Translation.Get("quickShop.button." + key);
        }

        private string GetTranslation(string key)
        {
            return ModEntry.GetInstance().Helper.Translation.Get(key);
        }

        private ItemGrabMenu ShippingBin()
        {
            ItemGrabMenu itemGrabMenu = new ItemGrabMenu(null, true, false,
                Utility.highlightShippableObjects,
                Game1.getFarm().shipItem, "", snapToBottom: true,
                canBeExitedWithKey: true, playRightClickSound: false, context: this);
            itemGrabMenu.initializeUpperRightCloseButton();
            itemGrabMenu.setBackgroundTransparency(false);
            itemGrabMenu.setDestroyItemOnClick(true);
            itemGrabMenu.initializeShippingBin();
            return itemGrabMenu;
        }
    }
}