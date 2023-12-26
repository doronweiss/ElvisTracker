using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace ElvisAnalyzer {
  public class ELImage : System.Windows.Controls.Image {
    public delegate void ImageRenderDelegate (DrawingContext dc);
    public ImageRenderDelegate OnImageRender;

    protected override void OnRender(DrawingContext dc) {
      base.OnRender(dc);
      OnImageRender?.Invoke(dc);
    }
  }
}
