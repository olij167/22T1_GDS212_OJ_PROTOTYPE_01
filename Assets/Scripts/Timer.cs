using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 10f, score = 0f;
    public TextMeshProUGUI timerUI, endGameText, itemsBoughtText, receiptText, endCommentText, endGameCostsText;

    public ShoppingList shoppingList;
    public SelectObjects selectObjects;

    public int itemsBought;

    public Camera mainCam, endCam;

    public GameObject cart, endCartPos, startButton, hand, player; //shoppingListUI, 

    public AudioSource announcementAudio, stressAudio;

    bool receiptPrinted = false, gamePaused = false, announcementComplete;
    void Start()
    {
        endCam.enabled = false;
        announcementAudio.enabled = false;
        stressAudio.enabled = false;
        timerUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        timerUI.text = Mathf.RoundToInt(timer).ToString();


        if (!announcementAudio.isPlaying && timer < 8)
        {
            stressAudio.enabled = true;
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    PauseGame();
        //}

        if (Time.timeScale != 1)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        //else Cursor.lockState = CursorLockMode.Locked;

        if (timer <= 0)
        {
            Time.timeScale = 0;
            selectObjects.enabled = false;
            timerUI.enabled = false;
            //shoppingListUI.gameObject;
            stressAudio.enabled = false;

            
            //    stressAudio.enabled = false;

            cart.transform.parent = endCartPos.transform;
            cart.transform.rotation = default;
            cart.transform.position = endCartPos.transform.position;
            
            endGameText.text = shoppingList.requiredItemsCollectedNum.ToString() + " / " + shoppingList.shoppingListNum.ToString() + " required items";

            if (shoppingList.requiredItemsCollectedNum == shoppingList.shoppingListNum)
            {
                endCommentText.text = "You finished your shopping!";
            }
            else endCommentText.text = "Come back for it tomorrow," + "\n" + "Loser";

            //receiptText.text = "Receipt";

            if (!receiptPrinted)
            {
                foreach (GameObject food in selectObjects.foodInCart)
                {
                    receiptText.text += "\n" + food.name.Replace("(Clone)", "");
                }

                itemsBought = receiptText.text.Split('\n').Length - 1;
                itemsBoughtText.text = itemsBought.ToString() + " items purchased";

                //shoppingListUI.transform.GetChild(0).transform.parent = endCommentText.transform.parent;
                //shoppingListUI.SetActive(false);

                receiptPrinted = true;
            }

            float accuracy = (float)shoppingList.requiredItemsCollectedNum / (float)shoppingList.shoppingListNum;
            float budget = shoppingList.shoppingListCost / selectObjects.foodInCartCost;

            Debug.Log("accuracy = " + accuracy + "\n" + "budget = " + budget);
            Debug.Log("collected items on list " + shoppingList.requiredItemsCollectedNum + "\n" + "shopping list length = " + shoppingList.shoppingListNum);
            score = accuracy * budget * 100; // << score formula.. did I enter it wrong?


            endGameCostsText.text = "Shopping List Cost = $" + shoppingList.shoppingListCost + "\n" + "You Spent = $" + selectObjects.foodInCartCost + "\n" + "Your Shopper score is: " + (int)score + "/100";

            endCam.enabled = true;
            mainCam.enabled = false;

        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        //player.GetComponent<CharacterController>().enabled = true;
        //player.GetComponent<Rigidbody>().freezeRotation = false;
        hand.GetComponent<SelectObjects>().enabled = true;
        announcementAudio.enabled = true;
        timerUI.enabled = true;
        //Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
       

        if (!gamePaused)
        {
            hand.GetComponent<SelectObjects>().enabled = false;

            Time.timeScale = 0f;

            gamePaused = true;
        }
        else
        {
            hand.GetComponent<SelectObjects>().enabled = true;

            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
