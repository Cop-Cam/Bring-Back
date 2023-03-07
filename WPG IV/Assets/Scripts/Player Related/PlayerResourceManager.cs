using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerResourceManager : GenericSingletonClass<PlayerResourceManager>
{
    //public static PlayerResourceManager instance { get; private set; }
    
    //resource money
    public int PlayerMoney { get; private set; }

    //resource energy
    public float PlayerEnergy { get; private set; }

    //instantiate script
    // void Awake() 
    // {
    //     if(instance != null)
    //     {
    //         Debug.Log("there is another PlayerResourceManager");
    //     }
    //     instance = this;
    // }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMoney = 0;
        PlayerEnergy = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //ShowCurrentMoney();
    }

    //Meng-update tampilan uang
    void ShowCurrentMoney() 
    {
        //MoneyText.text = PlayerMoney.ToString();
        Debug.Log("current money: " + PlayerMoney);
    }

    //Digunakan di class lain yang membutuhkan
    public void IncreaseMoney(int profit)
    {
        PlayerMoney += profit;
    }
    public void DecreaseMoney(int profit)
    {
        PlayerMoney -= profit;
    }

    public void IncreaseEnergy(float EnergyChanges)
    {
        PlayerEnergy += EnergyChanges;
    }
    public void DecreaseEnergy(float EnergyChanges)
    {
        PlayerEnergy -= EnergyChanges;
    }
}
