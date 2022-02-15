using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTarget; // 따라다닐 플레이어

    public Vector3 offset;
    public BoxCollider bound;

    [SerializeField]
    private Vector3 minBound;
    [SerializeField]
    private Vector3 maxBound;

    private Camera theCamera;

    private void Start()
    {
        theCamera = GetComponent<Camera>();
    }
    void Update()
    {
        CameraChasePlayer();
    }

    private void CameraChasePlayer()
    {
        if (playerTarget.position.x >= minBound.x && playerTarget.position.x <= maxBound.x &&
           playerTarget.position.z >= minBound.z && playerTarget.position.z <= maxBound.z)
        {
            transform.position = playerTarget.position + offset;
        }
        else if(playerTarget.position.x >= minBound.x && playerTarget.position.x <= maxBound.x)
        {
            transform.position = new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z )+ offset;
        }
        else if (playerTarget.position.z >= minBound.z && playerTarget.position.z <= maxBound.z)
        {
            transform.position = new Vector3(transform.position.x, playerTarget.position.y, playerTarget.position.z) + offset;
        }

    }
}
