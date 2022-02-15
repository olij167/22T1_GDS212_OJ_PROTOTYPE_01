using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour
{
    public List<GameObject> foodInStore, shoppingList, foodInCart, checkedFoodInCart;

    public int shoppingListNum, requiredItemsCollectedNum;

    public List<TextMeshProUGUI> shoppingListText;

    public List<Image> shoppingListIcons;

    public float shoppingListCost;

    public SelectObjects selectObjects;

    //public bool itemsRetrieved;

    //public bool retrieveItem1, retrieveItem2, retrieveItem3;

    //public List<Image> shoppingListCheckBox;

    void Start()
    {
        shoppingList = new List<GameObject>();
        //checkedFoodInCart = selectObjects.foodInCart;
        //retrieveItem1 = false;
        //retrieveItem2 = false;
        //retrieveItem3 = false;
    }

    private void Update()
    {
        //if (selectObjects.selectedObject != null)
        //{
        //    foodInCart.Add(selectObjects.selectedObject);
        //}
        //if (foodInCart != null)
        //{
        //    checkedFoodInCart = foodInCart;
        //}

        foodInCart = selectObjects.foodInCart;


        //Debug.Log("Checking cart");
        //CheckItemsInCart();
        

        //CheckItemsInCart();

        //foreach (TextMeshProUGUI listItem in  shoppingListText)
        //{
        //    //if (listItem.fontStyle == FontStyles.Strikethrough)
        //    //{

        //    //}
        //}

        ////if (!retrieveItem1 && !retrieveItem2 && !retrieveItem3)
        ////{
        ////    requiredItemsCollectedNum = 1;
        ////}
        ////else if (retrieveItem1 && !retrieveItem2 && !retrieveItem3)
        ////{
        ////    requiredItemsCollectedNum = 2;
        ////}
        ////else if (retrieveItem1 && retrieveItem2 && !retrieveItem3)
        ////{
        ////    requiredItemsCollectedNum = 3;
        ////}
    }

    public void FillShoppingList()
    {
        foreach (GameObject food in GameObject.FindGameObjectsWithTag("Food"))
        {
            foodInStore.Add(food);
        }

        for (int i = 0; i < shoppingListNum; i++)
        {
            int randFood = Random.Range(0, foodInStore.Count);
            if (!shoppingList.Contains(foodInStore[randFood]))
            {
                shoppingList.Add(foodInStore[randFood]);

                //foodInStore[randFood].GetComponent<MeshRenderer>().material.color = Color.green;
                //if (foodInStore[randFood].transform.childCount > 0)
                //{
                //    foreach (GameObject child in foodInStore[randFood].transform)
                //    {
                //        child.GetComponent<MeshRenderer>().material.color = Color.green;
                //    }
                //}

                //string listItem = foodInStore[randFood].ToString();

                string listItem = foodInStore[randFood].name.Replace("(Clone)", "");
                //listItem.ToUpper();

                shoppingListIcons[i].sprite = foodInStore[randFood].GetComponent<FoodStats>().foodSprite;
                shoppingListText[i].text = listItem;
            }
            shoppingListCost += shoppingList[i].GetComponent<FoodStats>().price;
        }

        
    }


    public void CheckItemsInCart()
    {
        for (int i = 0; i < shoppingListText.Count; i++)
        {
            if (shoppingListText[i].fontStyle != FontStyles.Strikethrough)
            {
                foreach (GameObject food in foodInCart)
                {

                    if (shoppingList[i].name.Contains(food.name))
                    {
                        shoppingListText[i].fontStyle = FontStyles.Strikethrough;

                        requiredItemsCollectedNum += 1;

                        Debug.Log("Item retrieved");

                    }

                }
                //return;
            }

            if (i == shoppingListText.Count - 1) //the cart items will be marked as checked after they have been compared to all the shopping list items
            {
                checkedFoodInCart.Clear();

                checkedFoodInCart = foodInCart;
            }
        }

        if (requiredItemsCollectedNum == shoppingListNum) //items retrieved is set to true if all items have been tick off on the list
        {

                Debug.Log("all items retrieved");
                //itemsRetrieved = true;

        }
        
    }

    //public void CheckItemsInCart()
    //{
    //    for (int i = 0; i < shoppingListText.Count; i++)
    //    {
    //        if (shoppingListText[i].fontStyle != FontStyles.Strikethrough)
    //        {
    //            foreach (GameObject food in foodInCart)
    //            {

    //                if (shoppingList[i].name.Contains(food.name))
    //                {
    //                    shoppingListText[i].fontStyle = FontStyles.Strikethrough;

    //                    requiredItemsCollectedNum += 1;

    //                    Debug.Log("Item retrieved");

    //                    checkedFoodInCart.Clear();

    //                    checkedFoodInCart = foodInCart;


    //                }

    //            }
    //            return;
    //        }
    //        else // << this is whats breaking the required items
    //        {
               
    //            Debug.Log("all items retrieved");
    //            itemsRetrieved = true;
                
    //        }
    //    }
    //}
}
