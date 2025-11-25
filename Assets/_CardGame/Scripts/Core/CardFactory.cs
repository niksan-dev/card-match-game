// CardFactory.cs
using UnityEngine;
using Niksan.CardGame.Data;
namespace Niksan.CardGame
{

    [System.Serializable]
    public class CardFactory : Singleton<CardFactory>
    {
        [SerializeField] private BasicCard _cardPrefab;
        [SerializeField] private Transform _cardParent;
        [SerializeField] private int _initialPoolSize = 10;


        void Start()
        {
            Initialize();
        }
        public void Initialize()
        {

        }

        public BasicCard CreateCard(Card data, Vector3 position)
        {
            BasicCard card = ObjectPooler.Instance.SpawnFromPool("Card", position, Quaternion.identity).GetComponent<BasicCard>();
            card.transform.position = position;
            card.transform.localScale = Vector3.one;
            card.SetData(data);
            Debug.Log("Card Created from Factory with ID: " + data.id);
            return card;
        }

        public void RecycleCard(BasicCard card)
        {
            ObjectPooler.Instance.ReturnToPool(card.gameObject);
        }

        public void Clear()
        {
            // _pool.Clear();
            //ObjectPooler.Instance.cl
        }
    }
}
