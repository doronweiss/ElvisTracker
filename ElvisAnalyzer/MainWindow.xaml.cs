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

namespace ElvisAnalyzer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    FilterInfoCollection videoDevices;
    VideoCaptureDevice camera;
    VidFileWriter vfw = new VidFileWriter();

    public MainWindow() {
      InitializeComponent();
    }

    private void OnWindowLoded(object sender, RoutedEventArgs e) {
      // show device list
      try {
        // enumerate video devices
        videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0) {
          MessageBox.Show("No cameras");
          Close();
          return;
        }
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
    }

    private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
      StopCameras();
      vfw?.CloseVidFile();
    }

    private void OnStartBtnClick(object sender, RoutedEventArgs e) {
      StartCameras();
      startCaptureBTN.IsEnabled = false;
      stopCaptureBTN.IsEnabled = true;
    }

    private void OnStopBtnClick(object sender, RoutedEventArgs e) {
      StopCameras();
      startCaptureBTN.IsEnabled = true;
      stopCaptureBTN.IsEnabled = false;
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
