using UnityEngine;
using System.Collections;

public class ScreenBounds : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Bird>())
        {
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.GetComponent<Brick>() || col.gameObject.GetComponent<Pig>())
            Destroy(col.gameObject);
    }
}
