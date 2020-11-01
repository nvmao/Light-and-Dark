using UnityEngine;
using System.Collections;

public class BlurOnAwaken
{
    float waitTime = 1.5f;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Color beginColor;
    Collider2D collider;


    public BlurOnAwaken(Collider2D collider,SpriteRenderer sprite, Animator animator = null)
    {
        this.animator = animator;
        beginColor = sprite.color;
        this.spriteRenderer = sprite;
        this.collider = collider;

        if (animator != null)
        {
            animator.enabled = false;
        }
        sprite.color = new Color(10, 10, 10, 10);
        if(collider != null)
        {
            collider.enabled = false;
        }
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);

        if(animator != null)
        {
            animator.enabled = false;
        }
        spriteRenderer.color = beginColor;
        
        if(collider != null)
        {
            collider.enabled = true;
        }
    }
}
