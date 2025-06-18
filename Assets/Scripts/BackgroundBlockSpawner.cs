using UnityEngine;

public class BackgroundBlockSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject blockPrefab; //배경으로 배치되는 블록 프리팹

    [SerializeField] 
    private int orderInLayer; //배치되는 블록들이 그려지는 순서
    
    //격자 형태로 생성되는 블록의 개수, 블록 하나의 절반 크기
    private Vector2Int blockCount = new Vector2Int(10, 10);
    private Vector2 blockHalf = new Vector2(0.5f, 0.5f);

    void Awake()
    {
        for (int y = 0; y < blockCount.y; y++)
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                //블록 판의 중앙이 (0,0,0)이 되도록 배치
                float px = -blockCount.x * 0.5f + blockHalf.x + x;
                float py = blockCount.y * 0.5f - blockHalf.y - y;
                Vector3 position = new Vector3(px, py, 0);
                //블록 생성
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                //방금 생성한 블록의 그려지는 순서 설정
                block.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }
    }
}
