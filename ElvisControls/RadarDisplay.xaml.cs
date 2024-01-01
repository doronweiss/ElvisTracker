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
    // radar circle size data
    private double cntrX, cntrY, radius;
    private double upAzimuth = 0.0;

    public RadarDisplay() {
      InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
    }

    private void OnControlUnLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender -= this.OnCanvasRendered;
    }

    public double UpAzimut{
      get => upAzimuth;
      set {
        upAzimuth = value;
        OnCanvasSizeChanged(mainCanvas, null);
      }
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.ActualWidth;
      double h = mainCanvas.ActualHeight;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ? w / 2.0 : h / 2.0);
      radius -= 30;
      Canvas.SetLeft(radrCirc, cntrX - radius);
      Canvas.SetTop(radrCirc, cntrY - radius);
      radrCirc.Width = radius * 2;
      radrCirc.Height = radius * 2;
      // position arrows
      // north
      Canvas.SetLeft(arrowN, cntrX - 10);
      Canvas.SetTop(arrowN, cntrY - radius - 30);
      RotateArrow(arrowE, -upAzimuth);
      // north
      Canvas.SetLeft(arrowE, cntrX +radius);
      Canvas.SetTop(arrowE, cntrY+15);
      RotateArrow(arrowE, 90 - upAzimuth);
      // north
      Canvas.SetLeft(arrowS, cntrX - 10);
      Canvas.SetTop(arrowS, cntrY + radius);
      RotateArrow(arrowS, 180 - upAzimuth);
      // north
      Canvas.SetLeft(arrowW, cntrX - radius);
      Canvas.SetTop(arrowW, cntrY+15);
      RotateArrow(arrowW, -90 - upAzimuth);
    }

    private void RotateArrow(DirectionArrow arrow, double angle) {
      RotateTransform rotateTransform = new RotateTransform(angle);
      arrow.RenderTransform = rotateTransform;
    }

    void OnCanvasRendered(DrawingContext dc) {
      // Pen p = new Pen(circleColor, 5);
      // dc.DrawEllipse(null, p, new Point( cntrX, cntrY), radius, radius);
    }

  }
}
