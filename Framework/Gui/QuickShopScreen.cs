using System;
using System.Collections.Generic;
using System.Reflection;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Objects;
using Object = StardewValley.Object;

namespace QuickShop.Framework.Gui
{
    public class QuickShopScreen : ScreenGui
    {
        private List<QuickShopData> _quickShopData;

        public QuickShopScreen()
        {
            _quickShopData = new List<QuickShopData>();
            _quickShopData.Add(new QuickShopData("pierreShop",
                new ShopMenu(new List<ISalable>(Utility.getShopStock(true)))));
            _quickShopData.Add(new QuickShopData("harveyShop", new ShopMenu(Utility.getHospitalStock())));
            _quickShopData.Add(new QuickShopData("gusShop", new ShopMenu(Utility.getSaloonStock())));
            _quickShopData.Add(new QuickShopData("robinShop", new ShopMenu(Utility.getCarpenterStock())));
            _quickShopData.Add(new QuickShopData("willyShop", new ShopMenu(Utility.getFishShopStock(Game1.player))));
            _quickShopData.Add(new QuickShopData("krobusShop", new ShopMenu(new Sewer().getShadowShopStock())));
            _quickShopData.Add(new QuickShopData("marnieShop", new ShopMenu(Utility.getAnimalShopStock())));
            _quickShopData.Add(new QuickShopData("animalShop",
                new PurchaseAnimalsMenu(Utility.getPurchaseAnimalStock())));
            _quickShopData.Add(new QuickShopData("clintShop", new ShopMenu(Utility.getBlacksmithStock())));
            _quickShopData.Add(new QuickShopData("morrisShop", new ShopMenu(Utility.getJojaStock())));
            _quickShopData.Add(new QuickShopData("dwarfShop", new ShopMenu(Utility.getDwarfShopStock())));
            _quickShopData.Add(new QuickShopData("marlonShop", new ShopMenu(Utility.getAdventureShopStock())));
            _quickShopData.Add(new QuickShopData("hatShop", new ShopMenu(Utility.getHatStock())));
            _quickShopData.Add(new QuickShopData("qiShop", new ShopMenu(Utility.getQiShopStock())));
            _quickShopData.Add(new QuickShopData("sandyShop",
                new ShopMenu(new List<ISalable>(Utility.getShopStock(false)))));
            if (Game1.player.mailReceived.Contains("JojaMember"))
            {
                _quickShopData.Add(new QuickShopData("joJaCD",
                    new JojaCDMenu(Game1.temporaryContent.Load<Texture2D>("LooseSprites\\JojaCDForm"))));
            }

            _quickShopData.Add(new QuickShopData("carpenter", new CarpenterMenu()));
            _quickShopData.Add(new QuickShopData("wizard", new CarpenterMenu(true)));
            if (!Game1.player.mailReceived.Contains("JojaMember"))
            {
                _quickShopData.Add(new QuickShopData("bundles", new JunimoNoteMenu(true, 1, true)));
            }

            _quickShopData.Add(new QuickShopData("geode", new GeodeMenu()));
            _quickShopData.Add(new QuickShopData("upgrade",
                new ShopMenu(Utility.getBlacksmithUpgradeStock(Game1.player))));
            _quickShopData.Add(new QuickShopData("sewing", new TailoringMenu()));
            _quickShopData.Add(new QuickShopData("dye", new DyeMenu()));
            _quickShopData.Add(new QuickShopData("mines", new MineElevatorMenu()));
            _quickShopData.Add(new QuickShopData("ship", ShippingBin()));

            foreach (var variable in _quickShopData)
            {
                AddElement(new Button(Get($"quickShop.button") + Get($"quickShop.button.{variable.Name}"),
                    Get($"quickShop.button") + Get($"quickShop.button.{variable.Name}"))
                {
                    OnLeftClicked = () => { Game1.activeClickableMenu = variable.Shop; }
                });
            }
        }

        private string Get(string key)
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