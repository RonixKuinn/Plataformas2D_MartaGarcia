using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    
    public static GameManager instance;
    private int coins = 0;
    private bool isPaused;
    private bool pauseAnimation;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] Text coinText;
    [SerializeField] Animator pausePanelAnimator;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        pausePanelAnimator = pauseCanvas.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if(!isPaused && !pauseAnimation)
        {
            isPaused = true;
            Time.timeScale = 0;
            SoundManager.instance.PlaySFX(SoundManager.instance.audioSource, SoundManager.instance.pauseAudio);
            pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation)
        {
            pauseAnimation = true;
            StartCoroutine(ClosePauseAnimation());
        }
    }

    IEnumerator ClosePauseAnimation()
    {
        pausePanelAnimator.SetBool("Close", true);
        yield return new WaitForSecondsRealtime(0.20f);
        Time.timeScale = 1;
       
        pauseCanvas.SetActive(false);
        isPaused = false;
        pauseAnimation = false;
    }

    public void AddCoin()
    {
        coins++;        //es lo mismo que "coin +=1;"
        coinText.text = coins.ToString();
    }
}
