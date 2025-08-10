using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYSTEM : MonoBehaviour
{
    public GameObject gameObj;
    private Animator animator;
    public GameObject soundPos;

    [Header("Âm thanh")]
    public AudioClip soundOn;

    private float time = 1f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    public void SetActiveTrue()
    {
        gameObj.SetActive(true);
    }

    public void PlaySound()
    {
        animator.SetTrigger("onclick");
        AudioSource.PlayClipAtPoint(soundOn, soundPos.transform.position);
        StartCoroutine(PlaySoundForOneSecond());
    }
    public void SetActiveFalse()
    {
        gameObj.SetActive(false);
    }

    private IEnumerator PlaySoundForOneSecond()
    {
        if (soundOn != null)
        {
            AudioSource.PlayClipAtPoint(soundOn, soundPos.transform.position);
            yield return new WaitForSeconds(time);
        }
    }
    public void ToggleGameObject()
    {
        animator.SetTrigger("onclick");
        bool isActive = gameObj.activeSelf;
        gameObj.SetActive(!isActive);
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
