using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Access to components
    public Slider healthSlider;
    public ParticleSystem explosionParticle;    //Reference object's particle system
    private GameManager gameManager;    //Reference Game Manager
    public Transform particleSpawnPoint;

    //Created variables
    public int maxHealth;
    [SerializeField] private int currentDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = 0;
        healthSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds parameter damage to currentDamage
    /// Updates health slider
    /// Check if currentDamage >= Maxhealth, if so destory gameobject
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentDamage += damage;

        if(currentDamage <= 0)
        {
            currentDamage = 0;
        }

        healthSlider.fillRect.gameObject.SetActive(true);
        healthSlider.value = currentDamage;
        /*print("Damage: " + damage);
        //print("Current Damage: " + currentDamage);
        print("health slider value: " + healthSlider.value);//*/
    }

    /// <summary>
    /// Called in animation
    /// </summary>
    public void DestoryCharacter()
    {
        if (currentDamage >= maxHealth && gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 1.0f);  //wait 1.0 seconds then destroy object
            explosionParticle.Play();
            Vector3 spawnlocation = particleSpawnPoint.transform.position;
            gameManager.SpawnItem(spawnlocation);//*/
            //print("Enemy died");
        }//*/

        if (currentDamage >= maxHealth && gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject, 1.0f);  //wait 1.0 seconds then destroy object
            explosionParticle.Play();
            Vector3 spawnlocation = particleSpawnPoint.transform.position;
            gameManager.SpawnKeyItem(spawnlocation);//*/
            //print("Boss died");
        }//*/
    }
}
