using System.Collections;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField] 
    private Transform[] blockSpawnPoints; //드래그 가능한 블록이 배치되는 위치
    [SerializeField]
    private GameObject[] blockPrefabs; // 생성 가능한 모든 블록 프리팹

    void Awake()
    {
        StartCoroutine(nameof(OnSpawnBlocks));
    }

    IEnumerator OnSpawnBlocks()
    {
        // 드래그 블록 3개 생성
        for (int i = 0; i < blockSpawnPoints.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            
            // 생성할 드래그 블록 순번
            int index = Random.Range(0, blockPrefabs.Length);
            //드래그 블록 생성
            Instantiate(blockPrefabs[index], blockSpawnPoints[i].position, Quaternion.identity, blockSpawnPoints[i]);
        }
    }
}
