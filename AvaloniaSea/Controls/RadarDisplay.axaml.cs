using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using System;
using Avalonia;

namespace AvaloniaSea.Controls {
  public partial class RadarDisplay : UserControl {
    private double cntrX, cntrY, radius;
    private double orientation = 0.0;
    private Bitmap? ship = null;

    public RadarDisplay() {
      InitializeComponent();
    }

    protected override void OnInitialized() {
      base.OnInitialized();
      Logger.TryGet(LogEventLevel.Debug, LogArea.Platform)?.Log(this, "Avalonia Infrastructure");
      System.Diagnostics.Trace.WriteLine("Initialized");
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
      ship = new Bitmap(AssetLoader.Open(new Uri("avares://AvaloniaSea/Assets/ship.png")));
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.Bounds.Width;
      double h = mainCanvas.Bounds.Height;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ? w / 2.0 : h / 2.0);
      radius -= 30;
      System.Diagnostics.Trace.WriteLine($"Size: {w}/{h}");
    }

    private void OnCanvasRendered(DrawingContext dc) {
      Pen p = new Pen(Brushes.GreenYellow, 2.0, null, PenLineCap.Flat, PenLineJoin.Miter, 10.0);
      Pen p2 = new Pen(Brushes.GreenYellow, 1.0, DashStyle.Dash, PenLineCap.Flat, PenLineJoin.Miter, 10.0);
      dc.DrawEllipse(null, p, new Avalonia.Point(cntrX, cntrY), radius, radius);
      dc.DrawEllipse(null, p2, new Avalonia.Point(cntrX, cntrY), radius*0.75, radius * 0.75);
      dc.DrawEllipse(null, p2, new Avalonia.Point(cntrX, cntrY), radius * 0.5, radius * 0.5);
      dc.DrawEllipse(null, p2, new Avalonia.Point(cntrX, cntrY), radius * 0.25, radius * 0.25);
      //dc.DrawImage(ship, new Rect(0, 0, 68, 355), new Rect(cntrX-10, cntrY-50, cntrX+10, cntrY+50));
      dc.DrawImage(ship, new Rect(cntrX-10, cntrY-50, 20, 100));
    }


  }
}
