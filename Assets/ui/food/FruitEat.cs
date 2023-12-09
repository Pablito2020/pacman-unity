using System.Collections.Generic;
using UnityEngine;

public class FruitEat : MonoBehaviour
{
    public delegate void FruitEaten();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnFruitEaten?.Invoke();
        StartCoroutine(PlayAndDestroy());
    }

    public static event FruitEaten OnFruitEaten;


    private IEnumerator<WaitForSeconds> PlayAndDestroy()
    {
        audioSource.Play();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}