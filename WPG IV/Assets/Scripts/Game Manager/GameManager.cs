using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : DontDestroyOnLoadSingletonClass<GameManager>
{
    public void PauseGame(bool shouldGameBePaused)
    {
        if(shouldGameBePaused)
        {
            Time.timeScale = 0;
        }
        else if(!shouldGameBePaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogError("Pause Game error");
        }
    }


}
