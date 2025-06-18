using UnityEngine;

public class BackgroundBlockSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] blockPrefab; //배경으로 배치되는 블록 프리팹

    [SerializeField] 
    private int orderInLayer; //배치되는 블록들이 그려지는 순서
    
    //격자 형태로 생성되는 블록의 개수, 블록 하나의 절반 크기
    private Vector2Int blockCount = new Vector2Int(10, 10);
    private Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
