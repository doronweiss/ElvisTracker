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

namespace ElvisAnalyzer {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    FilterInfoCollection videoDevices;
    VideoCaptureDevice camera;

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
    }

    private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {

    }

    private bool firstStart = true;
    private void OnStartBtnClick(object sender, RoutedEventArgs e) {

    }

    private void OnStopBtnClick(object sender, RoutedEventArgs e) {

    }

    // Start cameras
    private void StartCameras() {
      if (cameraSelectyCB.SelectedIndex < 0)
        return;
      // create first video source
      camera = new VideoCaptureDevice(videoDevices[cameraSelectyCB.SelectedIndex].MonikerString);
      camera.NewFrame += new NewFrameEventHandler(OnNewFrame);
      camera.Start();
    }

    // Stop cameras
    private void StopCameras() {
      camera.Stop();
    }


    void OnNewFrame(object sender, NewFrameEventArgs eventArgs) {
      try {
        System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

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
      } catch (Exception ex) {
      }
    }
  }
}
