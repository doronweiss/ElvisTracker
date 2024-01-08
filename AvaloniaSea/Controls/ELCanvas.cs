using Avalonia.Media;

namespace AvaloniaSea.Controls {
  public class ELCanvas : Avalonia.Controls.Control {
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    public override void Render(DrawingContext drawingContext) {
      base.Render(drawingContext);
      OnImageRender?.Invoke(drawingContext);
    }
  }
}
