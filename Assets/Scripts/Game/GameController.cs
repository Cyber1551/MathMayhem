  using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Runtime.Serialization.Formatters;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    public Light Sun;
    public ReflectionProbe ReflectionProbe;
    public Light[] NightLights = new Light[0];
    public TMP_InputField[] inputs;
    public TMP_InputField selectedInput;
    private int selectedId;
    public GameObject endGamePanel;

    public static GameController Instance { get; set; }


    private float colorHUE;
    private float startSunIntensity;
    private Quaternion startSunRotation;
    private Color startAmbientLight;
    private float startAmbientIntencity;
    private float startReflectionIntencity;
    private LightShadows startLightShadows;

    public GameObject PlayerPrefabIdle;
    public Skill effect1;
    public Transform PlayerSpawn;
    public GameObject currentPlayerPrefab;

    public Skill effect2;
    public GameObject PlayerPrefabEffect2;


    public GameObject EnemyPrefabIdle;
    public Skill effect1_enemy;
    public Transform EnemySpawn;
    public GameObject currentEnemyPrefab;


    private Skill equippedPlayer;
    private Skill equippedEnemy;
    public Camera mainCamera;
    public enum GameState { START, PLAYERTURN, PLAYERANIM, ENEMYTURN, ENEMYANIM };
    public enum AttackState {HIT, MISS}
    public enum BlockState {HIT, RESIST}
    public GameState currentState;
    public AttackState attackState;
    public BlockState blockState;
    public Problem currentProblem;
    public float difficulty = 0.05f;
    [SerializeField] private TextMeshProUGUI currentProblemText;
   [SerializeField] private Image countdownImage;


    public GameObject playerStatPanel;
    public GameObject enemyStatPanel;


    private void Awake()
    {
        Instance = this;

    }
    void Start () {
        currentState = GameState.START;
        startSunIntensity = Sun.intensity;
	    startSunRotation = Sun.transform.rotation;
	    startAmbientLight = RenderSettings.ambientLight;
	    startAmbientIntencity = RenderSettings.ambientIntensity;
	    startReflectionIntencity = RenderSettings.reflectionIntensity;
	    startLightShadows = Sun.shadows;

      ActivateState();


	}
public void Attack()
{
  Destroy(currentPlayerPrefab);
  currentPlayerPrefab = Instantiate(effect2.prefab, PlayerSpawn.position, PlayerSpawn.rotation, PlayerSpawn);
  currentPlayerPrefab.transform.SetParent(PlayerSpawn);
  if (attackState == AttackState.HIT)
  {
    currentPlayerPrefab.transform.GetChild(0).GetChild(1).GetComponent<RFX4_TransformMotion>().CollidesWith = LayerMask.GetMask("HitCollider");
    currentPlayerPrefab.transform.GetChild(0).GetChild(1).GetComponent<RFX4_TransformMotion>().skill = equippedPlayer;

  }
  else if(attackState == AttackState.MISS)
  {
    currentPlayerPrefab.transform.GetChild(0).GetChild(1).GetComponent<RFX4_TransformMotion>().CollidesWith = LayerMask.GetMask("MissCollider");
     GameObject.Find("EnemySpawn").GetComponent<Enemy>().PopupDamage("MISS!");
  }
StartCoroutine(RespawnIdle());

}
public void EnemyAttack()
{
  Destroy(currentEnemyPrefab);
  currentEnemyPrefab = Instantiate(effect1_enemy.prefab, EnemySpawn.position, EnemySpawn.rotation, EnemySpawn);
  currentEnemyPrefab.transform.SetParent(EnemySpawn);
  if (blockState == BlockState.HIT)
  {
    currentEnemyPrefab.transform.GetChild(3).GetChild(0).GetComponent<RFX4_TransformMotion>().CollidesWith = LayerMask.GetMask("PlayerHitCollider");
    currentEnemyPrefab.transform.GetChild(3).GetChild(0).GetComponent<RFX4_TransformMotion>().skill = equippedEnemy;
  }
  else if(blockState == BlockState.RESIST)
  {
    currentEnemyPrefab.transform.GetChild(3).GetChild(0).GetComponent<RFX4_TransformMotion>().CollidesWith = LayerMask.GetMask("PlayerHitCollider");
    currentEnemyPrefab.transform.GetChild(3).GetChild(0).GetComponent<RFX4_TransformMotion>().skill = equippedEnemy;
     GameObject.Find("PlayerSpawn").GetComponent<Player>().PopupDamage("RESIST!");
  }
StartCoroutine(RespawnIdleEnemy());

}
IEnumerator RespawnIdle()
{
  yield return new WaitForSeconds(5.2f);
  Destroy(currentPlayerPrefab);
  currentPlayerPrefab = Instantiate(PlayerPrefabIdle, PlayerSpawn.position, PlayerSpawn.rotation, PlayerSpawn);
  currentPlayerPrefab.transform.SetParent(PlayerSpawn);
  if (GameObject.Find("EnemySpawn").GetComponent<Enemy>().currentHealth <= 0)
  {
     //Win
    SceneManager.LoadScene("Victory");
  }
}
IEnumerator RespawnIdleEnemy()
{
  yield return new WaitForSeconds(2.5f);
  Destroy(currentEnemyPrefab);
  currentEnemyPrefab = Instantiate(EnemyPrefabIdle, EnemySpawn.position, EnemySpawn.rotation, EnemySpawn);
  currentEnemyPrefab.transform.SetParent(EnemySpawn);
  if (GameObject.Find("PlayerSpawn").GetComponent<Player>().currentHealth <= 0)
  {
     //Lose
    SceneManager.LoadScene("Defeat");
  }
}
public void ChangeSelectedInputField(int id)
{
  playerStatPanel.SetActive(false);
  enemyStatPanel.SetActive(false);
  selectedInput = inputs[id];
  selectedId = id;
}
public void DeselectInput(int id)
{
  if (selectedInput == inputs[id])
  {
    selectedInput = null;
  }
}
  void Update()
  {

    if (Input.GetKeyUp(KeyCode.Tab))
    {
      if (selectedInput == null)
      {
        playerStatPanel.SetActive(false);
        enemyStatPanel.SetActive(false);
      }
      else
      {
        selectedId++;
        if (selectedId > 2) selectedId = 0;
        selectedInput = inputs[selectedId];
        selectedInput.ActivateInputField();
      }

    }
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      if(selectedInput == null)
      {
        playerStatPanel.SetActive(true);
          enemyStatPanel.SetActive(true);
      }
    }
    if (Input.GetKeyUp(KeyCode.Return))
    {
      if(selectedInput == null) return;
      if (currentProblem.checkAnswer(inputs[0].text, inputs[1].text))
      {
        Debug.Log("TRUE");
        if (currentState == GameState.PLAYERTURN)
        {
          attackState = AttackState.HIT;
          currentState = GameState.PLAYERANIM;
        }
        else if (currentState == GameState.ENEMYTURN)
        {
          blockState = BlockState.RESIST;
          currentState = GameState.ENEMYANIM;
        }

        currentProblem.answered = true;
      }
      else
      {
        Debug.Log("FALSE");

        if (currentState == GameState.PLAYERTURN)
        {
          attackState = AttackState.MISS;
          currentState = GameState.PLAYERANIM;
        }
        else if (currentState == GameState.ENEMYTURN)
        {
          blockState = BlockState.HIT;
          currentState = GameState.ENEMYANIM;
        }
        currentProblem.answered = true;
      }

      ActivateState();
    }
    if (Input.GetKeyUp(KeyCode.Escape))
    {

      SceneManager.LoadScene("MainMenu");

    }


  }

  public void ActivateState()
  {
    switch(currentState)
    {
      case GameState.START:
        currentPlayerPrefab = Instantiate(PlayerPrefabIdle, PlayerSpawn.position, PlayerSpawn.rotation, PlayerSpawn);
        currentPlayerPrefab.transform.SetParent(PlayerSpawn);
        equippedPlayer = effect2;
        equippedEnemy = effect1_enemy;
        currentEnemyPrefab = Instantiate(EnemyPrefabIdle, EnemySpawn.position, EnemySpawn.rotation, EnemySpawn);
        currentEnemyPrefab.transform.SetParent(EnemySpawn);
        currentState = GameState.PLAYERTURN;
        ActivateState();
      break;
      case GameState.PLAYERTURN:
        GameObject.Find("EnemySpawn").GetComponent<Enemy>().Regen();
        currentProblem = GenerateQuestion();
      currentProblemText.text = currentProblem.equations;
      currentTime = currentProblem.maxTime;
      Debug.Log("ANS: " + currentProblem.answerX);
      if (!countdownRunning)
      {
        StartCoroutine(BeginCountDown());
      }


      break;
      case GameState.PLAYERANIM:
        //StopCoroutine(BeginCountDown());
        Attack();
        currentState = GameState.ENEMYTURN;
        ActivateState();
      break;
      case GameState.ENEMYTURN:
      GameObject.Find("PlayerSpawn").GetComponent<Player>().Regen();
      currentProblem = GenerateQuestion();
    currentProblemText.text = currentProblem.equations;
    currentTime = currentProblem.maxTime;
    Debug.Log("ANS: " + currentProblem.answerX);
    if (!countdownRunning)
    {
      StartCoroutine(BeginCountDown());
    }

      break;
    case GameState.ENEMYANIM:
    EnemyAttack();
    currentState = GameState.PLAYERTURN;
    ActivateState();
      break;
    }
  }
  public Problem GenerateQuestion()
  {
    Debug.Log("GEN");
     return new Problem(difficulty);
  }
bool countdownRunning = false;
float currentTime = 0.0f;
  IEnumerator BeginCountDown()
  {
    countdownRunning = true;

    while (currentTime > 0 && !currentProblem.answered)
    {
      yield return new WaitForSeconds(1f);
      currentTime -= currentProblem.amtPerSec;
      countdownImage.fillAmount = currentTime / currentProblem.maxTime;
    }
    if(!currentProblem.answered)
    {
        Debug.Log("TIME");
        if (currentState == GameState.PLAYERTURN)
        {
          attackState = AttackState.MISS;
          currentState = GameState.PLAYERANIM;
        }
        else if (currentState == GameState.ENEMYTURN)
        {
          blockState = BlockState.HIT;
          currentState = GameState.ENEMYANIM;
        }
        countdownRunning = false;

      ActivateState();
    }


  }


}
