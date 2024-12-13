using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using Forms = System.Windows.Forms;

namespace Remembvoc;

public class NotifyIconBackground : IDisposable
{
    private MainWindow? _window;

    private Forms.NotifyIcon _trayIcon;
    private Forms.ContextMenuStrip  _trayMenu;

    public NotifyIconBackground() => CreateTrayIcon();

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);
    
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

        _trayIcon.BalloonTipTitle = "You have words to revise!";
        _trayIcon.BalloonTipText = "See them in 'Translate' section";

        _trayIcon.BalloonTipClicked += OnOpen; 
        
        _trayIcon.MouseClick += (_, args) =>
        {
            GetCursorPos(out var cursorPosition);

            // Show the context menu near the system tray icon
            _trayMenu.Show(cursorPosition.X, cursorPosition.Y);
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

    /// <summary>
    /// Shows ballon tip notification in set interval.
    /// </summary>
    /// <param name="interval">In milliseconds</param>
    public void ShowNotification(int interval)
    {
        _trayIcon.ShowBalloonTip(interval);
    }
    
    private void OnOpen(object? sender, EventArgs e)
    {
        _window ??= new MainWindow
        {
            Visibility = Visibility.Visible
        };

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

    private struct POINT
    {
        public int X;
        public int Y;
    }
}