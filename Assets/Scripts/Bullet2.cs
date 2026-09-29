using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    //총알클래스 하는일
    //총알발사 및 생성은 플레이어가 하고
    //1. 총알은 그냥 생성된 이후 날라가기만 하면 된다.
    //플레이어가 사용할 총알이니 방향은 위쪽으로 움직인다.
    //2. 충돌이 되거나 화면밖으로 벗어나면 자기자신 삭제한다

    public float Speed = 2f;
    public float upwardSpeed = 3f;
    public float gravity = 29.81f;
    private Vector3 currentVelocity;

    void Start()
    {
        // 초기 발사 속도 벡터 설정
        currentVelocity = (transform.forward * Speed) + (Vector3.up * upwardSpeed);
    }

    void Update()
    {
        // 1. 매 프레임 중력 적용 (y축 속도 감소)
        currentVelocity.y -= gravity * Time.deltaTime;

        // 2. 위치 갱신
        transform.position += currentVelocity * Time.deltaTime;

        // 3. 진행 방향으로 머리 회전
        if (currentVelocity.sqrMagnitude > 0.01f)
        {
            transform.forward = currentVelocity.normalized;
        }
    }

    //카메라 화면밖으로 나가서 보이지 않게 되면 호출되는 함수
    //나중에 디스트로이존을 만들어서 충돌처리를 할 예정이고
    //임시로 사용
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
