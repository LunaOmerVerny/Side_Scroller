using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    float Barredevie;
    Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    // Update is called once per frame
    void Update()
    {
        Barredevie = Player.GetComponent<Character>().currentHealth;
        slider.value = Barredevie;

    }

}