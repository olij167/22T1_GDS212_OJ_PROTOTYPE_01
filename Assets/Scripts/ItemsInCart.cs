using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInCart : MonoBehaviour
{
    public ShoppingList shoppingList;
    public void OnTriggerEnter(Collider other)
    {
        shoppingList.foodInCart.Add(other.gameObject);
    }
}
