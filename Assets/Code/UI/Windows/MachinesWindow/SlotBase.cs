using Gameplay.Items;
using ObjectPooling;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Window.Inventory
{
    [RequireComponent(typeof(Button))]
    public abstract class SlotBase : MonoBehaviour, IIdEqualable, IPoolable
    {
        #region ACTIONS

        private Action OnClickAction;

        #endregion

        #region VARIABLES

        [SerializeField] protected Button button;
        [SerializeField] protected GameObject selectionFrame;
        [SerializeField] protected Image itemIcon;
        [SerializeField] protected TextMeshProUGUI countOfItems;

        protected int presentId;

        #endregion

        #region PROPERTIES

        public int Id => presentId;
        public PoolObject Poolable { get; set; }
        public bool HasItem => presentId > -1;
        public bool IsSelected { get; private set; }

        #endregion

        #region METHODS

        public abstract void RemoveItem();
        public abstract void RefreshCountInSlot();
        protected abstract void SetIcon();

        public virtual void InitializeClick(Action onClickAction)
        {
            OnClickAction = onClickAction;
            button.onClick.AddListener(HandleClickButton);
        }

        public virtual void CleanUp()
        {
            button.onClick.RemoveListener(HandleClickButton);
            SetSelected(false);
        }

        public void SetSelected(bool state)
        {
            selectionFrame.SetActiveOptimize(state);
            IsSelected = state;
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #region HANDLERS

        private void HandleClickButton()
        {
            OnClickAction?.Invoke();
        }

        #endregion

        #endregion
    }
}