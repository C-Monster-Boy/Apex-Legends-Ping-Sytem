using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    private GameObject selectedMesh;
    
    public bool isSelected;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            anim = GetComponent<Animator>();
        }
        catch
        {
            //nothing
        }
        
        isSelected = false;
        selectedMesh = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim)
        {
            anim.SetBool("ObjectSelected", isSelected);
        }
        
    }

    public void SelectToggle()
    {
        isSelected = !isSelected;
        if(isSelected)
        {
            selectedMesh.SetActive(true);
        }
        else
        {
            selectedMesh.SetActive(false);
        }
    }
}
