using UnityEngine;

public class FruitEat : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}
