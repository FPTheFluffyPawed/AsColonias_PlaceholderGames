using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _continueText;

    private void OnEnable()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(10);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            if(asyncOperation.progress >= 0.9f)
            {
                Debug.Log("Finished loading");

                _continueText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
