using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int level;

    public void LevelSelected() 
    {


        SceneManager.LoadScene(level, LoadSceneMode.Single);
    
    
    }
}
