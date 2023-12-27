using System.Windows.Media;

namespace ElvisControls {
  public class ELCanvas : System.Windows.Controls.Canvas {
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    protected override void OnRender(DrawingContext dc) {
      base.OnRender(dc);
      OnImageRender?.Invoke(dc);
    }
  }
}
