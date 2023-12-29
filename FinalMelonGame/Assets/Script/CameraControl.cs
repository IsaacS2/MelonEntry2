using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float smoothSpeed = 0.05f;
    private Vector3 startPosition;
    private Vector3 velocity = Vector3.zero;
    public GameObject playerTracker;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position - playerTracker.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, startPosition + playerTracker.transform.position, ref velocity, smoothSpeed, 100);
        //transform.position = startPosition + playerTracker.transform.position;
        transform.position = startPosition + playerTracker.GetComponent<Rigidbody>().transform.position;
    }
}
