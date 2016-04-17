﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using log4net;
using Application = System.Windows.Application;

namespace meteosat.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));
        public NotifyIcon NotifyIcon { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                if (NotifyIcon == null) return;
                NotifyIcon.Visible = false;
                //NotifyIcon.Icon = null;
                NotifyIcon.Dispose();
                NotifyIcon = null;
            }
            catch (Exception)
            {
                // handle the error
            }
        }

        private void InitializeNotifyIcon()
        {
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add("Show", (s, e) => ShowWindow());
            contextMenu.MenuItems.Add("Hide", (s, e) => this.Hide());
            contextMenu.MenuItems.Add("Exit", (s, e) => Application.Current.Shutdown(0));

            NotifyIcon = new NotifyIcon
            {
                Icon = (Icon)Properties.Resources.ResourceManager.GetObject("TrayIcon"),
                Visible = true,
                ContextMenu = contextMenu
            };
            NotifyIcon.DoubleClick += (s, e) => ShowWindow();
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }

            base.OnStateChanged(e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
