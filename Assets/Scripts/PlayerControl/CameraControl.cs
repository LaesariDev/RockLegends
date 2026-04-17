using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    Transform kamera;

    Vector3 cameraOffset = new Vector3(0, 1.0f, 0);
    Quaternion rotation = new Quaternion(0, 0, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kamera.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = kamera.position + cameraOffset;

       
        
    }
}
