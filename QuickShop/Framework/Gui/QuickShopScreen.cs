using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Tools;
using xTile.Dimensions;

namespace QuickShop.Framework.Gui;

public class QuickShopScreen : ScreenGui
{
    public QuickShopScreen() : base("Quick Shop")
    {
        var gameLocation = Game1.game1.instanceGameLocation;
        var shops = new List<Shop>
        {
            new("pierreShop", "SeedShop", "Pierre"),
            new("harveyShop", "Hospital", "Harvey"),
            new("gusShop", "Saloon", "Gus"),
            new("robinShop", "Carpenter", "Robin"),
            new("willyShop", "FishShop", "Willy"),
            new("krobusShop", "ShadowShop", "Krobus"),
            new("marnieShop", "AnimalShop", "Marnie"),
            new("travelingCart", "Traveler", "AnyOrNone"),
            new("magicShopBoat", "Festival_NightMarket_DecorationBoat", "AnyOrNone"),
            new("clintShop", "Blacksmith", "Clint"),
            new("jojaMarket", "Joja", "AnyOrNone"),
            new("dwarfShop", "Dwarf", "Dwarf"),
            new("danceOfTheMoonlightJellies", "Festival_DanceOfTheMoonlightJellies_Pierre", "AnyOrNone"),
            new("eggFestival", "Festival_EggFestival_Pierre", "AnyOrNone"),
            new("feastOfTheWinterStar", "Festival_FeastOfTheWinterStar_Pierre", "AnyOrNone"),
            new("festivalOfIceTravelingMerchant", "Festival_FestivalOfIce_TravelingMerchant", "AnyOrNone"),
            new("flowerDance", "Festival_FlowerDance_Pierre", "AnyOrNone"),
            new("luau", "Festival_Luau_Pierre", "AnyOrNone"),
            new("spiritsEve", "Festival_SpiritsEve_Pierre", "AnyOrNone"),
            new("stardewValleyFair", "Festival_StardewValleyFair_StarTokens", "AnyOrNone"),
            new("volcanoDungeonShop", "VolcanoShop", "VolcanoShop"),
            new("marlonShop", "AdventureShop", "Marlon"),
            new("adventureGuildRecovery", "AdventureGuildRecovery", "Marlon"),
            new("hatShop", "HatMouse", "AnyOrNone"),
            new("movieTheaterShop", "BoxOffice", "AnyOrNone"),
            new("casinoShop", "Casino", "AnyOrNone"),
            new("qiShop", "QiGemShop", "AnyOrNone"),
            new("sandyShop", "Sandy", "Sandy"),
            new("desertTrade", "DesertTrade", "AnyOrNone"),
            new("desertFestival", "DesertFestival_EggShop", "AnyOrNone"),
            new("islandTrade", "IslandTrade", "AnyOrNone"),
            new("resortBar", "ResortBar", "Gus"),
            new("iceCreamStand", "IceCreamStand", "AnyOrNone"),
            new("raccoonShop", "Raccoon", "AnyOrNone"),
            new("booksellerShop", "Bookseller", "AnyOrNone"),
            new("booksellerTrade", "BooksellerTrade", "AnyOrNone"),
            new("concessions", "Concessions", "AnyOrNone"),
            new("petAdoption", "PetAdoption", "AnyOrNone")
        };

        foreach (var shop in shops)
        {
            AddElement(new Button(GetButtonTranslation(shop.Title), GetButtonTranslation(shop.Title))
            {
                OnLeftClicked = () => { Utility.TryOpenShopMenu(shop.ShopId, shop.OwnerName); }
            });
        }

        for (var i = 1; i <= 3; i++)
        {
            var decorationBoatShopTitle = $"{GetButtonTranslation("decorationBoatShop")} {i}";
            var finalI = i;
            AddElement(new Button(decorationBoatShopTitle, decorationBoatShopTitle)
            {
                OnLeftClicked = () =>
                {
                    Utility.TryOpenShopMenu($"Festival_NightMarket_MagicBoat_Day{finalI}", "BlueBoat");
                }
            });
        }

        var desertFestivals = DataLoader.Shops(Game1.content)
            .Where(shop =>
                shop.Key.StartsWith("DesertFestival_")
                && shop.Value.Owners.Count == 1
                && Utility.getAllCharacters().Any(npc => npc.Name == shop.Value.Owners[0].Id));

        foreach (var festival in desertFestivals)
        {
            var festivalTitle =
                $"{GetButtonTranslation("desertFestival")}({Utility.getAllCharacters().First(npc => npc.Name == festival.Value.Owners[0].Id).displayName})";
            AddElement(new Button(festivalTitle, festivalTitle)
            {
                OnLeftClicked = () => { Utility.TryOpenShopMenu(festival.Key, festival.Value.Owners[0].Id); }
            });
        }

        var prizeTicketTitle = $"{GetButtonTranslation("prizeTicket")}";
        AddElement(new Button(prizeTicketTitle, prizeTicketTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new PrizeTicketMenu(); }
        });

        var carpenterBuildingTitle = $"{GetButtonTranslation("carpenterBuilding")}";
        AddElement(new Button(carpenterBuildingTitle, carpenterBuildingTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new CarpenterMenu("Robin", Game1.getFarm()); }
        });

        var animalShopTitle = $"{GetButtonTranslation("animalShop")}";
        var animalShopLocation = "";
        var animalShopTileX = 0;
        var animalShopTileY = 0;
        var animalShop = false;
        AddElement(new Button(animalShopTitle, animalShopTitle)
        {
            OnLeftClicked = () =>
            {
                animalShopTileX = (int)Game1.player.Tile.X;
                animalShopTileY = (int)Game1.player.Tile.Y;
                animalShopLocation = Game1.player.currentLocation.Name;
                animalShop = true;
                Game1.activeClickableMenu = new PurchaseAnimalsMenu(Utility.getPurchaseAnimalStock(Game1.getFarm()));
            }
        });

        ModEntry.GetInstance().Helper.Events.GameLoop.UpdateTicked += (sender, args) =>
        {
            if (!animalShop) return;
            if (Game1.activeClickableMenu is PurchaseAnimalsMenu) return;
            if (animalShopLocation != Game1.player.currentLocation.Name)
            {
                Game1.warpFarmer(animalShopLocation, animalShopTileX, animalShopTileY, Game1.player.FacingDirection);
            }

            animalShop = false;
        };

        var renovationTitle = $"{GetButtonTranslation("renovation")}";
        AddElement(new Button(renovationTitle, renovationTitle)
        {
            OnLeftClicked = HouseRenovation.ShowRenovationMenu
        });

        var geodeTitle = $"{GetButtonTranslation("geode")}";
        AddElement(new Button(geodeTitle, geodeTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new GeodeMenu(); }
        });

        var mailboxTitle = $"{GetButtonTranslation("mailbox")}";
        AddElement(new Button(mailboxTitle, mailboxTitle)
        {
            OnLeftClicked = () => { gameLocation.mailbox(); }
        });

        var calendarTitle = $"{GetButtonTranslation("calendar")}";
        AddElement(new Button(calendarTitle, calendarTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new Billboard(); }
        });

        var helpWantedTitle = $"{GetButtonTranslation("helpWanted")}";
        AddElement(new Button(helpWantedTitle, helpWantedTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new Billboard(true); }
        });

        var specialOrdersBoardTitle = $"{GetButtonTranslation("specialOrdersBoard")}";
        AddElement(new Button(specialOrdersBoardTitle, specialOrdersBoardTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new SpecialOrdersBoard(); }
        });

        var qiSpecialOrdersBoardTitle = $"{GetButtonTranslation("qiSpecialOrdersBoard")}";
        AddElement(new Button(qiSpecialOrdersBoardTitle, qiSpecialOrdersBoardTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new SpecialOrdersBoard("Qi"); }
        });

        if (Game1.player.mailReceived.Contains("JojaMember"))
        {
            var joJaCdTitle = $"{GetButtonTranslation("joJaCD")}";
            AddElement(new Button(joJaCdTitle, joJaCdTitle)
            {
                OnLeftClicked = () =>
                {
                    Game1.activeClickableMenu =
                        new JojaCDMenu(Game1.temporaryContent.Load<Texture2D>("LooseSprites\\JojaCDForm"));
                }
            });
        }

        var wizardBuildingTitle = $"{GetButtonTranslation("wizardBuilding")}";
        AddElement(new Button(wizardBuildingTitle, wizardBuildingTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new CarpenterMenu("Wizard", Game1.getFarm()); }
        });

        var changeAppearanceTitle = $"{GetButtonTranslation("changeAppearance")}";
        AddElement(new Button(changeAppearanceTitle, changeAppearanceTitle)
        {
            OnLeftClicked = () =>
            {
                gameLocation.createQuestionDialogue(
                    Game1.content.LoadString("Strings\\Locations:WizardTower_WizardShrine").Replace('\n', '^'),
                    gameLocation.createYesNoResponses(), "WizardShrine");
            }
        });
        var changeProfessionsTitle = $"{GetButtonTranslation("changeProfessions")}";
        AddElement(new Button(changeProfessionsTitle, changeProfessionsTitle)
        {
            OnLeftClicked = () =>
            {
                gameLocation.createQuestionDialogue(Game1.content.LoadString("Strings\\Locations:Sewer_DogStatue"),
                    gameLocation.createYesNoResponses(), "dogStatue");
            }
        });

        if (!Game1.player.mailReceived.Contains("JojaMember"))
        {
            var bundlesTitle = $"{GetButtonTranslation("bundles")}";
            AddElement(new Button(bundlesTitle, bundlesTitle)
            {
                OnLeftClicked = () => { Game1.activeClickableMenu = new Bundle(); }
            });
        }

        var sewingTitle = $"{GetButtonTranslation("sewing")}";
        AddElement(new Button(sewingTitle, sewingTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new TailoringMenu(); }
        });

        var dyeTitle = $"{GetButtonTranslation("dye")}";
        AddElement(new Button(dyeTitle, dyeTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new DyeMenu(); }
        });

        var forgeTitle = $"{GetButtonTranslation("forge")}";
        AddElement(new Button(forgeTitle, forgeTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new ForgeMenu(); }
        });

        var minesTitle = $"{GetButtonTranslation("mines")}";
        AddElement(new Button(minesTitle, minesTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = new MineElevatorMenu(); }
        });

        var shipTitle = $"{GetButtonTranslation("ship")}";
        AddElement(new Button(shipTitle, shipTitle)
        {
            OnLeftClicked = () => { Game1.activeClickableMenu = ShippingBin(); }
        });

        if (Game1.player.toolBeingUpgraded.Value == null && Game1.player.daysLeftForToolUpgrade.Value <= 0)
        {
            var upgradeToolsTitle = $"{GetButtonTranslation("upgrade")}";
            AddElement(new Button(upgradeToolsTitle, upgradeToolsTitle)
            {
                OnLeftClicked = () => { Utility.TryOpenShopMenu("ClintUpgrade", "Clint"); }
            });
        }

        if (Game1.player.toolBeingUpgraded.Value != null && Game1.player.daysLeftForToolUpgrade.Value <= 0)
        {
            var getUpgradedToolTitle = GetButtonTranslation("getUpgradedTool");
            AddElement(new Button(getUpgradedToolTitle,
                getUpgradedToolTitle)
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
                        Game1.DrawDialogue(Game1.getCharacterFromName("Clint"),
                            Game1.content.LoadString("Data\\ExtraDialogue:Clint_NoInventorySpace",
                                Game1.player.toolBeingUpgraded.Value.DisplayName));
                    }
                }
            });
        }

        if (Game1.player.maxItems.Value < 36)
        {
            var backpackUpgradeTitle = GetButtonTranslation("backpackUpgrade");
            AddElement(new Button(backpackUpgradeTitle,
                backpackUpgradeTitle)
            {
                OnLeftClicked = () =>
                {
                    gameLocation.performAction("BuyBackpack", Game1.player, new Location());
                }
            });
        }

        if (Game1.player.daysUntilHouseUpgrade.Value < 0 && !Game1.getFarm().isThereABuildingUnderConstruction())
        {
            if (Game1.player.HouseUpgradeLevel < 3)
            {
                var houseUpgradeTitle = GetButtonTranslation("houseUpgrade");
                AddElement(new Button(houseUpgradeTitle, houseUpgradeTitle)
                {
                    OnLeftClicked = () => { GetMethod(gameLocation, "houseUpgradeOffer").Invoke(); }
                });
            }
            else if ((Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") ||
                      Game1.MasterPlayer.mailReceived.Contains("JojaMember") ||
                      Game1.MasterPlayer.hasCompletedCommunityCenter()) &&
                     new Town().daysUntilCommunityUpgrade.Value <= 0 &&
                     !Game1.MasterPlayer.mailReceived.Contains("pamHouseUpgrade"))
            {
                AddElement(new Button(GetButtonTranslation("houseUpgrade.communityUpgrade"),
                    GetButtonTranslation("houseUpgrade.communityUpgrade.description"))
                {
                    OnLeftClicked = () => { GetMethod(gameLocation, "communityUpgradeOffer").Invoke(); }
                });
            }
        }

        if (Game1.player.isMarriedOrRoommates())
        {
            var divorceTranslation = GetButtonTranslation("divorce");
            AddElement(new Button(divorceTranslation, divorceTranslation)
            {
                OnLeftClicked = () =>
                {
                    gameLocation.createQuestionDialogue(
                        Game1.content.LoadString("Strings\\Locations:ManorHouse_DivorceBook_Question"),
                        gameLocation.createYesNoResponses(), "divorce");
                }
            });
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

    private IReflectedMethod GetMethod(object obj, string name)
    {
        return ModEntry.GetInstance().Helper.Reflection.GetMethod(obj, name);
    }

    private class Bundle : JunimoNoteMenu
    {
        public Bundle(int area = 1) : base(false, area)
        {
        }

        public override void draw(SpriteBatch b)
        {
            base.draw(b);
            areaNextButton.draw(b);
            areaBackButton.draw(b);
            drawMouse(b);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            foreach (var bundle in bundles)
            {
                bundle.depositsAllowed = true;
            }

            base.receiveLeftClick(x, y, playSound);
            if (areaNextButton.containsPoint(x, y))
            {
                SwapPage(1);
            }
            else if (areaBackButton.containsPoint(x, y))
            {
                SwapPage(-1);
            }

            if (areaNextButton.containsPoint(x, y) || areaBackButton.containsPoint(x, y))
            {
                if (Game1.activeClickableMenu is JunimoNoteMenu junimoNoteMenu)
                {
                    Game1.activeClickableMenu = new Bundle(junimoNoteMenu.whichArea);
                }
            }
        }
    }
}