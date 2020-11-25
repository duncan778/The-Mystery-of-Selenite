using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : PlayerManager
{
    public GameObject gamePanel;
    public GameObject iconCoin;
    public GameObject iconScafandr;
    public GameObject iconHelmet;
    public GameObject iconFruit;
    public GameObject iconWrench;

    public int Coins { get; set; }
    public bool Scafandr { get; set; }
    public bool Helmet { get; set; }
    public bool Fruit { get; set; }
    public bool Wrench { get; set; }

    private GameObject currentStayOn = null;

    public bool Change(string objectName, int changeAmount)
    {
        bool result = false;
        int iconCount = gamePanel.transform.childCount;
        int summ = 0;
        for (int i = 0; i < iconCount; i++)
        {
            if (gamePanel.transform.GetChild(i).GetComponent<Icon>().iconType == objectName)
            {
                summ++;
            }
        }
        if (summ >= changeAmount)
        {
            summ = 0;
            for (int i = 0; i < iconCount; i++)
            {
                if (gamePanel.transform.GetChild(i).GetComponent<Icon>().iconType == objectName)
                {
                    Destroy(gamePanel.transform.GetChild(i).gameObject);
                    summ++;
                    if (summ == changeAmount)
                    {
                        switch (objectName)
                        {
                            case "Coin":
                            {
                                Coins -= changeAmount;
                            }
                            break;

                            case "Fruit":
                            {
                                Fruit = false;
                            }
                            break;

                            case "Wrench":
                            {
                                Wrench = false;
                            }
                            break;

                            default: break;
                        }
                        result = true;
                        break;
                    }
                }
            }
        }
        return result;
    }

    private void Update()
    {
        if (currentStayOn != null && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (currentStayOn.CompareTag("Coin"))
            {
                Coins++;
                Debug.Log(Coins);
                Destroy(currentStayOn);
                Instantiate(iconCoin, gamePanel.transform);
                
            }
            else if (currentStayOn.CompareTag("Helmet"))
            {
                Helmet = true;
                Destroy(currentStayOn);
                Instantiate(iconHelmet, gamePanel.transform);
                
            }
            else if (currentStayOn.CompareTag("Scafandr"))
            {
                Scafandr = true;
                Destroy(currentStayOn);
                Instantiate(iconScafandr, gamePanel.transform);
                
            }
            else if (currentStayOn.CompareTag("Wrench"))
            {
                Wrench = true;
                Destroy(currentStayOn);
                Instantiate(iconWrench, gamePanel.transform);
                
            }
            else if (currentStayOn.CompareTag("Fruit"))
            {
                Fruit = true;
                Destroy(currentStayOn);
                Instantiate(iconFruit, gamePanel.transform);
                
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        currentStayOn = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        currentStayOn = null;
    }
}
