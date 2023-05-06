using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //Access to components
    public TextMeshProUGUI shurikenText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI stealthKillText;
    public TextMeshProUGUI detectedKillText;
    public TextMeshProUGUI notDetectedText;
    public TextMeshProUGUI finishedLevelText;
    public TextMeshProUGUI detectedText;
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI rankLetterText;

    public GameObject[] itemPrefabs;
    public GameObject keyItem;

    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject creditsMenu;
    public GameObject missionMenu;
    public GameObject missionCompleteMenu;
    public GameObject toBeContinuedMenu;

    public Button playButton;
    public Button optionButton;
    public Button quitButton;
    public Button restartButton;
    public Button startButton;

    public AudioClip clickSound;
    public AudioSource playerAudio; //*/

    //Create Variables
    private int shurikenCount;
    public bool isGameActive;
    public int stealthKillPoint;
    public int detectedKillPoint;
    public int notDetectedPoint;
    public int finishedLevelPoint;
    public int detectedPoint;
    private int total;

    // Start is called before the first frame update
    void Start()
    {
        stealthKillPoint = 0;
        detectedKillPoint = 0;
        notDetectedPoint = 20;
        finishedLevelPoint = 80;
        detectedPoint = 0;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateShurikenCount(int shurikenToAdd)
    {
        shurikenCount += shurikenToAdd;

        if (shurikenCount > 99)
        {
            shurikenCount = 99;
        }
        shurikenText.text = "" + shurikenCount;
        //print("GameManager shuriken count: " + shurikenCount);
    }

    //Starts game
    public void StartGame()
    {
        shurikenCount = 0;
        shurikenText.text = "" + shurikenCount;
        isGameActive = true;
        CloseAllMenu(); //Hides all menu screens
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Start Game audio");//*/
        playerAudio.Play();
    }

    public void BackToMain()
    {
        CloseAllMenu();        
        mainMenu.gameObject.SetActive(true);
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Back to main audio");//*/
    }

    public void OptionMenu()
    {
        optionMenu.gameObject.SetActive(true);

        creditsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Options audio");//*/
    }

    public void CreditsMenu()
    {
        CloseAllMenu();
        creditsMenu.gameObject.SetActive(true);
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Credits audio");//*/
    }

    public void MissionMenu()
    {
        CloseAllMenu();
        missionMenu.gameObject.SetActive(true);
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Mission menu audio");//*/

    }

    /// <summary>
    /// Mission complete screen
    /// Able to go to next level
    /// Able to go to Main menu
    /// </summary>
    public void MissionCompleteMenu()
    {
        CloseAllMenu();
        totalPoints();
        isGameActive = false;
        stealthKillText.text = "Stealth Kill: " + stealthKillPoint;
        detectedKillText.text = "Detected Kill: " + detectedKillPoint;
        notDetectedText.text = "Not Spotted: " + notDetectedPoint;
        finishedLevelText.text = "Finished Level: " + finishedLevelPoint;
        detectedText.text = "Spotted: " + detectedPoint;
        missionCompleteMenu.gameObject.SetActive(true);
        playerAudio.Stop();
    }

    public void totalPoints()
    {
        total = stealthKillPoint + detectedKillPoint + notDetectedPoint + finishedLevelPoint + detectedPoint;
        totalText.text = "Total: " + total;

        if (total >= 100)
        {
            rankText.text = "Master Ninja";
            rankLetterText.text = "A";
        }
        else if (total >= 90 && total < 100)
        {
            rankText.text = "Jonin Ninja";
            rankLetterText.text = "B";
        }
        else if (total >= 80 && total < 90)
        {
            rankText.text = "Chunin Ninja";
            rankLetterText.text = "C";
        }
        else if (total >= 70 && total < 80)
        {
            rankText.text = "genin Ninja";
            rankLetterText.text = "D";
        }
        else
        {
            rankText.text = "Novice Ninja";
            rankLetterText.text = "F";
        }
    }

    public void ToBeContinuedMenu()
    {
        CloseAllMenu();
        toBeContinuedMenu.gameObject.SetActive(true);
        playerAudio.PlayOneShot(clickSound, 1.0f);
    }

    public void CloseAllMenu()
    {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(false);
        missionMenu.gameObject.SetActive(false);
        missionCompleteMenu.gameObject.SetActive(false);
        toBeContinuedMenu.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        //Show game over text
        gameOverText.gameObject.SetActive(true);
        //Show restart button
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    //Restart game and load the current active Scene
    public void RestartGame()
    {
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Restart game audio");//*/
        SceneManager.LoadScene("My Game");
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Mission 2");
    }

    public void QuitGame()
    {
        playerAudio.PlayOneShot(clickSound, 1.0f);
        //print("Quit Game audio");//*/
        Application.Quit();
        print("Player quit game");
    }

    public void SpawnItem(Vector3 spawnLocation)
    {       
        int roll = 0;
        float roll2 = Random.Range(0.0f, 3.0f);
        Vector3[] locations = {spawnLocation, spawnLocation + new Vector3(0.5f, 0, 0) };
        for (int i = 0; i < 2; i++)
        {          
            if (roll2 != 0.0f)
            {
                i = 1;
                //print("Spawn 1 item: " + roll2);
            }
            if (roll2 == 0.0f)
            {
                //print("Spawn 2 items" + roll2);
            }
            roll = Random.Range(0, 6);  //Max is exclusive when using integers
            //print("Roll: " + roll);
            if( roll == 0 || roll == 5)
            {
                Instantiate(itemPrefabs[0], locations[i], itemPrefabs[0].transform.rotation);
                //print("Spawn Health Item");
            }
            else if (roll == 1 || roll == 4)
            {
                Instantiate(itemPrefabs[1], locations[i], itemPrefabs[1].transform.rotation);
                //print("Spawn Shuriken Item");
            }
        }//*/
    }

    /// <summary>
    /// Called in animation
    /// Used by HealthBar.cs
    /// </summary>
    /// <param name="spawnLoaction"></param>
    public void SpawnKeyItem(Vector3 spawnLoaction)
    {
        Instantiate(keyItem, spawnLoaction, keyItem.transform.rotation);
        //print("Spawn keyItem");
    }
}