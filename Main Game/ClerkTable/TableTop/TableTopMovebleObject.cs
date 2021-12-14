using UnityEngine;

public abstract class TableTopMovebleObject : Clickable
{
    protected bool isClicked = false;
    protected float timeWhenLastClicked = 0;
    protected float leftLimit, rightLimit, topLimit, botLimit;
    protected Vector3 mousePositionTracker;
    
    public TableTopMovebleObject()
    {
        SetDragLimits();
    }

    protected void Click()
    {
        if (!HaveIBeenClicked())
            return;

        isClicked = true;
        mousePositionTracker = Input.mousePosition;

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

        Vector3 mouseDisplacement = Input.mousePosition - mousePositionTracker;
        mousePositionTracker = Input.mousePosition;

        if (CheckIfPositionIsValid(mouseDisplacement).PositionX)
        {
            transform.position = new Vector3
                (
                transform.position.x + mouseDisplacement.x,
                transform.position.y,
                0
                );
        }

        if (CheckIfPositionIsValid(mouseDisplacement).PositionY)
        {
            transform.position = new Vector3
                (
                transform.position.x,
                transform.position.y + mouseDisplacement.y,
                0
                );
        }
    }

    protected (bool PositionX, bool PositionY) CheckIfPositionIsValid(Vector3 displacement)
    {
        return (
            transform.position.x + displacement.x > leftLimit 
            && transform.position.x + displacement.x < rightLimit,

            transform.position.y + displacement.y < topLimit 
            && transform.position.y + displacement.y > botLimit
            );
    }

    protected abstract void Drop();

    protected bool CheckIfDroppedOverCashRegister()
    {
        if (Input.mousePosition.x < 1250 && Input.mousePosition.y < 325)
            return true;

        return false;
    }

    protected bool CheckIfDroppedOverClientDeliveryArea()
    {
        if (Input.mousePosition.x < 1450 && Input.mousePosition.y > 375)
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
