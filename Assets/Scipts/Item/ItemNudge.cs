using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    // Start is called before the first frame update
    private WaitForSeconds pause;
    private bool isAnimation = false;

    private void Awake()
    {
        pause = new WaitForSeconds(0.04f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAnimation == false)
        {
            if (gameObject.transform.position.x < other.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

     IEnumerator RotateAntiClock()
     {
         SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
         for (int i = 0; i < 4; i++)
         {
             spriteRenderer.transform.Rotate(0,0,2f);
             yield return pause;
         }

         
         for (int i = 0; i < 5; i++)
         {
             spriteRenderer.transform.Rotate(0,0,-2f);
             yield return pause;
         }
         spriteRenderer.transform.Rotate(0,0,2f);
         yield return pause;
         isAnimation = false;


     }

     IEnumerator RotateClock()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.transform.Rotate(0,0,-2f);
            yield return pause;

        }

         
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.transform.Rotate(0,0,2f);
            yield return pause;
        }
        
        spriteRenderer.transform.Rotate(0,0,-2f);
         
        yield return pause;

        isAnimation = false;
    }
}
