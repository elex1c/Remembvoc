using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Forms = System.Windows.Forms;

namespace Remembvoc.UI;

public class NotifyIconBackground : INotificationIcon, IDisposable
{
    private readonly IWordService _wordService;
    private readonly IPaginationService _paginationService;
    
    private MainWindow? _mainWindow;
    private Forms.NotifyIcon _trayIcon;
    private Forms.ContextMenuStrip  _trayMenu;

    public NotifyIconBackground(MainWindow mainWindow,
        IWordService wordService,
        IPaginationService paginationService)
    {
        _mainWindow = mainWindow;
        _wordService = wordService;
        _paginationService = paginationService;

        CreateTrayIcon();
    } 
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
        if (_mainWindow == null) return;

        if (_mainWindow.WindowState == WindowState.Minimized)
        {
            _mainWindow.WindowState = WindowState.Normal;
        }
        _mainWindow.Activate();
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
        _mainWindow ??= new MainWindow(this, _wordService, _paginationService)
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
    
    public void SetWindow(object? window) => _mainWindow = (MainWindow?)window;

    private struct POINT
    {
        public int X;
        public int Y;
    }

    public void Dispose()
    {
        _trayIcon.Dispose();
        _trayMenu.Dispose();
    }
}