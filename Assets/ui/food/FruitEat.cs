using UnityEngine;

public class FruitEat : MonoBehaviour
{

    public delegate void FruitEaten();
    public static event FruitEaten OnFruitEaten;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        OnFruitEaten?.Invoke();
    }

}
