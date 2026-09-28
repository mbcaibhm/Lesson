using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float speed = 150f;
    float angleX, angleY;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(h, v, 0);
        //transform.Rotate(dir * speed * Time.deltaTime);
        //Vector3 dir = new Vector3(-v, h, 0);
        //transform.Rotate(dir * speed * Time.deltaTime);

        angleX += h * speed * Time.deltaTime;
        angleY += v * speed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60, 60);
        transform.eulerAngles = new Vector3(-angleY, angleX, 0);

    }
}
