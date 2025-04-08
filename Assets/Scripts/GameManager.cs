using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider hpSlider;    
    public Slider staminaSlider;
    public Slider expSlider;
    public Transform playerSpawnPoint;
    public Transform playerTrans;

    public GUIManager guiManager;
    public ItemManager itemManager;
    public Inventory inventory;
    public EquipmentManager equipmentManager;
    public ItemQuickSlot quickSlot;
    public EnemySpawnParent enemySpawnParent;
    public SkillManager skillManager;
    public SoundManager soundManager;

    public Player player;    

    public static GameManager GetInstance()
    {
        return instance;
    }

    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        CheckSlider();
        if(player.iDie)
        {
            StartCoroutine(RespawnPlayer());
        }
    }    

    void CheckSlider()
    {
        if (hpSlider == null) return;
        if (staminaSlider == null) return;
        
        hpSlider.value = player.curHealth / player.maxHealth;        
        staminaSlider.value = player.curStamina / player.maxStamina;
        expSlider.value = player.curExp / player.maxExp;
        
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(5f);        
        playerTrans.position = playerSpawnPoint.position;
        player.curHealth = player.maxHealth;
        player.iDie = false;
    }

    public void NewGame()
    {
        LoadingScene.LoadScene("GamePlay Scene");
    }

    public void Option()
    {

    }

    public void GameExit()
    {
        Application.Quit();
    }
}
