using UnityEngine;

public class KeypadKeyBehavior : Clickable
{
    Vector3 initialPosition;
    RectTransform myBody;
    private bool isClicked = false;
    const float displacement = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<RectTransform>();
        initialPosition = myBody.localPosition;
    }

    public void Click()
    {
        if (!HasSomethingBeenClickedOverMe())
            return;

        isClicked = true;
        myBody.localPosition = new Vector3
            (initialPosition.x + displacement,
            initialPosition.y - displacement);
    }

    private void Unclick()
    {
        isClicked = false;
        myBody.localPosition = initialPosition;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Click();

        if (Input.GetMouseButtonUp(0) && isClicked)
            Unclick();
    }

}
