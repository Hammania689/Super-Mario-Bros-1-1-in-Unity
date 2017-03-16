using UnityEngine;
using System.Collections;

public class PowerUpBlock : MonoBehaviour
{
    public GameObject Mushroom;
    public GameObject FireFlower;
    public GameObject Spawn;

    void OnTriggerEnter2D()
    {
        if(PlayerController.PlayerState == 0)
            {
                Instantiate(Mushroom, Spawn.transform.position, Quaternion.identity);               
            }
        else if(PlayerController.PlayerState >=1)
            {
                Instantiate(FireFlower, Spawn.transform.position, Quaternion.identity);
            }

        Destroy(this);
        print("Everybody gets one");

    }
}
