using System.Collections;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField] 
    private AnimationCurve curveMovement; // 이동 제어 그래프

    private float appearTime = 0.5f; //블록이 등장할 때 소요되는 시간

    public void Setup(Vector3 parentPosition)
    {
        // 우측 화면 바깥에 생성된 블록이 부모 오브젝트 위치까지 이동
        StartCoroutine(OnMoveTo(parentPosition, appearTime));
    }

    IEnumerator OnMoveTo(Vector3 end, float time)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }
}
