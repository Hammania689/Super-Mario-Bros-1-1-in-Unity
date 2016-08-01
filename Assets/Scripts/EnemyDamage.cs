using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            switch(PlayerController.PlayerState)
            {
                case 1:
                    PlayerController.PlayerState = 0;
                    break;
                case 2:
                    PlayerController.PlayerState = 1;
                    break;
                default:
                    Destroy(player);
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            print("Oh noooos, enemy messed up!");
            Destroy(this.gameObject);
        }
    }

}
