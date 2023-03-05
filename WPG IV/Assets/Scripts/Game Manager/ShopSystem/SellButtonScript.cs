
public class SellButtonScript : ButtonScript
{ 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //itemData = ShopSystem.Instance.GetCurrentItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnClick()
    {
        ShopSystem.Instance.ButtonEventSellItem();
    }
}
