using System;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] 
    private BackgroundBlockSpawner backgroundBlockSpawner; // 배경 블록 생성
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner; // 배경 블록 생성
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner; // 드래그 블록 생성

    private BackgroundBlock[] backgroundBlocks; // 생성한 배경 블록 정보 저장
    private int currentBlockCount; // 현재 남아있는 드래그 블록 개수

    private readonly Vector2Int blockCount = new Vector2Int(10, 10); // 블록 판에 배치되는 블록 개수
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f); // 블록 하나의 절반 크기
    private readonly int maxDragBlockCount = 3; // 한번에 생성할 수 있는 드래그 블록 개수


    void Awake()
    {
        // 뒷 배경으로 사용되는 배경 블록판 생성
        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        
        // 드래그 블록을 배치할 때 색상이 변경되는 배경 블록판 생성
        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        
        // 드래그 블록 생성
        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        // 현재 드래그 블록의 개수를 최대(3)로 설정
        currentBlockCount = maxDragBlockCount;
        //드래그 블록 생성
        dragBlockSpawner.SpawnBlocks();
    }


}
