using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Accord.Video;
using Accord.Video.DirectShow;
using ElvisAnalyzer.VideoUtils;
using Brushes = System.Drawing.Brushes;

namespace ElvisAnalyzer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    FilterInfoCollection videoDevices;
    VideoCaptureDevice camera;
    VidFileWriter vfw = new VidFileWriter(false);
    List<System.Windows.Rect> rects = new List<System.Windows.Rect>();

    public MainWindow() {
      InitializeComponent();
    }

    private void OnWindowLoded(object sender, RoutedEventArgs e) {
      // show device list
      try {
        // enumerate video devices
        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0)
          throw new Exception();
        for (int i = 1, n = videoDevices.Count; i <= n; i++) {
          string cameraName = i + " : " + videoDevices[i - 1].Name;
          cameraSelectyCB.Items.Add(cameraName);
        }
      } catch {
        startCaptureBTN.IsEnabled = false;
        cameraSelectyCB.Items.Add("No cameras found");
        cameraSelectyCB.SelectedIndex = 0;
        cameraSelectyCB.IsEnabled = false;
      }
      // create rects
      System.Random r = new System.Random();
      for (int idx = 0; idx < 10; idx++) {
        int x = r.Next(200);
        int y = r.Next(200);
        int w = r.Next(100);
        int h = r.Next(100);
        rects.Add(new Rect(new System.Windows.Point(x,y), new System.Windows.Size(w,h)));
      }
      // add render handler
      frameHolder.OnImageRender += new ELImage.ImageRenderDelegate(this.OnImageRendered);
    }

    private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
      // remove render handler
      frameHolder.OnImageRender -= this.OnImageRendered;
      StopCameras();
      vfw?.CloseVidFile();
    }

    void OnImageRendered(DrawingContext dc) {
      counter++;
      foreach (var r in rects) {
        dc.DrawRectangle(System.Windows.Media.Brushes.Red, null, r);
      }
    }

    private DateTime startTime;
    private int counter = 0;
    private void OnStartBtnClick(object sender, RoutedEventArgs e) {
      StartCameras();
      startCaptureBTN.IsEnabled = false;
      stopCaptureBTN.IsEnabled = true;
      //
      counter = 0;
      startTime = DateTime.Now;
    }

    private void OnStopBtnClick(object sender, RoutedEventArgs e) {
      StopCameras();
      startCaptureBTN.IsEnabled = true;
      stopCaptureBTN.IsEnabled = false;
      //
      double secs = (DateTime.Now - startTime).TotalSeconds;
      MessageBox.Show($"Frame rate: {counter / secs}");
    }

    // Start cameras
    private void StartCameras() {
      if (camera == null) {
        if (cameraSelectyCB.SelectedIndex < 0)
          return;
        // create first video source
        camera = new VideoCaptureDevice(videoDevices[cameraSelectyCB.SelectedIndex].MonikerString);
        if (camera == null) {
          MessageBox.Show("Failed creating camera");
          return;
        }
        camera.NewFrame += new NewFrameEventHandler(OnNewFrame);
      }
      camera.Start();
      cameraSelectyCB.IsEnabled = false;
    }

    // Stop cameras
    private void StopCameras() {
      camera?.Stop();
    }


    private bool firstStart = true;
    void OnNewFrame(object sender, NewFrameEventArgs eventArgs) {
      System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();
      if (firstStart) {
        DateTime dt = DateTime.Now;
        string fname = $"{dt.Year:D4}{dt.Month:D2}{dt.Day:D2}{dt.Hour:D2}{dt.Minute:D2}{dt.Second:D2}";
        VideoCapabilities vc = camera.VideoCapabilities.FirstOrDefault(x => x.FrameSize.Width == img.Width && x.FrameSize.Height == img.Height);
        if (vc == null || !vfw.MakeVidFile(fname, img.Width, img.Height, vc.AverageFrameRate)) {
          StopCameras();
          return;
        }
        firstStart = false;
        return;
      }
      try {
        MemoryStream ms = new MemoryStream();
        img.Save(ms, ImageFormat.Bmp);
        ms.Seek(0, SeekOrigin.Begin);
        BitmapImage bi = new BitmapImage();
        bi.BeginInit();
        bi.StreamSource = ms;
        bi.EndInit();

        bi.Freeze();
        Dispatcher.BeginInvoke(new ThreadStart(delegate
        {
          frameHolder.Source = bi;
        }));
        vfw?.AddFrame(img);
      } catch (Exception ex) {
      }
    }
  }
}
