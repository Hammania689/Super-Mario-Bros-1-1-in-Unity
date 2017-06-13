using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public static int CoinValue = 100;

    void OnTriggerEnter2D()
    {
        ScoreManager.score += CoinValue;
        Destroy(gameObject);
    }
}
