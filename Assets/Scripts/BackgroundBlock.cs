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
}
