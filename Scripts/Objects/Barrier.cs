using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool isHollow;
    BoxCollider2D collider;
    SpriteRenderer sprite;
    float r;
    float g;
    float b;
    public float upperOpacity = 1f;
    public float lowerOpacity = 0.3f;

    void Start()
    {
       collider = GetComponent<BoxCollider2D>();
       sprite = GetComponent<SpriteRenderer>();
       r = sprite.color.r;
       g = sprite.color.g;
       b = sprite.color.b;

       if(isHollow) {
            collider.enabled = false;
            sprite.color = new Color(r,g,b,lowerOpacity);
       }
    }

    public void changeHollow() {
        collider.enabled = isHollow;
        isHollow = !isHollow;
        if(isHollow) {
            sprite.color = new Color(r,g,b,lowerOpacity);
        } else {
            sprite.color = new Color(r,g,b,upperOpacity);
        }
    }
}
