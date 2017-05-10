using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTap : MonoBehaviour {

    public GameObject game_manager;

    public LayerMask TouchInputMask;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, TouchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;

                    if (recipient.tag == "cube")
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            game_manager.SendMessage("swap_numbers", recipient, SendMessageOptions.DontRequireReceiver);
                        }
                    }
                }
            }
        }
	}
}
