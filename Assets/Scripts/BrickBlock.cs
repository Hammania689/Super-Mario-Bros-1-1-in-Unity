using UnityEngine;
using System.Collections;

public class BrickBlock : MonoBehaviour
{

    public bool isBlockFull = true;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] int CoinCount = 2;

    [SerializeField] SoundFxManager sfx;
    [SerializeField] AudioClip coinSfx;

    BoxCollider2D block;
    Bounds hitzone;

    private void Start()
    {
        if (CoinCount <= 0)
            isBlockFull = false;

        block = gameObject.GetComponent<BoxCollider2D>();
        hitzone = block.bounds;
    }

    IEnumerator OnCollisionEnter2D(Collision2D other)
    {
        Vector3 impactPoint;

        impactPoint = other.contacts[0].point;

        bool isBelowBrick = impactPoint.y <= hitzone.min.y;
        
        if(CoinCount != 0 && isBelowBrick)
        {
            Object coin = Instantiate(CoinPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            yield return new WaitForSeconds(.3f);

            Debug.Log("sweet spot \t : " + impactPoint);
            sfx.PlaySoundFX(coinSfx);

            Destroy(coin);
            CoinCount--;
            ScoreManager.score += Coin.CoinValue;
        }
        
        if(CoinCount == 0)
        {
            // Play the famous bump sfx
            // Ensure that the blockFull status is correct
            // If the block is destroyed or not
            isBlockFull = false;

            print("You're breaking me... ");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(new Vector3(0, hitzone.min.y), Vector3.one / 2);
    }

}
