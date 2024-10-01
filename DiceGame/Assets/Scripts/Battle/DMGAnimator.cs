using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DMGAnimator : MonoBehaviour
{
    [SerializeField] TextMeshPro dmgValue;
    
    public void SetData(int dmg)
    {
        dmgValue.text = dmg.ToString();

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(transform.position.y + 50f, 0.25f)).OnComplete(() => Destroy(gameObject)); // Destroy after the animation completes
    }
}
