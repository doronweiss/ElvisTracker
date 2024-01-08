using Avalonia.Media;

namespace AvaloniaSea.Controls {
  public class ELCanvas : Avalonia.Controls.Canvas {
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    // public override void Render(DrawingContext dc) {
    //   base.Render(dc);
    //   OnImageRender?.Invoke(dc);
    // }
  }
}
