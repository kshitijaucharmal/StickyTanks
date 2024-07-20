using UnityEngine;
public class Health 
{
    int maxHealth;
    int currentHealth;
    public Health(int maxHealth){
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public int GetHealth(){
        return currentHealth;
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        Debug.Log("currentHealth = "+currentHealth);
        if (currentHealth <= 0){
            Die();
        }
    }
    public void Heal(int heal){
        currentHealth += heal;
        Debug.Log("currentHealth = "+currentHealth);
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }
    void Die(){

    } 
}
