using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    [SerializeField] GameObject healthIconTemplate;
    [SerializeField] GameObject healthContainer;
    [SerializeField] float healthIconDistance = 0.5f;

    private List<GameObject> healthIcons = new List<GameObject>();
    private int healthIndex;

    //start health counter with specific number of health
    public void StartHealthCounter(int currentHealth) {
        for (int i = 0; i < currentHealth; i++) {
            GameObject newHealth = null;

            //instantiate new health icon
            newHealth = Instantiate(healthIconTemplate, healthContainer.transform);
            newHealth.name = "Heart " + i;

            //positioning new health
            RectTransform newHealthTransform = newHealth.GetComponent<RectTransform>();
            newHealthTransform.anchoredPosition = new Vector2(-healthIconDistance * i, 0);

            //add object to list in order to keep track of changing lifes
            healthIcons.Add(newHealth);
        }

        healthIconTemplate.SetActive(false); //hides original template
    }

    //update health counter
    public void UpdateHealthCounter(bool isDamage) {
        if (isDamage) {
            //lost health
            healthIcons[healthIndex].GetComponent<Animator>().SetTrigger("HealthLoss");
            healthIndex++;
        }
        else {
            //got health
            healthIcons[healthIndex].GetComponent<Animator>().SetTrigger("HealthGain");
            healthIndex--;
        }
    }
}