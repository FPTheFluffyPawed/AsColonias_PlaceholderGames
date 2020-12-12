using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

    private bool _hasFaded;
    private float timer;

    private void Start()
    {
        _hasFaded = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && _hasFaded)
            SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GAZ-66")
        {
            if(blackScreen.GetComponent<Animator>() != null)
            {
                Animator a = blackScreen.GetComponent<Animator>();

                a.SetTrigger("Fade");
            }

            timer = 2.0f;
            _hasFaded = true;
        }
    }
}
