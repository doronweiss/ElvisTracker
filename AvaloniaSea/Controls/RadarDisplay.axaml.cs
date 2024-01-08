using Avalonia.Controls;
using Avalonia.Logging;

namespace AvaloniaSea.Controls {
  public partial class RadarDisplay : UserControl {
    private double cntrX, cntrY, radius;

    public RadarDisplay() {
      InitializeComponent();
    }

    protected override void OnInitialized() {
      base.OnInitialized();
      Logger.TryGet(LogEventLevel.Debug, LogArea.Platform)?.Log(this, "Avalonia Infrastructure");
      System.Diagnostics.Trace.WriteLine("Initialized");
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.Bounds.Width;
      double h = mainCanvas.Bounds.Height;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ? w / 2.0 : h / 2.0);
      radius -= 30;
      System.Diagnostics.Trace.WriteLine($"Size: {w}/{h}");
    }

  }
}
