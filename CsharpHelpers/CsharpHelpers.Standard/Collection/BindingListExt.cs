using System.Collections.Generic;
using System.ComponentModel;

namespace CsharpHelpers.Standard.Collection
{
    public class BindingListExt<T> : BindingList<T>
    {
        protected override void RemoveItem(int itemIndex)
        {
            //itemIndex = index of item which is going to be removed
            //get item from binding list at itemIndex position
            T deletedItem = Items[itemIndex];

            //raise event containing item which is going to be removed
            BeforeRemove?.Invoke(deletedItem);

            //remove item from list
            base.RemoveItem(itemIndex);
        }

        public delegate void BeforeRemoveDelegate(T deletedItem);
        public event BeforeRemoveDelegate BeforeRemove;

        public BindingListExt()
        {
            
        }

        public BindingListExt(IList<T> list) : base(list)
        {

        }
    }
}