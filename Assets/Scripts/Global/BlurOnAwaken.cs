using UnityEngine;
using System.Collections;

public class BlurOnAwaken
{
    float waitTime = 1.5f;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Color beginColor;

    public BlurOnAwaken(SpriteRenderer sprite, Animator animator = null)
    {
        this.animator = animator;
        beginColor = sprite.color;
        this.spriteRenderer = sprite;

        if (animator != null)
        {
            animator.enabled = false;
        }
        sprite.color = new Color(10, 10, 10, 10);

        sprite.GetComponent<mao.ICanDisable>().disabled();
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);

        if(animator != null)
        {
            animator.enabled = false;
        }
        spriteRenderer.color = beginColor;
        spriteRenderer.GetComponent<mao.ICanDisable>().enabled();
    }
}
