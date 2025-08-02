using System.Collections;
using TMPro;
using UnityEngine;

public class UIHienThiSatThuong : MonoBehaviour
{
    public float speed = 400f;
    public float timeToLive = 2.5f;
    public TextMeshProUGUI textTMP;

    public void KhoiTao(int dame)
    {
        if (textTMP != null)
            textTMP.text = dame.ToString();
        StartCoroutine(GoUpAndDestroy());
    }

    private IEnumerator GoUpAndDestroy()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * speed;

        while (elapsed < timeToLive)
        {
            float t = elapsed / timeToLive;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}