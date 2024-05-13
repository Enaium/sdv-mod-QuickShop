namespace QuickShop.Framework;

public record Shop(string Title, string ShopId, string OwnerName, Shop.ShopType Type)
{
    public enum ShopType
    {
        Normal,
        Festival,
    }
}