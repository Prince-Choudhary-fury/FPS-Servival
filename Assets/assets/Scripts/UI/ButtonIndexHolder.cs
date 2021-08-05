using UnityEngine;

public class ButtonIndexHolder : MonoBehaviour
{

    public UIButtonManager UIButton;
    public int buttonIndex;

    public void OnButtonClick()
    {
        UIButton.buttonPressed(this.GetComponent<ButtonIndexHolder>().buttonIndex, this.gameObject.name);
    }

}
