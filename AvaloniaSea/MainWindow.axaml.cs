using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;
using AvaloniaSea.Common;

namespace AvaloniaSea;

public partial class MainWindow : Window {
  private double azimuth = 0.0;
  private DispatcherTimer? timer;
  private List<Detection> detections = new List<Detection>();


  public MainWindow() {
    InitializeComponent();
    detections.Add(new Detection() {range = 1000, azimuth = 0, descrition = "1"});
    detections.Add(new Detection() {range = 2000, azimuth = 30, descrition = "22"});
    detections.Add(new Detection() {range = 3000, azimuth = 45, descrition = "333"});
    detections.Add(new Detection() {range = 4000, azimuth = 135, descrition = "4444"});
    detections.Add(new Detection() {range = 75000, azimuth = 270, descrition ="55555"});
  }

  public void OnWindowOpened(object? sender, EventArgs eventArgs) {
    timer =
      new DispatcherTimer(
        TimeSpan.FromMilliseconds(1000),
        DispatcherPriority.Render,
        OnTimer);
        // (o, args) => {
        //   azimuth += 10.0;
        //   radar.Azimuth = azimuth;
        // });
    timer.Start();
  }

  public void OnWindowClosing(object? sender, EventArgs eventArgs) {
    timer?.Stop();
  }

  void OnTimer(object sender, EventArgs args) {
    foreach (Detection dt in detections)
      dt.azimuth = (dt.azimuth + 15) % 360;
    radar.UpdateDetections(detections);
  }
}
