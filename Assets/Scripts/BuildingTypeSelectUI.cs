using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class BuildingTypeSelectUI : MonoBehaviour {

    /************************ FIELDS ************************/
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;
    private Transform arrowButton;
    
    /************************ INITIALIZE ************************/
    private void Awake() {
        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        Transform buttonTemplate = transform.Find("buttonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList =  Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = +130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);

        arrowButton.Find("buildingImage").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("buildingImage").GetComponent<RectTransform>().sizeDelta = new Vector2(0f,-30f);

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();

        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
            TooltipUI.Instance.Show("Arrow");
        };

        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
            TooltipUI.Instance.Hide();
        };

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            if (ignoreBuildingTypeList.Contains(buildingType)) {
                continue;
            }
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offsetAmount = +130f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);

            buttonTransform.Find("buildingImage").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            mouseEnterExitEvents =  buttonTransform.GetComponent<MouseEnterExitEvents>();

            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };

            buttonTransformDictionary[buildingType] = buttonTransform;
            index++;
        }
    }

    

    private void Start() {
        UpdateActiveBuildingTypeButton();
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    

    /************************ LOOPING ************************/
    private void Update() {
    }

    /************************ METHODS ************************/

    private void MouseEnterExitEvents_OnMouseEnter(object sender, System.EventArgs e) {
        throw new System.NotImplementedException();
    }

    private void UpdateActiveBuildingTypeButton() {
        arrowButton.Find("selectedImage").gameObject.SetActive(false);

        foreach(BuildingTypeSO buildingType in buttonTransformDictionary.Keys) {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("selectedImage").gameObject.SetActive(false);
        }
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if(activeBuildingType == null) {
            arrowButton.Find("selectedImage").gameObject.SetActive(true);
        }
        else {
            buttonTransformDictionary[activeBuildingType].Find("selectedImage").gameObject.SetActive(true);
        }
    }
    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }
}
