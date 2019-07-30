using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.DirectShow;

namespace ElvisTracker {
  public partial class MainForm : Form {
    FilterInfoCollection videoDevices;

    public MainForm() {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e) {
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
        startCaptureBTN.Enabled = false;
        cameraSelectyCB.Items.Add("No cameras found");
        cameraSelectyCB.SelectedIndex = 0;
        cameraSelectyCB.Enabled = false;
      }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {

    }

    private void StartCaptureBTN_Click(object sender, EventArgs e) {
      StartCameras();
      startCaptureBTN.Enabled = false;
      stopCaptureBTN.Enabled = true;
    }

    private void StopCaptureBTN_Click(object sender, EventArgs e) {
      StopCameras();
      startCaptureBTN.Enabled = true;
      stopCaptureBTN.Enabled = false;
    }


    // Start cameras
    private void StartCameras() {
      if (cameraSelectyCB.SelectedIndex < 0)
        return;
      // create first video source
      var videoSource = new VideoCaptureDevice(videoDevices[cameraSelectyCB.SelectedIndex].MonikerString);
      // videoSource1.DesiredFrameRate = 10;
      videoSourcePlayer.VideoSource = videoSource;
      videoSourcePlayer.Start();
    }

    // Stop cameras
    private void StopCameras() {
      videoSourcePlayer.SignalToStop();
      videoSourcePlayer.WaitForStop();
    }

  }
}
