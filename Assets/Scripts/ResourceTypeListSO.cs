using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
public class ResourceTypeListSO : ScriptableObject {

    public List<ResourceTypeSO> list;
}
