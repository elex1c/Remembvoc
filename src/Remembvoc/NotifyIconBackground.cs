using System.Drawing;
using System.Windows;
using Forms = System.Windows.Forms;

namespace Remembvoc;

public class NotifyIconBackground : IDisposable
{
    private readonly Window _window;

    public bool IsClosedByBackground;
    private Forms.NotifyIcon _trayIcon;
    private Forms.ContextMenuStrip  _trayMenu;
    
    public NotifyIconBackground()
    {
        CreateTrayIcon();
    }
    
    private void CreateTrayIcon()
    {
        _trayMenu = new Forms.ContextMenuStrip();
        
        _trayMenu.Items.Add("Open",null, OnOpen);
        _trayMenu.Items.Add("Exit", null, OnExit);

        _trayIcon = new Forms.NotifyIcon
        {
            Icon = new Icon("Icons/Error.ico"),
            ContextMenuStrip = _trayMenu,
            Visible = true
        };

        _trayIcon.MouseClick += (_, args) =>
        {
            if (args.Button == Forms.MouseButtons.Left)
            {
                ShowWindow();
            }
        };
    }

    private void ShowWindow()
    {
        if (_window.WindowState == WindowState.Minimized)
        {
            _window.WindowState = WindowState.Normal;
        }
        _window.Activate();
    }
    
    private void OnOpen(object? sender, EventArgs e)
    {
        ShowWindow();
    }

    private void OnExit(object? sender, EventArgs e)
    {
        _trayIcon.Visible = false;
        IsClosedByBackground = true;
        Application.Current.Shutdown();
    }
    
    public void Dispose()
    {
        _trayIcon.Dispose();
        _trayMenu.Dispose();
    }
}