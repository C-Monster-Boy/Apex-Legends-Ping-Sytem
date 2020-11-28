using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPing : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            DestroyPingObject();
            QuickPing q = GameObject.Find("Player").GetComponent<QuickPing>();
            q.IgnoreZPres();
            q.PlayAudioClip(3);
            print(name);
        }
    }

    public void DestroyPingObject()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("DestroyPing");
        GameObject g = transform.parent.parent.gameObject;
        g.GetComponent<OrientWrtPlayer>().DestroyUI();
        
        Destroy(g, 0.5f);
    }
}
