using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    ////PlayerFire 역할?
    ////1. 사용자가 발사버튼을 누르게 되면 총알 생성

    //public GameObject bulletFactory1;            //총알 프리팹
    //public GameObject bulletFactory2;
    //public Transform firePoint;                 //총알 발사위치
    //public Transform cameraTransform;

    public Transform firePoint;
    public GameObject bImpactFactory;
    public GameObject bombFactory;
    public float power = 10f;



    void Update()
    {
        //Fire();
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit))
            {
                print("충돌오브젝트: " + hit.collider.name);
            }

            GameObject bulletImpact = Instantiate(bImpactFactory);
            bulletImpact.transform.position = hit.point;
            bulletImpact.transform.forward = hit.normal;

            //int layer = gameObject.layer;

            //layer = 1 << 6;
            //layer = 1 << 8 | 1 << 4 | 1 << 6;
            
            //if(Physics.Raycast(ray, out hit, 100, layer))
            //{

            //}
            //if(Physics.Raycast(ray, out hit, 100, ~layer))
            //{

            //}
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePoint.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            Vector3 dir = Camera.main.transform.up + (Camera.main.transform.forward * 2f);
            dir.Normalize();
            rb.AddForce(dir * power, ForceMode.Impulse);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Camera.main.fieldOfView = 20f;
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Camera.main.fieldOfView = 60f;
        }
    }

    //void Fire()
    //{
    //    //왼쪽컨트롤 키 또는 마우스 왼쪽 버튼 클릭
    //    //if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
    //    //{
    //    //}

    //    //위 코드와 동일하다
    //    if(Input.GetButtonDown("Fire1"))
    //    {
    //        //총알공장(총알프리팹)에서 무한대로 찍어낼 수 있다
    //        //Instantiate() 함수로 프리팹 파일을 게임 오브젝트로 생성시킨다

    //        Vector3 spawnPosition = firePoint != null ? firePoint.position : cameraTransform.position;
    //        Quaternion spawnRotation = cameraTransform.rotation;
    //        //총알 게임오브젝트 생성
    //        GameObject bullet = Instantiate(bulletFactory1, spawnPosition, spawnRotation);
    //        //총알 오브젝트 위치 지정
    //        //bullet.transform.position = firePoint.position;
    //    }

    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        //총알공장(총알프리팹)에서 무한대로 찍어낼 수 있다
    //        //Instantiate() 함수로 프리팹 파일을 게임 오브젝트로 생성시킨다
    //        Vector3 spawnPosition = firePoint != null ? firePoint.position : cameraTransform.position;
    //        Quaternion spawnRotation = cameraTransform.rotation;
    //        //총알 게임오브젝트 생성
    //        GameObject bullet2 = Instantiate(bulletFactory2, spawnPosition, spawnRotation);
    //        //총알 오브젝트 위치 지정
    //        //bullet2.transform.position = firePoint.position;
    //    }
    //}
}
