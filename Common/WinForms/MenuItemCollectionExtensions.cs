using System.Windows.Forms;

namespace Common.WinForms
{
    public static class MenuItemCollectionExtensions
    {
        public static void AddDelimiter(this Menu.MenuItemCollection menuItemCollections)
        {
            menuItemCollections.Add("-");
        }
    }
}
