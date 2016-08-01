using UnityEngine;
using System.Collections;

public class BrickBlock : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject CoinPrefab;
    public int CoinCount = 2;

    IEnumerator OnTriggerEnter2D()
    {
        if(CoinCount != 0)
        {
            Object coin = Instantiate(CoinPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            yield return new WaitForSeconds(.25f);

            Destroy(coin);
            CoinCount--;
            ScoreManager.score += Coin.CoinValue;
        }
        
        if(CoinCount == 0)
        {
            print("You're breaking me... ");
        }
    }

}
