using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SYSTEM : MonoBehaviour
{
    // public TextMeshProUGUI text;
    public GameObject gameObj;
    private Animator animator;

    [Header("Âm thanh")]
    public AudioSource audioSource;
    public AudioClip soundOn;

    private float time = 1f; // thời gian chạy âm thanh

    private void Start()
    {
        animator = gameObj.GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void SetActiveTrue()
    {
        animator.SetTrigger("a");
        gameObj.SetActive(true);
        StartCoroutine(PlaySoundForOneSecond());
    }

    public void SetActiveFalse()
    {
        gameObj.SetActive(false);
    }

    private IEnumerator PlaySoundForOneSecond()
    {
        if (soundOn != null && audioSource != null)
        {
            audioSource.clip = soundOn;
            audioSource.Play();
            yield return new WaitForSeconds(time);
            audioSource.Stop();
        }
    }

}
