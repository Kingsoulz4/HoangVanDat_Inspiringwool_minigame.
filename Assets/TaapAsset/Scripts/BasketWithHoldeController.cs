using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketWithHoldeController : BasketController
{
    LTDescr dropWoolDelay = null;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void SetCurrentBasket()
    {
        base.SetCurrentBasket();
        CancelAutoDrop(); 
    }

    public override void ResetState()
    {
        base.ResetState();
        CancelAutoDrop();
    }

    public void CancelAutoDrop()
    {
        if (dropWoolDelay != null)
        {
            LeanTween.cancel(dropWoolDelay.id);
            dropWoolDelay = null;
        }
        LeanTween.delayedCall(15f, () =>
        {
            DropWool();
        });
    }

    public override void PushWool()
    {
        base.PushWool();
        CancelAutoDrop();
    }
}
