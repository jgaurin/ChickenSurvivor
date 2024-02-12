using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotationSpeedModifier = 0.1f;
    private Touch touch;

    // Update is called once per frame
    void Update()
    {
  
            // Partie rotation du joueur avec le doigt numéro 0

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeedModifier, 0f);
                    transform.rotation = rotationY * transform.rotation;

                }
            }

    }
}
