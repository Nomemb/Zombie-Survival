using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{
    private GameObject instantZombie;
    private TextInfoManager info;
    private PlayerController player;

    private string _info;
    public int stage;
    public int zombieCount;
    public enum ZombieType { Normal = 0, Boss }
    public string sceneName;
    [SerializeField]
    private int normalZombieSpawnRate;
    private bool isClearStage;

    [SerializeField]
    private Transform[] spawnZones;
    public GameObject[] zombies;
    private List<int> zombieList;

    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        zombieList = new List<int>();
        info = FindObjectOfType<TextInfoManager>();
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
        if (stage != 0)
        {
            info.AddInfo("Stage " + stage + " Clear!");
        }
        StartCoroutine(NextStageCoroutine());

    }
    IEnumerator NextStageCoroutine()
    {
        yield return new WaitForSeconds(3f);
        // 대기 후 좀비 스폰
        stage += 1;
        info.AddInfo("====Stage " + stage + "====");

        for (int i = 0; i < stage * 15; i++)
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

            yield return new WaitForSeconds(1.5f);
        }

        while (zombieCount > 0)
        {
            yield return null;
        }
        ClearStage();
    }

    public void GameOver()
    {
        int _currentScore = mainPanel.GetComponentInChildren<ScoreManager>().currentScore;
        mainPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponentInChildren<ScoreManager>().currentScore = _currentScore;
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectMap()
    {
        SceneManager.LoadScene("Main");
    }

}
