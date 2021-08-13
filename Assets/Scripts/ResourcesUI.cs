using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourcesUI : MonoBehaviour {

    /************************ FIELDS ************************/
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO,Transform> resourceTypeTransformDictionary;

    /************************ INITIALIZE ************************/
    private void Awake() {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach(ResourceTypeSO resourceType in resourceTypeList.list) {
            Transform resourceTransform = Instantiate(resourceTemplate,transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index,0f);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }

    private void Start() {
        ResourceManager.Instance.OnResourceAmmountChanged += ResourceManager_OnResourceAmmountChanged;
        UpdateResourceAmount();
    }

    

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/
    private void ResourceManager_OnResourceAmmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
