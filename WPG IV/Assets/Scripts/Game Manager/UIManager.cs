using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : GenericSingletonClass<UIManager>
{
    private Dictionary<Type, GameObject> menuDictionary = new Dictionary<Type, GameObject>();

    private Type currentOpenedMenu;
    
    // Start is called before the first frame update
    private void Start()
    {
        foreach(GameObject menuUI in menuDictionary.Values)
        {
            menuUI.SetActive(false);
        }
    }

    public void RegisterMenu<T>(T instance, GameObject menuPrefab) where T : MonoBehaviour
    {
        Type instanceType = instance.GetType();
        if (!menuDictionary.ContainsKey(instanceType))
        {
            menuDictionary.Add(instanceType, menuPrefab);
            // Debug.Log("registered menu type: "+instanceType);
            // Debug.Log("registered menu prefab: "+menuPrefab.name);
        }
        else
        {
            Debug.LogWarning("Menu type already registered: " + instanceType);
        }
    }

    //public void OpenMenu<T>(T instance) where T : MonoBehaviour
    public void OpenMenu(IMenuHandler instance)
    {
        Type instanceType = instance.GetType();
        
        if(currentOpenedMenu == instanceType )
        {
            Debug.Log("menu is opened, closing it down");
            CloseMenu(instance);
            return;
        }

        if (menuDictionary.TryGetValue(instanceType, out GameObject menuPrefab))
        {
            Debug.Log("opening menu '"+instanceType+"'");

            GameManager.Instance.PauseGame(true);
            
            instance.OpeningMenu();
            currentOpenedMenu = instanceType;

            menuPrefab.SetActive(true);

            // Iterate through the menuDictionary to set other menu prefabs inactive
            foreach (var kvp in menuDictionary)
            {
                if (kvp.Key != instanceType)
                {
                    GameObject otherMenuPrefab = kvp.Value;
                    otherMenuPrefab.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Menu type not registered: " + instanceType);
        }
    }

    public void CloseMenu(IMenuHandler instance)
    {
        Type instanceType = instance.GetType();

        if (menuDictionary.TryGetValue(instanceType, out GameObject menuPrefab))
        {
            Debug.Log("closing menu '"+instanceType+"'");

            GameManager.Instance.PauseGame(false);
            
            instance.ClosingMenu();
            currentOpenedMenu = null;

            menuPrefab.SetActive(false);

            // Iterate through the menuDictionary to set other menu prefabs inactive
            foreach (var kvp in menuDictionary)
            {
                if (kvp.Key != instanceType)
                {
                    GameObject otherMenuPrefab = kvp.Value;
                    otherMenuPrefab.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Menu type not registered: " + instanceType);
        }
    }

}
