using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMerger : MonoBehaviour
{
    #region Public members

    [Header("Setup")]
    public char m_suffixDelimiter = '_';
    public string m_mainSceneSuffix = "Main";
    public string m_thisSceneSuffix;
    public List<string> m_allSceneSuffixes;

    #endregion


    #region System methods

    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        string levelPrefix = currentSceneName.Substring(0, currentSceneName.LastIndexOf(m_suffixDelimiter));

        bool isMainLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains(m_mainSceneSuffix))
            {
                isMainLoaded = true;
                break;
            }
        }

        // Main scene is loading : we have to load all other scenes in additive mode
        if (m_thisSceneSuffix.Contains(m_mainSceneSuffix))
        {
            for (int i = 0; i < m_allSceneSuffixes.Count; i++)
            {
                SceneManager.LoadScene(levelPrefix + m_suffixDelimiter + m_allSceneSuffixes[i], LoadSceneMode.Additive);
            }
        }
        // We are loading from another scene : we have to load the main scene
        else
        {
            // ...unless it is already loaded (avoid infinite loop)
            if (isMainLoaded)
            {
                return;
            }
            SceneManager.LoadScene(levelPrefix + m_suffixDelimiter + m_mainSceneSuffix);
        }
    }

    #endregion


    #region Main methods

    #endregion


    #region Tools and debug

    #endregion


    #region Private and protected members

    #endregion
}
