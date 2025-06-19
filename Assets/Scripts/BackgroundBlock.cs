using System.Collections;
using UnityEngine;

public enum BlockState
{
    Empty = 0,
    Fill = 1
}
public class BackgroundBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 배경 블록의 색상 제어를 위한 컴포넌트
    
    public BlockState BlockState { private set; get; } // 배경 블록의 상태

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BlockState = BlockState.Empty;
    }

    public void FillBlock(Color color)
    {
        BlockState = BlockState.Fill;
        spriteRenderer.color = color;
    }
    
    // 줄이 완성되면 배경 블록의 Scale 애니메이션을 재생하고,
    // 배경 블록 정보를 초기화
    public void EmptyBlock()
    {
        BlockState = BlockState.Empty;

        StartCoroutine("ScaleTo", Vector3.zero);
    }
    
    // 0.15초 동안 블록의 크기를 1에서 0으로 축소하고,
    // 블록의 색상과 크기 재설정
    IEnumerator ScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;
        float time = 0.15f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }
        
        // 축소 애니메이션이 종료되면 블록의 색상을 하얀색으로 설정하고, 블록 크기를 1로 설정
        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
