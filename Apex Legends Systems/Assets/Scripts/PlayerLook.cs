using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLook : MonoBehaviour
{
    [Range(0, 90)]
    public float yRotLimit;
    public float sensitivityX;
    public float sensitivityY;

    private float xSens;
    private float ySens;
    private Transform cam;
    private float rotInputX;
    private float rotInputY;
    private float yRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        float sensFromMenu = PlayerPrefs.GetFloat("Sens");
        sensitivityX = sensFromMenu;
        sensitivityY = sensFromMenu;
        SetSens();

        cam = transform.GetChild(0);
        yRot = cam.localEulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }


        rotInputX = Input.GetAxis("Mouse X");
        rotInputY = Input.GetAxis("Mouse Y");

        

        if(rotInputX != 0)
        {
            float rotAmount = rotInputX * xSens * Time.deltaTime;
            transform.eulerAngles += new Vector3(0f , rotAmount, 0f);
        }   

        if(rotInputY != 0)
        {
            float rotAmount = rotInputY * ySens * Time.deltaTime;
            yRot -= rotAmount;
            yRot = Mathf.Clamp(yRot, -yRotLimit, yRotLimit);
            cam.localRotation = Quaternion.Euler(yRot, 0, 0);
        }
    }

    public void SetSens(float x, float y)
    {
        xSens = x;
        ySens = y;
    }

    public void SetSens()
    {
        xSens = sensitivityX;
        ySens = sensitivityY;
    }
}
