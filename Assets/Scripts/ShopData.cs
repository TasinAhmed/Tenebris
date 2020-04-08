using System.Collections;
using System.Collections.Generic;

public enum Item
{
    healthPotion,
    armour1,
    speedElixir,
    sharpenClaw,
    None
};

public class ItemQuantity
{
    public Item itemType;
    public int quantity;
}

public class ShopData
{
    private int wallet;
    private List<ItemQuantity> bought;

    public ShopData(int startingSouls)
    {
        wallet = startingSouls;
        bought = new List<ItemQuantity>();
    }

    public int getWallet()
    {
        return wallet;
    }

    public List<ItemQuantity> getBought()
    {
        return bought;
    }

    public int numPurchases()
    {
        int total = 0;

        for (int i = 0; i < bought.Count; i++)
        {
            total += bought[i].quantity;
        }

        return total;
    }

    // buys object - changes wallet and adds item
    public void CompleteTransaction(int cost, string itemName)
    {
        if ( getItem(itemName) == Item.None )
        {
            return;
        }

        wallet -= cost;

        // adds item to bough list
        bool added = false;
        // 
        for (int i = 0; i < bought.Count; i++)
        {
            if (bought[i].itemType == getItem(itemName))
            {
                bought[i].quantity++;
                added = true;
            }
        }
        if (!added)
        {
            ItemQuantity p = new ItemQuantity();
            p.itemType = ShopData.getItem(itemName);
            p.quantity = 1;
            bought.Add(p);
        }
    }

    public static int getCost(string itemType){
        switch (itemType)
        {
            case "healthPotion":
                return 10;
            case "armourLvl1":
                return 30;
            case "speedElixir":
                return 20;
            case "sharpenClaw":
                return 15;
            default:
                return -1;
        }
    }
    public static Item getItem(string itemType){
        switch (itemType)
        {
            case "healthPotion":
                return Item.healthPotion;
            case "armourLvl1":
                return Item.armour1;
            case "speedElixir":
                return Item.speedElixir;
            case "sharpenClaw":
                return Item.sharpenClaw;
            default:
                return Item.None;
        }
    }
    public static bool getOneTime(string itemType){
        switch (itemType)
        {
            case "armourLvl1":
                return true;
            default:
                return false;
        }
    }
}
