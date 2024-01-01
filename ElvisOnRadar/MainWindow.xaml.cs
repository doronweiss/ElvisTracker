using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ElvisOnRadar {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    DispatcherTimer timer = new DispatcherTimer();
    public MainWindow() {
      InitializeComponent();
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e) {

    }

    private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e) {

    }

    private double angle = 0.0;
    private void OnRun(object sender, RoutedEventArgs e) {
      timer.Interval = TimeSpan.FromSeconds(0.1);
      timer.Tick += new EventHandler(OnTicket);
      timer.IsEnabled = true;
    }

    private void OnStop(object sender, RoutedEventArgs e) {
      timer.IsEnabled = false;
      timer.Tick -= OnTicket;
    }

    private void OnTicket(object sender, EventArgs e) {
      angle += 1.0;
      radar.UpAzimut = angle % 360.0;
    }

  }
}