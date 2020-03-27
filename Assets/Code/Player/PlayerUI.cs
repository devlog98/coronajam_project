using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    [SerializeField] GameObject healthTemplate;
    [SerializeField] GameObject healthContainer;
    [SerializeField] float healthDistance = 0.5f;

    //start health counter with specific number of health
    public void StartHealthCounter(int health) {
        for(int i = 0; i < health; i++) {
            GameObject newHealth = null;

            //instantiate new health icon
            newHealth = Instantiate(healthTemplate, healthContainer.transform);
            newHealth.name = "Heart " + i;

            //positioning new health
            RectTransform newHealthTransform = newHealth.GetComponent<RectTransform>();
            newHealthTransform.anchoredPosition = new Vector2(healthDistance * i, 0);
        }

        healthTemplate.SetActive(false); //hides original template
    }
}