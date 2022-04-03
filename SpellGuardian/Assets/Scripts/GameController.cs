using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public GameObject UpgradeMenu;
    private GameObject mainCharacter;
    private bool isOpenUpgradeMenu;

    public float gameSeconds;
    public int gameMinutes;
    public Text timeDisplay;

    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        menu.SetActive(false);
        isOpenUpgradeMenu = false;

        gameMinutes = 0;
        gameSeconds = 0;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openMenu();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeDamage();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeAttackSpeed();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeMovementSpeed();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeAmountWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeHP();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mainCharacter.GetComponent<PlayerManager>().upgradeRecoverHP();
        }

        gameTimeDisplay();
    }

    public void gameTimeDisplay()
    {
        gameSeconds += Time.deltaTime;

        if (gameSeconds >= 60)
        {
            gameSeconds = 0;
            gameMinutes++;
        }

        if (gameMinutes >= 10)
        {
            if (gameSeconds >= 10)
            {
                timeDisplay.text = gameMinutes + ":" + (int)gameSeconds;
            }
            else
            {
                timeDisplay.text = gameMinutes + ":0" + (int)gameSeconds;
            }
        }
        else
        {
            if (gameSeconds >= 10)
            {
                timeDisplay.text = "0"+gameMinutes + ":" + (int)gameSeconds;
            }
            else
            {
                timeDisplay.text = "0"+gameMinutes + ":0" + (int)gameSeconds;
            }
        }

    }

    public void ToggleUpgradeMenu()
    {
        if (isOpenUpgradeMenu)
        {
            isOpenUpgradeMenu = false;
            UpgradeMenu.transform.position = new Vector2(UpgradeMenu.transform.position.x,
                                                         UpgradeMenu.transform.position.y - 10);
        }
        else
        {
            isOpenUpgradeMenu = true;
            UpgradeMenu.transform.position = new Vector2(UpgradeMenu.transform.position.x,
                                                         UpgradeMenu.transform.position.y + 10);
        }
    }
    public void openMenu()
    {
        if (menu.activeSelf == true)
        {
            ResumeGame();
        }
        else
        {
            menu.SetActive(true);
            PauseGame();
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        mainCharacter.GetComponent<PlayerManager>().rg.velocity = Vector2.zero;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (menu.activeSelf == true)
        {
            menu.SetActive(false);
        }
    }

}
