using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : GenericSingletonClass<UIManager>
{
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


    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Awake()
    {
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
    }

    public void AddUiObjToList(GameObject obj)
    {
        GameUIList.Add(obj);
    }

    public void AddGameObjectToDictionary(GameObject obj)
    {
        ManagerWithUIObj.Add(obj.name, obj);
        Debug.Log("new ui: " + obj.name);
        Debug.Log("size: " + ManagerWithUIObj.Count);
    }
}
