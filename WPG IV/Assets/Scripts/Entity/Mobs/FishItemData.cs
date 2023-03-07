
using UnityEngine;

[CreateAssetMenu(fileName = "FishItemData", menuName = "SO/FishItemData", order = 1)]
public class FishItemData : InventoryItemData 
{
    public enum FishTypes {Endemic, Invansive};
    public Sprite fishPicture { get; private set; }//Foto ikan dewasa
    public string fishDescription { get; private set; }//deksripsi ikan dewasa
    public FishTypes fishTypes; //tipe ikan (endemic atau invansif)
    public bool isFishFeeded { get; set; }
    public int daysToMatured { get; set; } //lamanya ikan bertumbuh
    public bool isFishMatured { get; set; }
    public int fishPoint; //objective point



    // public void InitializeFish()
    // {
    //     hoursToMatured = daysToMatured * 24;

    //     FishMaturingMethod();
    // }

    // private IEnumerator FishMaturingMethod()
    // {
    //     Debug.Log("maturing fish");
    //     while(!isMatured)
    //     {
    //         hoursToMatured -= TimeManager.Instance.CalculateTimeOfDay();
    //         if(hoursToMatured <= 0)
    //         {
    //             Debug.Log("fish is matured");
    //             isMatured = true;
    //         }
    //         yield return null;
    //     }
    // }
}

