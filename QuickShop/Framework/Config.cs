using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace QuickShop.Framework;

public class Config
{
    public static Config Default { get; } = new();
    public KeybindList OpenQuickShop { get; set; } = new(SButton.M);
    public bool AllowToolUpgradeAgain { get; set; } = false;
    public bool AllowBuildingAgain { get; set; } = false;
}