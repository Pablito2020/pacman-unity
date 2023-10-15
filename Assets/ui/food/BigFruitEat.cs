using UnityEngine;

public class BigFruitEat : MonoBehaviour
{

    public delegate void FruitEaten();
    public static event FruitEaten OnBigFruitEaten;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        OnBigFruitEaten?.Invoke();
    }

}
