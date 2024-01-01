using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ElvisControls {
  /// <summary>
  /// Interaction logic for DirectionArrow.xaml
  /// </summary>
  public partial class DirectionArrow : UserControl {
    #region dependency properties
    public static readonly DependencyProperty arrowColorProperty = DependencyProperty.Register(
      "arrowColorDP", typeof(SolidColorBrush), typeof(DirectionArrow), new PropertyMetadata(Brushes.Yellow));

    public SolidColorBrush arrowColor {
      get => (SolidColorBrush)GetValue(arrowColorProperty);
      set => SetValue(arrowColorProperty, value);
    }
    #endregion dependency properties

    public DirectionArrow() {
      InitializeComponent();
    }
  }
}
