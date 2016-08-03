using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour
{
    //Be able to destroy prefab and raise player state
    void OnTriggerEnter2D()
    {
        if(PlayerController.PlayerState == 0)
        {
            PlayerController.PlayerState = 1;
        }

        Destroy(this.gameObject);
    }
}
