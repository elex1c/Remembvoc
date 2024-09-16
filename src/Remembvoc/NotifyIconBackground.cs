using System.Drawing;
using System.Windows;
using Forms = System.Windows.Forms;

namespace Remembvoc;

public class NotifyIconBackground : IDisposable
{
    private MainWindow? _window;

    private Forms.NotifyIcon _trayIcon;
    private Forms.ContextMenuStrip  _trayMenu;

    public NotifyIconBackground() => CreateTrayIcon();

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
        if (_window == null) return;

        if (_window.WindowState == WindowState.Minimized)
        {
            _window.WindowState = WindowState.Normal;
        }
        _window.Activate();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        if (_window == null)
        {
            _window = new MainWindow();
            _window.Visibility = Visibility.Visible;
        }

        ShowWindow();
    }

    private void OnExit(object? sender, EventArgs e)
    {
        _trayIcon.Visible = false;
        Application.Current.Shutdown();
    }
    
    public void SetWindow(MainWindow? window) => _window = window;

    public void Dispose()
    {
        _trayIcon.Dispose();
        _trayMenu.Dispose();
    }
}