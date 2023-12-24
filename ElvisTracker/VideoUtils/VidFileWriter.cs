using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;

namespace ElvisTracker.VideoUtils {
  class VidFileWriter {
    private int frameIdx = 0;
    VideoFileWriter vfw = new VideoFileWriter();
    private bool actuallyWrite = true;

    public VidFileWriter(bool actuallyWrite = true) {
      this.actuallyWrite = actuallyWrite;
    }

    public bool MakeVidFile(string fileName, int width, int height) {
      if (! actuallyWrite)
        return true;
      try {
        vfw = new VideoFileWriter();
        vfw.Open(fileName, width, height);
      } catch (Exception ex) {
        return false;
      }
      return true;
    }

    public void CloseVidFile() {
      if (!actuallyWrite)
        return;
      vfw?.Close();
      System.Diagnostics.Debug.WriteLine($"Wrote total of {frameIdx} frames");
    }

    public void AddFrame(Bitmap frame) {
      if (!actuallyWrite)
        return;
      frameIdx++;
      vfw?.WriteVideoFrame(frame);
    }
  }
}
