using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private TextMeshProUGUI _continueText;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            if(asyncOperation.progress >= 0.9f)
            {
                _continueText.gameObject.SetActive(true);

                Debug.Log("Finished loading");

                if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
