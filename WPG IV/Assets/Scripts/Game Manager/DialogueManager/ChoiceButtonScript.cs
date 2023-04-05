
public class ChoiceButtonScript : ButtonScript
{
    private int indexChoice;
    protected override void OnClick()
    {
        DialogueSystem.DialogueManager.Instance.MakeChoice(indexChoice);
    }

    public void SetThisButtonChoiceIndex(int index)
    {
        indexChoice = index;
    }
}
