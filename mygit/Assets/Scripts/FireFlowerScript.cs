using UnityEngine;
using System.Collections;

public class FireFlowerScript : MonoBehaviour
{
    //Be able to destroy prefab and raise player state
    void OnTriggerEnter2D()
    {
        PlayerController.PlayerState = 2;
        Destroy(this.gameObject);
    }

}
