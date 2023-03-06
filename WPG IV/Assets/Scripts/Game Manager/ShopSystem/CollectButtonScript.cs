
public class CollectButtonScript : ButtonScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnClick()
    {
        ShopSystem.Instance.ButtonEventCollectItem();
    }

}
