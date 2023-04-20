using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.CodeBase.Infrustructure.SceneData
{
    [CreateAssetMenu(fileName = "BuildData")]
    public class BuildStaticData : ScriptableObject
    {
        [SerializeField] public List<LocationStaticData> LocationData;
    }
}
