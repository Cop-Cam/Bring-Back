using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{

    // Use this for initialization
    public int index;
    [SerializeField] int maxIndex;

    private int defaultIndex;

    void Start()
    {
        defaultIndex = index;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (index < maxIndex)
                {
                    index++;
                }
                else
                {
                    index = defaultIndex;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (index > defaultIndex)
                {
                    index--;
                }
                else
                {
                    index = maxIndex;
                }
            }
        }
    }

}
