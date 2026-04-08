using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EmoteWheelButtonController : MonoBehaviour
{
    private Animator anim;
    public string emoteName;
    //private bool selected = false;
    public TextMeshProUGUI emoteNameText;
    public Animator playerAnim;
    //public bool StopEmote;

    void Start()
    {
        anim = GetComponent<Animator>();
        emoteNameText.text = "";
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
        emoteNameText.text = emoteName;
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
        emoteNameText.text = "";
    }

    public void PlayEmote(string triggerName)
    {
        playerAnim.ResetTrigger("Wave");
        playerAnim.ResetTrigger("Thankful");
        playerAnim.ResetTrigger("TwistDance");
        playerAnim.ResetTrigger("BackFlip");
        playerAnim.SetLayerWeight(1, 1f);
        playerAnim.SetTrigger(triggerName);
        StartCoroutine(reseter());
    }

    //public void BoolUpdate() // Called via animation event
    //{
    //    StopEmote = true;
    //}
    private IEnumerator reseter()
    {
        yield return new WaitForSeconds(3f);
        ResetAnimationState();
    }
    public void ResetAnimationState()
    {
        // ✅ First wait for StopEmote to become true
        //yield return new WaitUntil(() => StopEmote);
        Debug.Log("ok");
        // ✅ Now reset the animation state
        playerAnim.SetLayerWeight(1, 0f);
        playerAnim.ResetTrigger("Wave");
        playerAnim.ResetTrigger("Thankful");
        playerAnim.ResetTrigger("TwistDance");
        playerAnim.ResetTrigger("BackFlip");

        // ✅ Reset StopEmote for the next animation cycle
        //StopEmote = false;
    }
}
