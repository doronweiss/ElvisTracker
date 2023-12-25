using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ElvisOnRadar.UserControls {
  public class ELCanvas : System.Windows.Controls.Canvas {
    public delegate void ImageRenderDelegate(DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    protected override void OnRender(DrawingContext dc) {
      base.OnRender(dc);
      OnImageRender?.Invoke(dc);
    }
  }
}
