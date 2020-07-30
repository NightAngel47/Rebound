/* Primary Author: Alex Cline
 * Co-Author: Steven Drovie - Sound
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // levels
    public static GameManager instance = null;
    public int nextLevel = 0;
    public Texture2D[] levels = new Texture2D[0];

    // music
    AudioSource audioSource;
    public AudioClip[] music;

    // player
    public static bool isPlayerDead = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadLevel();
            if (nextLevel > levels.Length)
                ResetNextLevel();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1) && SceneManager.GetActiveScene().name == "GameScene" && nextLevel < levels.Length)
            NextLevel();
#endif
    }

    public void ResetNextLevel()
    {
        nextLevel = 0;
        SaveLevel();
    }

    public void NextLevel()
    {
        nextLevel++;
        SaveLevel();
        SceneManager.LoadScene("LoadingScreen");
    }

    public void NextLevel(int givenLevel)
    {
        nextLevel = givenLevel;
        SaveLevel();
        SceneManager.LoadScene("LoadingScreen");
    }

    public void SaveLevel()
    {
        SaveSystem.SavedCurrentLevel(instance);   
    }

    public void LoadLevel()
    {
        SavedLevel level = SaveSystem.LoadCurrentLevel();
        if (level != null)
            nextLevel = level.levelSaved;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchMusic(int i)
    {
        audioSource.clip = music[i];
        audioSource.Play();
    }
}