using Assets.Source.CodeBase.Infrustructure.SceneData;
using UnityEditor;


namespace Assets.Source.CodeBase.Editor
{
    [CustomEditor(typeof(BuildLocation))]
    public class ProductLocationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildLocation productLocation = (BuildLocation)target;



            EditorUtility.SetDirty(target);
        }
    }
}
