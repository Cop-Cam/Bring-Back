using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ensyclopedia : MonoBehaviour
{
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject listEndemic;
    [SerializeField] GameObject listInvasive;
    [SerializeField] GameObject ensyclopedia;

    public void OpenEndemic()
    {
        mainPage.SetActive(false);
        listInvasive.SetActive(false);
        listEndemic.SetActive(true);
    }

    public void OpenInvasive()
    {
        mainPage.SetActive(false);
        listEndemic.SetActive(false);
        listInvasive.SetActive(true);
    }

    public void OpenEns()
    {
        ensyclopedia.SetActive(true);
    }

    public void CloseEns()
    {
        ensyclopedia.SetActive(false);
    }
}
