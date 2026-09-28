using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //float speed = 5f;
    //float force = 5f;
    //private Rigidbody rb;

    public float speed = 5.0f;
    CharacterController cc;

    public float gravity = -20f;
    float velocityY;
    float jumpPower = 10f;
    int jumpCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        //if (Input.GetButtonDown("Jump"))
        //{
        //  Jump();
        //}



        //if(cc.collisionFlags == CollisionFlags.Below)
        //{

        //}


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        //dir.Normalize();
        //transform.Translate(dir * speed * Time.deltaTime);
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.Translate(dir * speed * Time.deltaTime);
        //cc.Move(dir * speed * Time.deltaTime);
        //velocityY += gravity * Time.deltaTime;
        //dir.y = velocityY;


        //if (cc.isGrounded)
        //{
        //    velocityY = 0;
        //}
        //if (Input.GetButtonDown("Jump"))
        //{
        //    velocityY = jumpPower;
        //}

        if (cc.isGrounded)
        {
            velocityY = 0;
            jumpCount = 0;
        }
        //if (cc.collisionFlags == CollisionFlags.Below)
        //{
        //    velocityY = 0;
        //    jumpCount = 0;
        //}
        else
        {
            velocityY += gravity * Time.deltaTime;
            dir.y = velocityY;
        }
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            velocityY = jumpPower;
        }


        cc.Move(dir * speed * Time.deltaTime);



    }

    //void Move()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");
    //    Vector3 dir = new Vector3(h, 0, v);
    //    transform.Translate(dir.normalized * speed * Time.deltaTime);
    //}

    //void Jump()
    //{
    //    rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    //}
}
