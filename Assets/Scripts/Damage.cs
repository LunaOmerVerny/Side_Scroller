using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag(targetTag))
        {
            
            float targetHealth = collision.GetComponent<Character>().currentHealth;
            if (targetHealth > 0 )
            {
                collision.GetComponent<Character>().currentHealth -= damageAmount;
            }
            else if (targetHealth <= 0)
            {
                //TO DO : Add Stun Effect
                Debug.Log("STUNNED");
            }
        }
    }
}



