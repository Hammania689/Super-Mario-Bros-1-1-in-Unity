using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public GameObject ShellPrefab;

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
                    Destroy(col.gameObject);
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //On the instance of trigger collision a shell prefab is instantiated and Koopa Troopa is Destroyed
            Destroy(this.gameObject);
            Object Shell = Instantiate(ShellPrefab, transform.position, transform.rotation);
        }
    }

}
