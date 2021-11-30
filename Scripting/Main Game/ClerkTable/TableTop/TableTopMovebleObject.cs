using UnityEngine;

public abstract class TableTopMovebleObject : Clickable
{
    protected bool isClicked = false;
    protected float timeWhenLastClicked = 0;
    protected float leftLimit, rightLimit, topLimit, botLimit;
    
    public TableTopMovebleObject()
    {
        SetDragLimits();
    }

    protected void Click()
    {
        if (!HaveIBeenClicked())
            return;

        isClicked = true;

        transform.SetAsLastSibling();

        if (Time.time - timeWhenLastClicked < 0.5f)
            DoubleClick();

        timeWhenLastClicked = Time.time;
    }

    protected abstract void DoubleClick();

    protected void SetDragLimits(
        float leftLimit = 1150, 
        float rightLimit = 1600,
        float topLimit = 475,
        float botLimit = 75)
    {
        this.leftLimit = leftLimit;
        this.rightLimit = rightLimit;
        this.topLimit = topLimit;
        this.botLimit = botLimit;
    }

    protected void Drag()
    {
        if (CheckIfPositionIsValid().PositionX)
        {
            transform.position = new Vector3
                (
                Input.mousePosition.x,
                transform.position.y,
                0
                );
        }

        if (CheckIfPositionIsValid().PositionY)
        {
            transform.position = new Vector3
                (
                transform.position.x,
                Input.mousePosition.y,
                0
                );
        }
    }

    protected (bool PositionX, bool PositionY) CheckIfPositionIsValid()
    {
        return (
            Input.mousePosition.x > leftLimit 
            && Input.mousePosition.x < rightLimit,

            Input.mousePosition.y < topLimit 
            && Input.mousePosition.y > botLimit
            );
    }

    protected abstract void Drop();

    protected bool CheckIfDroppedOverCashRegister()
    {
        if (transform.position.x < 1100 && transform.position.y < 350)
            return true;

        return false;
    }

    protected bool CheckIfDroppedOverClientDeliveryArea()
    {
        if (transform.position.x < 1430 && transform.position.y > 500)
            return true;

        return false;
    }

    protected void RemoveMe()
    {
        Destroy(gameObject);
    }

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Click();

        if (Input.GetMouseButtonUp(0) && isClicked)
        {
            isClicked = false;
            Drop();
        }

        if (isClicked)
            Drag();
    }
}
