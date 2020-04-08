using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private enum shopState
    {
        Ready,
        Confirm,
        Insufficient
    }
    public Text wallet_output;
    public Text purchases_output;
    public GameObject confirmPurchaseObject;
    public GameObject confirmInsufficientObject;
    private ShopData shopping;
    private shopState state;
    private int purchaseCost;
    private string purchaseItem;



    public void Buy(string itemType)
    {
        if (state == shopState.Ready)
        {
            purchaseCost = ShopData.getCost(itemType);

            if (purchaseCost <= shopping.getWallet())
            {
                
                purchaseItem = itemType;
                state = shopState.Confirm;
                // run confirmation window - make visible
                confirmPurchaseObject.SetActive(true);
            }
            else
            {
                // insufficient funds
                state = shopState.Insufficient;
                //GameObject.Find("ConfirmPurchase").setActive(true);
                confirmInsufficientObject.SetActive(true);
            }
        }
    }

    public void ConfirmPurchase()
    {
        if (state == shopState.Confirm)
        {
            // complete purchase
            shopping.CompleteTransaction(purchaseCost, purchaseItem);
            
            // if item is a one time buy, disable
            if (ShopData.getOneTime(purchaseItem))
            {
                GameObject obj = GameObject.Find("ShopItem-" + purchaseItem);
                Transform parentTrans = obj.transform;

                Transform childTrans = parentTrans.Find("BuyButton");
                GameObject objChild = childTrans.gameObject;
                Button b = objChild.GetComponent<Button>();
                b.interactable = false;

                childTrans = parentTrans.Find("BoughtIndicator");
                objChild = childTrans.gameObject;
                objChild.SetActive(true);
            }
            
            state = shopState.Ready;
        }
    }

    public void CancelPurchase()
    {
        if (state == shopState.Confirm)
        {
            state = shopState.Ready;
            // hide confirmation window

        }
    }
    
    public void Continue()
    {
        if (state == shopState.Insufficient)
        {
            state = shopState.Ready;
            // hide confirmation window
            
        }
    }

    public void Exit()
    {
        Debug.Log("Exit");
        // return list of purchased items
        // Inventory is placeholder name. shopping.getBought() returns a List of type ItemQuantity, which are defined in ShopData.cs
//        Inventory.addPurchases(shopping.getBought());
//        SceneManager.LoadScene( mainMenu );   // switch scene
    }
    

    // Start is called before the first frame update
    void Start()
    {
        // calls some other function to get the number of souls
        int souls = 100;

        shopping = new ShopData(souls);

        state = shopState.Ready;

        purchaseItem = "None";
        purchaseCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        wallet_output.text = "Souls: " + shopping.getWallet();
        purchases_output.text = "Purchased: " + shopping.numPurchases() + " item(s)";
    }
}
