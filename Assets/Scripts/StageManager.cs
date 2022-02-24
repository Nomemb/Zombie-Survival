using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private GameObject instantZombie;

    private PlayerController player;
    public int stage;
    public int zombieCount;
    public enum ZombieType { Normal = 0, Boss }
    [SerializeField]
    private int normalZombieSpawnRate;
    private bool isClearStage;
    [SerializeField]
    private Transform[] spawnZones;
    public GameObject[] zombies;
    private List<int> zombieList;
    // Start is called before the first frame update
    void Start()
    {
        zombieList = new List<int>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClearStage)
        {
            ClearStage();
        }
    }

    private void ClearStage()
    {
        isClearStage = true;
        // 남아있는 좀비가 0마리면 클리어 문구를 띄우고
        Debug.Log("Stage " + stage + " Clear!");
        StartCoroutine(NextStageCoroutine());

    }
    IEnumerator NextStageCoroutine()
    {
        yield return new WaitForSeconds(3f);
        // 대기 후 좀비 스폰
        stage += 1;
        for (int i = 0; i < stage * 10; i++)
        {
            int random = Random.Range(0, 100);
            int randomZombie = (normalZombieSpawnRate <= random) ? (int)ZombieType.Boss : (int)ZombieType.Normal;
            zombieList.Add(randomZombie);
            zombieCount++;
        }

        while (zombieList.Count > 0)
        {
            int randomZone = Random.Range(0, spawnZones.Length);
            instantZombie = Instantiate(zombies[zombieList[0]], spawnZones[randomZone].position, spawnZones[randomZone].rotation);
            Zombie zombie = instantZombie.GetComponent<Zombie>();
            zombie.target = player.transform;
            zombieList.RemoveAt(0);

            yield return new WaitForSeconds(1f);
        }

        while (zombieCount > 0)
        {
            yield return null;
        }
        ClearStage();
    }
}
