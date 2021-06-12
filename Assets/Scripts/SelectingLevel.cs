using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingLevel : MonoBehaviour
{

    public GameObject PanelLevels;
    public GameObject PanelMenu;
    public void LevelSelecting() 
    {

        PanelMenu.SetActive(false);
        PanelLevels.SetActive(true);
    
    }

    public void GoBack() 
    {


        PanelMenu.SetActive(true);
        PanelLevels.SetActive(false);
    
    }
}
