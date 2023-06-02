using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : GenericSingletonClass<UIManager>
{
<<<<<<< Updated upstream
    [Tooltip("Masukkan GameObject parent untuk UI Shop")]
    //mungkin bisa satu2 didefinisikan tapi gak nyaman ama sekali aowkowkw
    //public GameObject ShopUI;



    //mungkin bisa pake list, tapi gtw caramu makenya
    private static List<GameObject> GameUIList;


    //atau mungkin bisa pake dict, tapi gtw caramu makenya
    private static Dictionary<string, GameObject> ManagerWithUIObj;


    private bool disableInput = false;
    private bool _pauseMenuOn = false;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject phone = null;
    [SerializeField] private GameObject roundLake = null;
    public bool PauseMenuOn { get => _pauseMenuOn; set => _pauseMenuOn = value; }

    [SerializeField] private GameObject pauseMenuBG = null;
    [SerializeField] private GameObject taskMenu = null;
    [SerializeField] private GameObject ensMenu = null;
    [SerializeField] private GameObject setMenu = null;


=======
    private Dictionary<Type, GameObject> menuDictionary = new Dictionary<Type, GameObject>();

    private Type currentOpenedMenu;
    
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        base.Awake();

        ManagerWithUIObj = new Dictionary<string, GameObject>();

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        PauseMenu();
    }

    private void PauseMenu()
    {
        // Toggle pause menu if escape is pressed

        if (Input.GetKeyDown(KeyCode.Escape) && !disableInput)
        {

            if (PauseMenuOn)
            {
                DisablePauseMenu();
            }
            else
            {
                EnablePauseMenu();
            }

        }
    }

    private void EnablePauseMenu()
    {
        PauseMenuOn = true;
        PlayerController.Instance.PlayerInputIsDisabled = true;
        pauseMenuBG.SetActive(true);
        setMenu.SetActive(false);
        ensMenu.SetActive(false);
        taskMenu.SetActive(false);
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        roundLake.SetActive(false);
        phone.SetActive(false);

        // Trigger garbage collector
        System.GC.Collect();
    }

    public void DisablePauseMenu()
    {
        PauseMenuOn = false;
        PlayerController.Instance.PlayerInputIsDisabled = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        roundLake.SetActive(true);
        phone.SetActive(true);
    }

    public void Close()
    {
        DisablePauseMenu();
    }

    public void Task()
    {
        pauseMenuBG.SetActive(false);
        taskMenu.SetActive(true);
    }

    public void encyclopedia()
    {
        pauseMenuBG.SetActive(false);
        ensMenu.SetActive(true);
    }

    public void Setting()
    {
        pauseMenuBG.SetActive(false);
        setMenu.SetActive(true);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
=======
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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        ManagerWithUIObj.Add(obj.name, obj);
        Debug.Log("new ui: " + obj.name);
        Debug.Log("size: " + ManagerWithUIObj.Count);
=======
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
>>>>>>> Stashed changes
    }

}
