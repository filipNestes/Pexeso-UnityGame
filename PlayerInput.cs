using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !GameManager.instance.HasPicked() && !GameManager.instance.IsGameOver()) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit)) {
                Debug.Log(hit.transform.gameObject);
                Card currentCard = hit.transform.GetComponent<Card>();
                    //ADD NULL CHECK HERE
                    if(currentCard!=null) {
                        currentCard.FlipOpen(true);
                        GameManager.instance.AddCardToPickedList(currentCard);
                    }
            }
        }
    }
}
