using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed = 150f;
    float angleX;


    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        angleX += h * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, angleX, 0);
    }

}
