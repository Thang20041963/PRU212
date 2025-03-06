using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
  public void SetMaxHealth (int health)
    {
        Debug.Log(health);
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
}
