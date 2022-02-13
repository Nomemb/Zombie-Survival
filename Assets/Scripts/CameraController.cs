using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTarget; // 따라다닐 플레이어

    public Vector3 offset;
    public BoxCollider bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;

    private void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfWidth * Screen.width / Screen.height; 
    }
    void Update()
    {
        transform.position = playerTarget.position + offset;

        // 필드 밖은 안 보이게 설정
        //float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        ////float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
        //float clampedZ = Mathf.Clamp(this.transform.position.z, minBound.z + halfHeight, maxBound.z - halfHeight);

        //this.transform.position = new Vector3(clampedX, this.transform.position.y, clampedZ);
        ////this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.y);

    }
}
