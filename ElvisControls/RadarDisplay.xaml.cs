using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Canvas = System.Windows.Controls.Canvas;

namespace ElvisControls {
  /// <summary>
  /// Interaction logic for RadarDisplay.xaml
  /// </summary>
  public partial class RadarDisplay : UserControl {
    #region dependency properties
    public static readonly DependencyProperty circleColorPRoperty = DependencyProperty.Register(
      "circleColorDP", typeof(SolidColorBrush), typeof(RadarDisplay), new PropertyMetadata(Brushes.Yellow));
    public static readonly DependencyProperty backgroundColorPRoperty = DependencyProperty.Register(
      "backgroundColorDP", typeof(SolidColorBrush), typeof(RadarDisplay), new PropertyMetadata(Brushes.Black));

    public SolidColorBrush circleColor {
      get => (SolidColorBrush)GetValue(circleColorPRoperty);
      set => SetValue(circleColorPRoperty, value);
    }
    public SolidColorBrush backgroundColor {
      get => (SolidColorBrush)GetValue(backgroundColorPRoperty);
      set => SetValue(backgroundColorPRoperty, value);
    }
    #endregion dependency properties
    // callback for the application to draw data
    // public delegate void ImageRenderDelegate(DrawingContext dc);
    // public ImageRenderDelegate OnImageRender;

    public RadarDisplay() {
      InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
    }

    private void OnControlUnLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender -= this.OnCanvasRendered;
    }

    // radar circle size data
    private double cntrX, cntrY, radius;

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.ActualWidth;
      double h = mainCanvas.ActualHeight;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ? w / 2.0 : h / 2.0);
    }

    void OnCanvasRendered(DrawingContext dc) {
      Pen p = new Pen(circleColor, 5);
      dc.DrawEllipse(null, p, new Point( cntrX, cntrY), radius, radius);
    }

  }
}
