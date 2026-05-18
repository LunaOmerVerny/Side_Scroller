using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private string targetTag = "Player";

    [SerializeField] private float damageInterval = 1f;

    private float timer = 0f;
    private Character characterInside = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            characterInside = collision.GetComponent<Character>();
             timer = damageInterval; // dégat instantané
        }
    }

         
    private void Update()
    {
        //dégat même quand player immobile
        if (characterInside != null)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                timer = 0f;

                if (characterInside.currentHealth > 0)
                {
                    characterInside.currentHealth -= damageAmount;
                    Debug.Log("Dégâts ! Vie : " + characterInside.currentHealth);
                }
                else
                {
                    Debug.Log("STUNNED");
                }
            }
        }
    }  

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterInside = null;
            timer = 0f; //quand player sort reset à 0 le timer
        }
    }
}


