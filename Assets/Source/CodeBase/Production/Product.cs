using UnityEngine;

namespace Assets.Source.CodeBase.Production
{
    public class Product : MonoBehaviour
    {
        [SerializeField] ProductType _type;
        [SerializeField] private int _price;
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private Transferer _transferer;


        public ProductType Type => _type;
        public int Price => _price;
        public float Height => _collider.size.y * transform.localScale.y;
        public Transferer Transferer => _transferer;
    }
}
