using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuSelections;
    //0-instructions
    //1-key bindings
    //2-Settings
    //3-About

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float sens = PlayerPrefs.GetFloat("Sens", 500f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    public void LoaGivenScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMenuSelectionActive(int index)
    {
        menuSelections[index].SetActive(true);
        if(index == 2)
        {
            menuSelections[index].transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<SliderDisplay>().Activate();
        }
    }


    public void BackToMainMenu()
    {
        foreach(GameObject g in menuSelections)
        {
            g.SetActive(false);
        }
    }
}
