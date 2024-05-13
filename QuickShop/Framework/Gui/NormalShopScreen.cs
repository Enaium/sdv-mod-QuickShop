using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using StardewValley;

namespace QuickShop.Framework.Gui;

public class NormalShopScreen : ScreenGui
{
    public NormalShopScreen(List<Shop> shops) : base(ModEntry.GetInstance().Helper.Translation
        .Get("quickShop.screen.normalShop.title"))
    {
        foreach (var shop in shops)
        {
            AddElement(new Button(ModEntry.GetInstance().GetButtonTranslation(shop.Title))
            {
                OnLeftClicked = () => { Utility.TryOpenShopMenu(shop.ShopId, shop.OwnerName); }
            });
        }
    }
}