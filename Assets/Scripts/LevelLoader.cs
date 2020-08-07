using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public float transitionTiome = 1f;
    private int sceneID;
    private Animator animator;

    [SerializeField]
    private TMP_Text levelTitle;

    private void Start()
    {

        StartCoroutine(StartLevel());
        levelTitle.text = GameConstants.Current.getLevelData().Name;
        sceneID = SceneManager.GetActiveScene().buildIndex;

        animator = GetComponent<Animator>();

        GameEventSystem.Current.RegisterListener<EndLevelInfo>(HandleLevelEnd);
    }

    private void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<EndLevelInfo>(HandleLevelEnd);
        }
    }

    public void HandleLevelEnd(EndLevelInfo info)
    {
        if (info.GoToScene == -1)
        {
            StartCoroutine(LoadLevel(sceneID + 1));
        }
        else
        {
            StartCoroutine(LoadLevel(info.GoToScene));
        }

    }

    IEnumerator StartLevel()
    {
        GameConstants.Current.Paused = true;
        yield return new WaitForSeconds(2f);
        GameConstants.Current.Paused = false;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        GameConstants.Current.Paused = true;
        animator.SetTrigger("End Level");
        yield return new WaitForSeconds(transitionTiome);
        SceneManager.LoadScene(levelIndex);
    }
}
