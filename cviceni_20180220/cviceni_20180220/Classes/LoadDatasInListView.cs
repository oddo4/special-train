using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_20180220.Classes
{
    public class LoadDatasInListView
    {
        public ObservableCollection<Item> GetItems(int type)
        {
            var result = new ObservableCollection<Item>();
            var loadedData = App.Database.GetItemByTypeSync(type);

            foreach (Item item in loadedData)
            {
                var transaction = App.Database.GetTransaction(item.ID);
                var debt = App.Database.GetDebt(transaction.ID);

                var tie = App.Database.GetItemTiesSync().Where(i => i.IDItem == item.ID).First();
                var itemListName = App.Database.GetItemsList(tie.IDItemsList);

                item.ListName = itemListName.Name;
                item.FormattedDate = transaction.DateTransaction.ToString("dd/MM/yyyy");

                if (debt != null)
                {
                    if (debt.RaiseCounter > 0)
                    {
                        item.Cost += (item.Cost / debt.RaisePercentage) * debt.RaiseCounter;
                    }
                }

                result.Add(item);
            }

            return result;
        }
        public void DeleteItem(ObservableCollection<Item> items, int index)
        {
            var item = items[index];
            App.Database.DeleteItem(item);
            items.Remove(item);
        }
        public ObservableCollection<ItemsList> GetItemsLists(int type)
        {
            var result = new ObservableCollection<ItemsList>();
            var loadedData = App.Database.GetItemsLists(type);

            foreach (ItemsList iList in loadedData)
            {
                var items = App.Database.GetItemsSync(iList.ID);
                foreach (Item item in items)
                {
                    iList.CostSum += item.Cost;
                }
                result.Add(iList);
            }
            return result;
        }

    }
}
