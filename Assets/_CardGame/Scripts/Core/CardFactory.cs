// CardFactory.cs
using UnityEngine;
using Niksan.CardGame.Data;
namespace Niksan.CardGame
{

    [System.Serializable]
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private BasicCard _cardPrefab;
        [SerializeField] private Transform _cardParent;
        [SerializeField] private int _initialPoolSize = 10;

        private ObjectPool<BasicCard> _pool;


        void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            _pool = new ObjectPool<BasicCard>(_cardPrefab, _cardParent, _initialPoolSize);
        }

        public BasicCard CreateCard(Card data, Vector3 position)
        {
            BasicCard card = _pool.Get();
            card.transform.position = position;
            card.transform.localScale = Vector3.one;
            card.SetData(data);
            return card;
        }

        public void RecycleCard(BasicCard card)
        {
            _pool.ReturnToPool(card);
        }

        public void Clear()
        {
            _pool.Clear();
        }
    }
}
