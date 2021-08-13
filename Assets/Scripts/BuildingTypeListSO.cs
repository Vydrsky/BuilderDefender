using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="ScriptableObjects/BuildingTypeList")]
public class BuildingTypeListSO : ScriptableObject {

    public List<BuildingTypeSO> list;
}
