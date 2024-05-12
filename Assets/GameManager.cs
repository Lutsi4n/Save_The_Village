using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audio;
    public AudioSource btnAudio;

    public Image_Timer harvest_timer;
    public Image_Timer eating_timer;

    public Image raidTimerImg;
    public Image warriorTimerImg;
    public Image peasantTimerImg;

    public Button peasBtn;
    public Button warBtn;

    public Text resourceText;
    public Text statsfinishText;
    public Text enemyCountText;

    public int peasantCount;
    public int warriorCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarriors;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    public int enemyCount;
    public GameObject GameOverScreen;
    public GameObject PauseScreen;
    public GameObject WinScreen;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;

    public int roundsSurvive;
    public int countPeasantsFinish;
    public int countWarriorsFinish;
    public int countWheatFinish;



    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
    }

    void Update()
    {
        raidTimer -= Time.deltaTime;
        raidTimerImg.fillAmount = raidTimer / raidMaxTime;
        if (wheatCount <= 0)
        {
            warBtn.interactable = false;
            peasBtn.interactable = false;
        }

        if ((wheatCount > 0) && (warriorTimer == -2))
        {
            warBtn.interactable = true;
        }

        if ((wheatCount > 0) && (peasantTimer == -2))
        {
            peasBtn.interactable = true;
        }

        if (raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorCount -= nextRaid;
            nextRaid += raidIncrease;
            enemyCount += raidIncrease;
            roundsSurvive++;
        }

        if (harvest_timer.tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
            countWheatFinish += peasantCount * wheatPerPeasant;
        }
        if (eating_timer.tick)
        {
            wheatCount -= warriorCount * wheatToWarriors;
        }
        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            peasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            peasantTimerImg.fillAmount = 1;
            peasBtn.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
        }
        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            warriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
            warriorTimerImg.fillAmount = 1;
            warBtn.interactable = true;
            warriorCount += 1;
            warriorTimer = -2;
        }
        UpdateText();

        if (warriorCount < 0)
        {
            Time.timeScale = 0;
            GameOverScreen.SetActive(true);
        }
        if (wheatCount >= 100)
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
        }
    }

    public void CreatePeasant()
    {
        btnAudio.Play();
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        countPeasantsFinish++;
        peasBtn.interactable = false;
    }

    public void CreateWarrior()
    {
        btnAudio.Play();
        wheatCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        countWarriorsFinish++;
        warBtn.interactable = false;
    }

    public void OnPause()
    {
        btnAudio.Play();
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    public void OffPause()
    {
        btnAudio.Play();
        Time.timeScale = 1.0f;
        PauseScreen.SetActive(false);
    }

    void UpdateText()
    {
        resourceText.text = peasantCount + "\n" + warriorCount + "\n\n" + wheatCount;
        statsfinishText.text = roundsSurvive + "\n\n" + countWarriorsFinish + "\n" + countPeasantsFinish + "\n" + countWheatFinish;
        enemyCountText.text = enemyCount + "";
    }

    public void MusciOnOff()
    {
        if (audio.isPlaying)
        {
            btnAudio.Play();
            audio.Pause();
        }
        else
        {
            btnAudio.Play();
            audio.Play();
        }
    }
}
