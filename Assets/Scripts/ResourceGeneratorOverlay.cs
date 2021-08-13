using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        //Debug.Log(resourceGenerator.GetAmountGeneratedPerSecond());
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    /************************ LOOPING ************************/
    private void Update() {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1f, 1f);
    }

    /************************ METHODS ************************/
}
