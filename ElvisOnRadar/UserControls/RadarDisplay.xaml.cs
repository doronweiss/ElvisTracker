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
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    public RadarDisplay() {
      InitializeComponent();
    }

    // radar circle size data
    private double cntrX, cntrY, radius;
    private void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e) {
      double w = mainCanvas.ActualWidth;
      double h = mainCanvas.ActualHeight;
      (cntrX, cntrY, radius) = (w / 2.0, h / 2.0, w <= h ?  w / 2.0 : h / 2.0);
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender += new ELCanvas.ImageRenderDelegate(this.OnCanvasRendered);
    }

    private void OnControlUnLoaded(object sender, RoutedEventArgs e) {
      mainCanvas.OnImageRender -= this.OnCanvasRendered;
    }

    void OnCanvasRendered(DrawingContext dc) {
      Pen p = new Pen(circleColor, 5);
      dc.DrawEllipse(null, p, new Point( cntrX, cntrY), radius, radius);
    }

  }
}
