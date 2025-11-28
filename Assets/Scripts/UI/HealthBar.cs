using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public Gradient gradient;
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }
    
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        //Disminuir la barra de vida inmediatamente
        /*slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); */
        
        //Disminuir la barra de vida gradualmente
        StartCoroutine(DrainHealth(health));

    }

    private IEnumerator DrainHealth(float targetHealth)
    {
        while (slider.value > targetHealth)
        {
            slider.value -= 1;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
