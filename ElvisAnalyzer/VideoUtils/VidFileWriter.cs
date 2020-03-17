using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;

namespace ElvisAnalyzer.VideoUtils {
  class VidFileWriter {
    private int frameIdx = 0;
    VideoFileWriter vfw = new VideoFileWriter();

    public bool MakeVidFile(string fileName, int width, int height, int frameRate) {
      try {
        vfw = new VideoFileWriter();
        vfw.Open(fileName, width, height, frameRate, VideoCodec.MSMPEG4v3);
      } catch (Exception ex) {
        return false;
      }
      return true;
    }

    public void CloseVidFile() {
      vfw?.Close();
      System.Diagnostics.Debug.WriteLine($"Wrote total of {frameIdx} frames");
    }

    public void AddFrame(Image img) {
      AddFrame((Bitmap) img);
    }

    RectangleF rectf = new RectangleF(640, 10, 120, 30);
    private Font dtFont = new Font("Tahoma", 18);
    public void AddFrame(Bitmap frame) {
      frameIdx++;
      DateTime dt = DateTime.Now;
      Graphics g = Graphics.FromImage(frame);
      // g.SmoothingMode = SmoothingMode.AntiAlias;
      // g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      // g.PixelOffsetMode = PixelOffsetMode.HighQuality;
      g.DrawString(dt.ToLongTimeString(), dtFont, Brushes.Black, rectf);
      g.Flush();      
      vfw?.WriteVideoFrame(frame);
    }
  }
}
