namespace ElvisTracker {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.videoSourcePlayer = new Accord.Controls.VideoSourcePlayer();
      this.cameraSelectyCB = new System.Windows.Forms.ComboBox();
      this.startCaptureBTN = new System.Windows.Forms.Button();
      this.stopCaptureBTN = new System.Windows.Forms.Button();
      this.vidInfoLBL = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // videoSourcePlayer
      // 
      this.videoSourcePlayer.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.videoSourcePlayer.ForeColor = System.Drawing.Color.White;
      this.videoSourcePlayer.Location = new System.Drawing.Point(1, 3);
      this.videoSourcePlayer.Name = "videoSourcePlayer";
      this.videoSourcePlayer.Size = new System.Drawing.Size(511, 341);
      this.videoSourcePlayer.TabIndex = 1;
      this.videoSourcePlayer.VideoSource = null;
      this.videoSourcePlayer.NewFrame += new Accord.Controls.VideoSourcePlayer.NewFrameHandler(this.VideoSourcePlayer_NewFrame);
      // 
      // cameraSelectyCB
      // 
      this.cameraSelectyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cameraSelectyCB.FormattingEnabled = true;
      this.cameraSelectyCB.Location = new System.Drawing.Point(667, 3);
      this.cameraSelectyCB.Name = "cameraSelectyCB";
      this.cameraSelectyCB.Size = new System.Drawing.Size(121, 21);
      this.cameraSelectyCB.TabIndex = 2;
      // 
      // startCaptureBTN
      // 
      this.startCaptureBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.startCaptureBTN.Location = new System.Drawing.Point(619, 44);
      this.startCaptureBTN.Name = "startCaptureBTN";
      this.startCaptureBTN.Size = new System.Drawing.Size(75, 23);
      this.startCaptureBTN.TabIndex = 3;
      this.startCaptureBTN.Text = "Start";
      this.startCaptureBTN.UseVisualStyleBackColor = true;
      this.startCaptureBTN.Click += new System.EventHandler(this.StartCaptureBTN_Click);
      // 
      // stopCaptureBTN
      // 
      this.stopCaptureBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.stopCaptureBTN.Location = new System.Drawing.Point(713, 44);
      this.stopCaptureBTN.Name = "stopCaptureBTN";
      this.stopCaptureBTN.Size = new System.Drawing.Size(75, 23);
      this.stopCaptureBTN.TabIndex = 4;
      this.stopCaptureBTN.Text = "Stop";
      this.stopCaptureBTN.UseVisualStyleBackColor = true;
      this.stopCaptureBTN.Click += new System.EventHandler(this.StopCaptureBTN_Click);
      // 
      // vidInfoLBL
      // 
      this.vidInfoLBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.vidInfoLBL.AutoSize = true;
      this.vidInfoLBL.Location = new System.Drawing.Point(753, 88);
      this.vidInfoLBL.Name = "vidInfoLBL";
      this.vidInfoLBL.Size = new System.Drawing.Size(35, 13);
      this.vidInfoLBL.TabIndex = 5;
      this.vidInfoLBL.Text = "label1";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.vidInfoLBL);
      this.Controls.Add(this.stopCaptureBTN);
      this.Controls.Add(this.startCaptureBTN);
      this.Controls.Add(this.cameraSelectyCB);
      this.Controls.Add(this.videoSourcePlayer);
      this.Name = "MainForm";
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Accord.Controls.VideoSourcePlayer videoSourcePlayer;
    private System.Windows.Forms.ComboBox cameraSelectyCB;
    private System.Windows.Forms.Button startCaptureBTN;
    private System.Windows.Forms.Button stopCaptureBTN;
    private System.Windows.Forms.Label vidInfoLBL;
  }
}

