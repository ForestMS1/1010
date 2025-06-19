using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] 
    private BackgroundBlockSpawner backgroundBlockSpawner; // 배경 블록 생성
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner; // 배경 블록 생성
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner; // 드래그 블록 생성
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem; // 블록 배치

    private BackgroundBlock[] backgroundBlocks; // 생성한 배경 블록 정보 저장
    private int currentBlockCount; // 현재 남아있는 드래그 블록 개수

    private readonly Vector2Int blockCount = new Vector2Int(10, 10); // 블록 판에 배치되는 블록 개수
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f); // 블록 하나의 절반 크기
    private readonly int maxDragBlockCount = 3; // 한번에 생성할 수 있는 드래그 블록 개수

    private List<BackgroundBlock> filledBlockList; // 줄이 완성된 블록들을 삭제하기 위해 임시 저장하는 리스트
    void Awake()
    {   
        // 줄이 완성된 블록들을 삭제하기 위해 임시 저장하는 리스트
        filledBlockList = new List<BackgroundBlock>();
        
        // 뒷 배경으로 사용되는 배경 블록판 생성
        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        
        // 드래그 블록을 배치할 때 색상이 변경되는 배경 블록판 생성
        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);
        
        // 블록 배치 시스템
        blockArrangeSystem.Setup(blockCount, blockHalf, backgroundBlocks, this);
        
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
    
    // 블록 배치 후처리
    // 드래그 블록 삭제/생성, 줄 완성, 게임오버, 점수
    public void AfterBlockArrangement(DragBlock block)
    {
        // 블록 배치 완료 후 행동 처리
        StartCoroutine(OnAfterBlockArrangement(block));
    }

    IEnumerator OnAfterBlockArrangement(DragBlock block)
    {
        // 배치가 완료된 드래그 블록 삭제
        Destroy(block.gameObject);
        
        // 완성된 줄이 있는지 검사하고, 완성된 줄의 블록들은 별도로 저장
        int filledLineCount = CheckFilledLine();
        
        // 줄이 완성된 블록들을 삭제 (마지막에 배치한 블록을 기준으로 퍼져나가듯이 삭제)
        yield return StartCoroutine(DestroyFilledBlocks(block));
        
        // 블록 배치에 성공했으니 현재 남아있는 드래그 블록의 개술르 1 감소
        currentBlockCount--;
        // 현재 배치 가능한 드래그 블록의 개수가 0이면 드래그 블록 생성
        if (currentBlockCount == 0)
        {
            SpawnDragBlocks();
        }
    }
    
    private int CheckFilledLine()
    {
        int filledLineCount = 0;
        
        filledBlockList.Clear();
        
        // 가로 줄 검사
        for (int y = 0; y < blockCount.y; y++)
        {
            int fillBlockCount = 0;
            for (int x = 0; x < blockCount.x; x++)
            {
                // 해당 블록이 채워져 있으면 fillBlockCount 1 증가
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }
            
            // 완성된 줄이 있으면 해당 줄의 모든 배경 블록을 filledBlockList에 저장
            if (fillBlockCount == blockCount.x)
            {
                for (int x = 0; x < blockCount.x; x++)
                {
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }

                filledLineCount++;
            }
        }
        
        // 세로 줄 검사
        for (int x = 0; x < blockCount.x; x++)
        {
            int fillBlockCount = 0;
            for (int y = 0; y < blockCount.y; y++)
            {
                // 해당 블록이 채워져 있으면 fillBlockCount 1 증가
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill) fillBlockCount++;
            }
            
            // 완성된 줄이 있으면 해당 줄의 모든 배경 블록을 filledBlockList에 저장
            if (fillBlockCount == blockCount.y)
            {
                for (int y = 0; y < blockCount.y; y++)
                {
                    filledBlockList.Add(backgroundBlocks[y*blockCount.x+x]);
                }
                filledLineCount++;
            }
        }

        return filledLineCount;
    }

    IEnumerator DestroyFilledBlocks(DragBlock block)
    {
        // 마지막에 배치한 블록(block)과 거리가 가까운 순서로 정렬
        filledBlockList.Sort((a,b) => 
            (a.transform.position-block.transform.position).sqrMagnitude.CompareTo((b.transform.position-block.transform.position).sqrMagnitude));
        
        // filledBlockList에 저장되어 있는 배경 블록을 순서대로 초기화
        for (int i = 0; i < filledBlockList.Count; ++i)
        {
            filledBlockList[i].EmptyBlock();

            yield return new WaitForSeconds(0.01f);
        }
        filledBlockList.Clear();
    }


}
