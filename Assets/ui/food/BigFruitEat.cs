using System.Collections.Generic;
using UnityEngine;

public class BigFruitEat : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public delegate void FruitEaten();
    public static event FruitEaten OnBigFruitEaten;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        OnBigFruitEaten?.Invoke();
        StartCoroutine(PlayAndDestroy());
    }
    
    private IEnumerator<WaitForSeconds> PlayAndDestroy()
    {
        audioSource.Play();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }

}
