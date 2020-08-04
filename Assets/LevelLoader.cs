using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public float transitionTiome = 1f;
    private int sceneID;
    private Animator animator;

    private void Start()
    {
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
        Debug.Log("Test 2");
        if (info.GoToScene == -1)
        {
            StartCoroutine(LoadLevel(sceneID + 1));
        }
        else
        {
            StartCoroutine(LoadLevel(info.GoToScene));
        }

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("End Level");
        yield return new WaitForSeconds(transitionTiome);
        SceneManager.LoadScene(levelIndex);
    }
}
