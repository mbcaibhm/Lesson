using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float speed = 5f;
    //public Transform target;
    public Transform target1st;
    public Transform target3rd;
    bool isFPS = true;




    // Update is called once per frame
    void Update()
    {
        ChangeView();
    }

    void ChangeView()
    {
        if(Input.GetKeyDown("1"))
        {
            isFPS = true;
        }
        if(Input.GetKeyDown("3"))
        {
            isFPS = false;
        }

        if(isFPS)
        {
            transform.position = target1st.position;
        }
        else
        {
            transform.position = target3rd.position;
        }
    }

    private void LateUpdate()
    {
        //FollowTarget();
    }

    //void FollowTarget()
    //{
    //    Vector3 dir = target.position - transform.position;
    //    dir.Normalize();
    //    transform.Translate(dir * speed * Time.deltaTime);

    //    if(Vector3.Distance(transform.position, target.position) < 1.0f)
    //    {
    //        transform.position = target.position;
    //    }
    //}
}
