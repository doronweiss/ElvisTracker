using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Canvas = System.Windows.Controls.Canvas;

namespace ElvisOnRadar.UserControls {
  /// <summary>
  /// Interaction logic for RadarDisplay.xaml
  /// </summary>
  public partial class RadarDisplay : UserControl {
    #region dependency properties
    public static readonly DependencyProperty circleColorPRoperty = DependencyProperty.Register(
      "circleColorDP", typeof(SolidColorBrush), typeof(RadarDisplay), new PropertyMetadata(Brushes.Blue));

    public SolidColorBrush circleColor {
      get => (SolidColorBrush)GetValue(circleColorPRoperty);
      set => SetValue(circleColorPRoperty, value);
    }
    #endregion dependency properties
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    public RadarDisplay() {
      InitializeComponent();
    }

    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      //Canvas.Left = "0" Canvas.Top = "0" Canvas.Right = "0" Canvas.Bottom = "0"
      // double w = mainCanvas.ActualWidth;
      // double h = mainCanvas.ActualHeight;
      // Canvas.SetLeft(radarCirc, 0.0);
      // Canvas.SetTop(radarCirc, 0.0);
      // Canvas.SetRight(radarCirc, w);
      // Canvas.SetBottom(radarCirc, h);
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
    }

    private void OnControlUnLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender -= this.OnCanvasRendered;
    }

    void OnCanvasRendered(DrawingContext dc) {
      // counter++;
      // foreach (var r in rects) {
      //   dc.DrawRectangle(System.Windows.Media.Brushes.Red, null, r);
      // }
      double w = mainCanvas.ActualWidth;
      double h = mainCanvas.ActualHeight;
      Pen p = new Pen(Brushes.Black, 5);
      dc.DrawEllipse(null, p, new Point( w/2.0, h/2.0), w / 2.0, h / 2.0);
    }

  }
}
