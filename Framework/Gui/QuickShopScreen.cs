using System;
using System.Collections.Generic;
using System.Reflection;
using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Objects;
using Object = StardewValley.Object;

namespace QuickShop.Framework.Gui
{
    public class QuickShopScreen : ScreenGui
    {
        private List<WalkShopData> _walkShopDatas;

        public QuickShopScreen()
        {
            _walkShopDatas = new List<WalkShopData>();
            _walkShopDatas.Add(new WalkShopData("pierre", new ShopMenu(getShopStock(true))));
            _walkShopDatas.Add(new WalkShopData("willy", new ShopMenu(Utility.getFishShopStock(Game1.player))));
            _walkShopDatas.Add(new WalkShopData("adventure", new ShopMenu(Utility.getAdventureShopStock())));
            _walkShopDatas.Add(new WalkShopData("blacksmith", new ShopMenu(Utility.getBlacksmithStock())));
            _walkShopDatas.Add(new WalkShopData("saloon", new ShopMenu(Utility.getSaloonStock())));
            _walkShopDatas.Add(new WalkShopData("hospital", new ShopMenu(Utility.getHospitalStock())));
            _walkShopDatas.Add(new WalkShopData("krobus", new ShopMenu(getKrobusStock())));
            _walkShopDatas.Add(new WalkShopData("animal", new ShopMenu(Utility.getAnimalShopStock())));
            _walkShopDatas.Add(new WalkShopData("merchant",
                new ShopMenu(
                    Utility.getTravelingMerchantStock((int) (Game1.uniqueIDForThisGame + Game1.stats.DaysPlayed)))));

            _walkShopDatas.Add(new WalkShopData("qi", new ShopMenu(Utility.getQiShopStock())));
            _walkShopDatas.Add(new WalkShopData("animal", new PurchaseAnimalsMenu(Utility.getPurchaseAnimalStock())));
            _walkShopDatas.Add(new WalkShopData("carpenter", new CarpenterMenu()));
            _walkShopDatas.Add(new WalkShopData("wizard", new CarpenterMenu(true)));
            _walkShopDatas.Add(new WalkShopData("bundles", new JunimoNoteMenu(true, 1, true)));
            _walkShopDatas.Add(new WalkShopData("geode", new GeodeMenu()));
            _walkShopDatas.Add(new WalkShopData("upgrade",
                new ShopMenu(Utility.getBlacksmithUpgradeStock(Game1.player))));
            _walkShopDatas.Add(new WalkShopData("sewing", new TailoringMenu()));
            _walkShopDatas.Add(new WalkShopData("dye", new DyeMenu()));
            _walkShopDatas.Add(new WalkShopData("mines", new MineElevatorMenu()));
            _walkShopDatas.Add(new WalkShopData("ship", ShippingBin()));

            foreach (var variable in _walkShopDatas)
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

        private List<ISalable> getShopStock(bool pierres)
        {
            List<ISalable> objList1 = new List<ISalable>();
            if (pierres)
            {
                if (Game1.currentSeason.Equals("spring"))
                {
                    objList1.Add(new Object(Vector2.Zero, 472, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 473, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 474, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 475, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 427, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 429, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 477, int.MaxValue));
                    objList1.Add(new Object(628, int.MaxValue, false, 1700));
                    objList1.Add(new Object(629, int.MaxValue, false, 1000));
                    if (Game1.year > 1)
                        objList1.Add(new Object(Vector2.Zero, 476, int.MaxValue));
                }

                if (Game1.currentSeason.Equals("summer"))
                {
                    objList1.Add(new Object(Vector2.Zero, 480, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 482, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 483, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 484, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 479, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 302, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 453, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 455, int.MaxValue));
                    objList1.Add(new Object(630, int.MaxValue, false, 2000));
                    objList1.Add(new Object(631, int.MaxValue, false, 3000));
                    if (Game1.year > 1)
                        objList1.Add(new Object(Vector2.Zero, 485, int.MaxValue));
                }

                if (Game1.currentSeason.Equals("fall"))
                {
                    objList1.Add(new Object(Vector2.Zero, 487, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 488, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 490, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 299, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 301, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 492, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 491, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 493, int.MaxValue));
                    objList1.Add(new Object(431, int.MaxValue, false, 100));
                    objList1.Add(new Object(Vector2.Zero, 425, int.MaxValue));
                    objList1.Add(new Object(632, int.MaxValue, false, 3000));
                    objList1.Add(new Object(633, int.MaxValue, false, 2000));
                    if (Game1.year > 1)
                        objList1.Add(new Object(Vector2.Zero, 489, int.MaxValue));
                }

                objList1.Add(new Object(Vector2.Zero, 297, int.MaxValue));
                objList1.Add(new Object(Vector2.Zero, 245, int.MaxValue));
                objList1.Add(new Object(Vector2.Zero, 246, int.MaxValue));
                objList1.Add(new Object(Vector2.Zero, 423, int.MaxValue));
                Random random = new Random((int) Game1.stats.DaysPlayed + (int) Game1.uniqueIDForThisGame / 2);
                List<ISalable> objList2 = objList1;
                Wallpaper wallpaper1 = new Wallpaper(random.Next(112));
                wallpaper1.Stack = int.MaxValue;
                objList2.Add(wallpaper1);
                List<ISalable> objList3 = objList1;
                Wallpaper wallpaper2 = new Wallpaper(random.Next(40), true);
                wallpaper2.Stack = int.MaxValue;
                objList3.Add(wallpaper2);
                List<ISalable> objList4 = objList1;
                Clothing clothing = new Clothing(1000 + random.Next(128));
                clothing.Stack = int.MaxValue;
                clothing.Price = 1000;
                objList4.Add(clothing);
                if (Game1.player.achievements.Contains(38))
                    objList1.Add(new Object(Vector2.Zero, 458, int.MaxValue));
            }
            else
            {
                if (Game1.currentSeason.Equals("spring"))
                    objList1.Add(new Object(Vector2.Zero, 478, int.MaxValue));
                if (Game1.currentSeason.Equals("summer"))
                {
                    objList1.Add(new Object(Vector2.Zero, 486, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 481, int.MaxValue));
                }

                if (Game1.currentSeason.Equals("fall"))
                {
                    objList1.Add(new Object(Vector2.Zero, 493, int.MaxValue));
                    objList1.Add(new Object(Vector2.Zero, 494, int.MaxValue));
                }

                objList1.Add(new Object(Vector2.Zero, 88, int.MaxValue));
                objList1.Add(new Object(Vector2.Zero, 90, int.MaxValue));
            }

            return objList1;
        }

        public List<ISalable> getKrobusStock()
        {
            return new List<ISalable>
            {
                new Object(305, int.MaxValue, false, 2500),
                new Object(434, 1, false, 10000)
            };
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
            int num = 0;
            itemGrabMenu.setBackgroundTransparency(num != 0);
            int num2 = 1;
            itemGrabMenu.setDestroyItemOnClick(num2 != 0);
            itemGrabMenu.initializeShippingBin();
            return itemGrabMenu;
        }
    }

    public class WalkShopData
    {
        public string Name { get; }
        public IClickableMenu Shop { get; }

        public WalkShopData(string name, IClickableMenu shop)
        {
            Name = name;
            Shop = shop;
        }
    }
}