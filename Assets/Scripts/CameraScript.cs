using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform playerPos;
    public Transform rightCamBoundary;
    public Transform levelEnd;

    Vector3 velocity = Vector3.zero;
    Vector3 destination;

    private void Start()
    {
        destination = Vector3.ClampMagnitude(levelEnd.position, 28.8f);
        destination = new Vector3(destination.x, destination.y, -10f);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerPos.position, rightCamBoundary.position) < 14.5)
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.14f, 8.5f);
    }
}
