using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour {
    public Animator _anim;
    private Text _damageText;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = _anim.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        _damageText = _anim.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        _damageText.text = text;
    }

    public void SetTextColor(Color textColor)
    {
        _damageText.color = new Color(textColor.r, textColor.g, textColor.b, textColor.a);
    }
}
