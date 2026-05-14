using UnityEngine;

public class RechargeEnergie : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag(targetTag)&& Input.GetKey(KeyCode.E))
        {
            Character character = collision.GetComponent<Character>();
            if (character != null)
            {
                character.currentHealth = character.MaxHealth;
                Debug.Log("Santé au MAX !");
            }
        }
    }
}
