using Assets.Source.CodeBase.Infrustructure.SceneData;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Source.CodeBase.Editor
{
    [CustomEditor(typeof(BuildStaticData))]
    public class BuildStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildStaticData buildData = (BuildStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                buildData.LocationData =
                    FindObjectsOfType<BuildLocation>()
                    .Select(x => new LocationStaticData(x.LocationType, x.ProductType, x.IsOwned, x.transform.position, x.Price))
                    .ToList();

            }

            EditorUtility.SetDirty(target);
        }
    }

}
