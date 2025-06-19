using UnityEngine;

public class BlockArrangeSystem : MonoBehaviour
{
    private Vector2Int blockCount;
    private Vector2 blockHalf;
    private BackgroundBlock[] backgroundBlocks;
    private StageController stageController;

    public void Setup(Vector2Int blockCount, Vector2 blockHalf,
        BackgroundBlock[] backgroundBlocks, StageController stageController)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
        this.backgroundBlocks = backgroundBlocks;
        this.stageController = stageController;
    }
    
    // 매개변수로 받아온 block을 배치할 수 있는지 검사하고,
    // 배치가 가능하면 블록 배치, 줄 완성 검사, 점수 계산 처리
    public bool TryArrangementBlock(DragBlock block)
    {
        // 블록 배치가 가능한지 검사
        for (int i = 0; i < block.ChildBlocks.Length; i++)
        {
            // 자식 블록의 월드 위치 (부모의 월드 좌표 + 자식의 지역 좌표)
            Vector3 position = block.transform.position + block.ChildBlocks[i];
            
            // 블록이 맵 내부에 위치하고 있는지?
            if (!IsBlockInsideMap(position)) return false;
            
            // 현재 위치에 이미 다른 블록이 배치되어 있는지?
            if (!IsOtherBlockInThisBlock(position)) return false;
        }
        
        // 블록 배치
        for (int i = 0; i < block.ChildBlocks.Length; i++)
        {
            // 자식 블록의 월드 위치 (부모의 월드 좌표 + 자식의 지역 좌표)
            Vector3 position = block.transform.position + block.ChildBlocks[i];
            
            // 해당 위치에 있는 배경 블록의 색상을 변경하고, 채움(BlockState.Fill)으로 변경
            backgroundBlocks[PositionToIndex(position)].FillBlock(block.Color);
        }
        
        // 블록 배치 후처리
        stageController.AfterBlockArrangement(block);

        return true;
    }
    
    
    // 매개변수로 받아온 위치가 배경 블록판의 바깥인지 검사
    // 블록판의 바깥이면 false, 왼쪽이면 true
    private bool IsBlockInsideMap(Vector3 position)
    {
        if (position.x < -blockCount.x * 0.5f + blockHalf.x || position.x > blockCount.x * 0.5f - blockHalf.x
                                                            || position.y < -blockCount.y * 0.5f + blockHalf.y ||
                                                            position.y > blockCount.y * 0.5f - blockHalf.y)
        {
            return false;
        }

        return true;
    }
    
    // 매개변수로 받아온 위치 정보를 바탕으로 맵에 배치된 블록의 index를 계산해서 반환
    private int PositionToIndex(Vector2 position)
    {
        float x = blockCount.x * 0.5f - blockHalf.x + position.x;
        float y = blockCount.y * 0.5f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }

    private bool IsOtherBlockInThisBlock(Vector2 position)
    {
        int index = PositionToIndex(position);

        if (backgroundBlocks[index].BlockState == BlockState.Fill)
        {
            return false;
        }

        return true;
    }
}
