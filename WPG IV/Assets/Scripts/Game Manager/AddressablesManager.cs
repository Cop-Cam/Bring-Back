/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
 
public class AddressablesManager : GenericSingletonClass<AddressablesManager>
{
    // public void ReplaceUISprite(string spriteAddressablesAddress, Image setSpriteHere)
    // {
    //     //The Sprite will be loaded, and when it's done it will call the method "SetSpriteWhenDone"
    //     Addressables.LoadAssetAsync<Sprite>(spriteAddressablesAddress).Completed += op => SetSpriteWhenDone(op,setSpriteHere);
    // }
 
    // private void SetSpriteWhenDone(AsyncOperationHandle<Sprite> op, Image setSpriteHere)
    // {
    //     switch (op.Status)
    //     {
    //         case AsyncOperationStatus.Succeeded:
    //             setSpriteHere.sprite = op.Result;
    //             break;
    //         case AsyncOperationStatus.Failed:
    //             Debug.LogError("Sprite load failed.");
    //             break;
    //         default:
    //             // case AsyncOperationStatus.None:
    //             break;
    //     }
    // }

    public override void Awake() 
    {
        base.Awake();
        //GetAllScriptableObject();
    }

    // private void GetAllScriptableObject(string path)
    // {
    //     //Addressables.LoadAssetsAsync<ScriptableObject>("").Completed;
    //     //Addressables.LoadAssetsAsync<ScriptableObject>(path, null).Completed += OnAssetsLoaded;

    // }

    private void OnAssetsLoaded(AsyncOperationHandle<IList<GameObject>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var asset in handle.Result)
            {
                // Do something with the loaded asset
            }
        }
    }

    public IEnumerator LoadAllAssetsByKey<T>(string key, List<T> listResult, object Type) where T : UnityEngine.Object
    {
        Debug.LogWarning("Object loading is started");
        // Will load all objects that match the given key.  
        // If this key is an Addressable label, it will load all assets marked with that label
        AsyncOperationHandle<IList<T>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<T>(key, null, Addressables.MergeMode.Union);

        Debug.LogWarning("Object is loaded");
        yield return loadWithSingleKeyHandle;
        IList<T> singleKeyResult = loadWithSingleKeyHandle.Result;
        listResult = singleKeyResult as List<T>;
    }

    // public IEnumerator LoadAllAssetsByKey(string key, List<InventoryItemData> listResult)
    // {
    //     Debug.LogWarning("object loading is started");
    //     //Will load all objects that match the given key.  
    //     //If this key is an Addressable label, it will load all assets marked with that label
    //     AsyncOperationHandle<IList<InventoryItemData>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<InventoryItemData>(key, null, Addressables.MergeMode.Union); 
        
    //     Debug.LogWarning("object is loaded");
    //     yield return loadWithSingleKeyHandle;
    //     IList<InventoryItemData> singleKeyResult = loadWithSingleKeyHandle.Result;
    //     listResult = singleKeyResult as List<InventoryItemData>;

    //     // //Loads all assets that match the list of keys.
    //     // //With no MergeMode parameter specified, the Result will be that of the first key.
    //     // AsyncOperationHandle<IList<GameObject>> loadWithMultipleKeys =
    //     //     Addressables.LoadAssetsAsync<GameObject>(new List<object>() { "key1", "key2" },
    //     //         obj =>
    //     //         {
    //     //             //Gets called for every loaded asset
    //     //             Debug.Log(obj.name);
    //     //         });
    //     // yield return loadWithMultipleKeys;
    //     // IList<GameObject> multipleKeyResult1 = loadWithMultipleKeys.Result;


    //     // //Use this only when the objects are no longer needed
    //     // Addressables.Release(loadWithSingleKeyHandle);
    //     // Addressables.Release(loadWithMultipleKeys);
    // }

    // public void LoadAllAssetsByKeyStartCoroutine(string key, List<ScriptableObject> listResult)
    // {
    //     //StartCoroutine(LoadAllAssetsByKey(key, listResult));
    //     //StartCoroutine(LoadAllAssetsByKey(key, listResult, type));
    // }


}*/