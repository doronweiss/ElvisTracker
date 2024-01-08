using System;
using Avalonia.Controls;
using Avalonia.Threading;

namespace AvaloniaSea;

public partial class MainWindow : Window {
  private double azimuth = 0.0;
  private DispatcherTimer? timer;

  public MainWindow() {
    InitializeComponent();
  }

  public void OnWindowOpened(object? sender, EventArgs eventArgs) {
    timer =
      new DispatcherTimer(
        TimeSpan.FromMilliseconds(100),
        DispatcherPriority.Render,
        (o, args) => {
          azimuth += 10.0;
          radar.Azimuth = azimuth;
        });
    timer.Start();
  }

  public void OnWindowClosing(object? sender, EventArgs eventArgs) {
    timer?.Stop();
  }
}
