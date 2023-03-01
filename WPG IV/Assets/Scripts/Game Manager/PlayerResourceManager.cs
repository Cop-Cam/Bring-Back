using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Untuk meletak pondasi trading dan economy
public class PlayerResourceManager : MonoBehaviour
{
    public int PlayerMoney { get; private set; }

    public TextMeshPro MoneyText;

    public static PlayerResourceManager instance { get; private set; }

    //instantiate script
    void Awake() 
    {
        if(instance != null)
        {
            Debug.Log("there is another PlayerResourceManager");
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ShowCurrentMoney();
    }

    void ShowCurrentMoney()
    {
        //MoneyText.text = PlayerMoney.ToString();
        Debug.Log("current money: " + PlayerMoney);
    }

    //Digunakan di class lain yang membutuhkan
    public void IncreaseMoney(int NewMoney)
    {
        PlayerMoney += NewMoney;
    }
    public void DecreaseMoney(int NewMoney)
    {
        PlayerMoney -= NewMoney;
    }
}
