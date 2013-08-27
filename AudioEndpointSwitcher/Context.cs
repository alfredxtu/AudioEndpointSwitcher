using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.WinForms;

namespace AudioEndpointSwitcher
{
    public class CContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private MenuItem _exitItem;
        CAudioEndpointsPolicy _policy = new CAudioEndpointsPolicy();

        public CContext()
        {
            _exitItem = new MenuItem("Exit", OnExitSelected);

            _trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources._16x16,
                ContextMenu = new ContextMenu(new MenuItem[] { _exitItem }),
                Visible = true
            };
            _trayIcon.ContextMenu.Popup += OnContextMenuPopup;
        }

        private void OnContextMenuPopup(object sender, EventArgs e)
        {
            if (!_trayIcon.ContextMenu.MenuItems.IsReadOnly)
                PopulateContextMenu(_trayIcon.ContextMenu.MenuItems);
        }

        private void PopulateContextMenu(Menu.MenuItemCollection menuItemCollection)
        {
            menuItemCollection.Clear();
            List<MenuItem> newItems = new List<MenuItem>();
            try
            {
                CAudioEndpointInfo defaultEndpoint = _policy.Default;
                foreach (var endpoint in _policy.Endpoints)
                {
                    var menuItem = new MenuItem(endpoint.FriendlyName, OnDefaultEnpointSelected);
                    menuItem.Tag = endpoint;
                    if (endpoint.Id == defaultEndpoint.Id)
                        menuItem.Checked = true;
                    newItems.Add(menuItem);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(String.Format("Failed to enumerate audio endpoints. Error:\n{0}", exc.Message), "Failed to enumerate endpoints", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            menuItemCollection.AddRange(newItems.ToArray());
            menuItemCollection.AddDelimiter();
            menuItemCollection.Add(_exitItem);
        }

        void OnDefaultEnpointSelected(object sender, EventArgs e)
        {
            try
            {
                var endpoint = (CAudioEndpointInfo)((MenuItem)sender).Tag;
                if (_policy.Default.Id != endpoint.Id)
                    _policy.Default = endpoint;
            }
            catch (Exception exc)
            {
                MessageBox.Show(String.Format("Failed to update default audio endpoint. Error:\n{0}", exc.Message), "Failed to set endpoint", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void OnExitSelected(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
