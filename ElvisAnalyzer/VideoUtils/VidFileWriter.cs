﻿using System;
using System.Collections.Generic;
using System.Drawing;
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

    public void AddFrame(Bitmap frame) {
      frameIdx++;
      vfw?.WriteVideoFrame(frame);
    }
  }
}
