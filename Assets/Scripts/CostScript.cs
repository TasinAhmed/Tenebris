using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostScript : MonoBehaviour
{
    private int cost;
    public Text component;
    // Start is called before the first frame update
    void Start()
    {
        cost = ShopData.getCost(component.text);
        component.text = "Cost: " + cost + " souls";
    }
}
