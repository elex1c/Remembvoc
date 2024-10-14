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
        
        _trayIcon.MouseClick += (_, args) =>
        {
            GetCursorPos(out var cursorPosition);

            // Show the context menu near the system tray icon
            _trayMenu.Show(cursorPosition.X, cursorPosition.Y);
        };
        
        /*
         How to show notification
         
         _trayIcon.BalloonTipTitle = "Hello!";
        _trayIcon.BalloonTipText = "This is a system tray notification.";
        _trayIcon.BalloonTipIcon = Forms.ToolTipIcon.Info; // Can be Info, Warning, Error
        _trayIcon.ShowBalloonTip(3000); // Show notification for 3 seconds
        
        */
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
    
    public struct POINT
    {
        public int X;
        public int Y;
    }
}