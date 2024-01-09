using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using Avalonia;
using AvaloniaSea.Common;

namespace AvaloniaSea.Controls {
  public partial class RadarDisplay : UserControl {
    private const double D2R = Math.PI / 180.0;
    private const double R2D = 1.0 / D2R;
    private double cntrX, cntrY, radius;
    private Bitmap? ship = null;
    private List<Detection> detections = new List<Detection>();
    // pens
    Pen pThickY = new Pen(Brushes.Yellow, 2.0, null, PenLineCap.Flat, PenLineJoin.Miter, 10.0);
    Pen pThickGY = new Pen(Brushes.GreenYellow, 2.0, null, PenLineCap.Flat, PenLineJoin.Miter, 10.0);
    Pen pThinGY = new Pen(Brushes.GreenYellow, 1.0, DashStyle.Dash, PenLineCap.Flat, PenLineJoin.Miter, 10.0);
    Pen pThinGR = new Pen(Brushes.Red, 1.0, DashStyle.Dash, PenLineCap.Flat, PenLineJoin.Miter, 10.0);

    public static readonly StyledProperty<double> rotationProperty =
      AvaloniaProperty.Register<RadarDisplay, double>(nameof(Azimuth));

    public RadarDisplay() {
      InitializeComponent();
      AffectsRender<RadarDisplay>(rotationProperty);
    }

    protected override void OnInitialized() {
      base.OnInitialized();
      Logger.TryGet(LogEventLevel.Debug, LogArea.Platform)?.Log(this, "Avalonia Infrastructure");
      System.Diagnostics.Trace.WriteLine("Initialized");
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
      ship = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSea/Assets/ship.png")));
    }

    public double Azimuth {
      get => GetValue(rotationProperty);
      set => SetValue(rotationProperty, value);
    }

    public double MaxRange { get; set; } = 10000.0;

    public void UpdateDetections(List<Detection> detections) {
      this.detections = detections;
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.Bounds.Width;
      double h = mainCanvas.Bounds.Height;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ? w / 2.0 : h / 2.0);
      radius -= 30;
      System.Diagnostics.Trace.WriteLine($"Size: {w}/{h}");
    }

    private void OnCanvasRendered(DrawingContext dc) {
      // draw circles
      dc.DrawEllipse(null, pThickGY, new Avalonia.Point(cntrX, cntrY), radius, radius);
      dc.DrawEllipse(null, pThinGY, new Avalonia.Point(cntrX, cntrY), radius*0.75, radius * 0.75);
      dc.DrawEllipse(null, pThinGY, new Avalonia.Point(cntrX, cntrY), radius * 0.5, radius * 0.5);
      dc.DrawEllipse(null, pThinGY, new Avalonia.Point(cntrX, cntrY), radius * 0.25, radius * 0.25);
      // draw eadial lines
      double rot = -Azimuth*D2R;
      dc.DrawLine(pThickY, new Point(cntrX, cntrY), new Point(cntrX + radius * Math.Sin(rot), cntrY - radius * Math.Cos(rot)));
      rot += Math.PI / 2.0;
      dc.DrawLine(pThinGY, new Point(cntrX, cntrY), new Point(cntrX + radius * Math.Sin(rot), cntrY - radius * Math.Cos(rot)));
      rot += Math.PI / 2.0;
      dc.DrawLine(pThinGY, new Point(cntrX, cntrY), new Point(cntrX + radius * Math.Sin(rot), cntrY - radius * Math.Cos(rot)));
      rot += Math.PI / 2.0;
      dc.DrawLine(pThinGY, new Point(cntrX, cntrY), new Point(cntrX + radius * Math.Sin(rot), cntrY - radius * Math.Cos(rot)));
      // draw the ship image
      dc.DrawImage(ship, new Rect(cntrX-10, cntrY-50, 20, 100));
      // draw detection
      double px2m = radius / MaxRange;
      foreach (Detection dt in detections) {
        double detAng = (dt.azimuth - Azimuth) * D2R;
        double detRad = dt.range * px2m;
      }
    }


  }
}
