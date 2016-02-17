

using Inventory.Data.Repository;

namespace InventoryWcfService.Event
{
    public static class EventManager 
    {

        public static void AddNotification(string message)
        {
            new LogRepository().Add(message);
        }

  

    }
}