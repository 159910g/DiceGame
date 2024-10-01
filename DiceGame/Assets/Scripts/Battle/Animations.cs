using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Animations : MonoBehaviour
{
    [SerializeField] bool isPlayer;

    Vector3 originalPos;
    SpriteRenderer SR;

    public void Start()
    {
        originalPos = transform.localPosition * -1;
        SR = GetComponent<BattleCharacter>().SpriteRenderer;
        Debug.Log(originalPos);
    }
    public void PlayAttackAnimation()
    {
        Debug.Log(originalPos.x + 50f);
        
        var sequence = DOTween.Sequence();
        if(isPlayer)
        {
           sequence.Append(SR.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f)); //second value is how long the animation occurs for
        }
        else
        {
            sequence.Append(SR.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        }

        sequence.Append(SR.transform.DOLocalMoveX(originalPos.x, 0.25f));

        Debug.Log(transform.localPosition);
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence(); //total animation time is 0.4 secs
        sequence.Append(SR.DOColor(Color.gray, 0.1f));
        sequence.Append(SR.DOColor(Color.white, 0.1f));
        sequence.Append(SR.DOColor(Color.gray, 0.1f));
        sequence.Append(SR.DOColor(Color.white, 0.1f));
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(SR.transform.DOLocalMoveY(originalPos.y - 150f, 1f));
        sequence.Join(SR.DOFade(0f, 1f));    
    }
}
