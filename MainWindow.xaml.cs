using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace StopWatch
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly DispatcherTimer timer = new();
    private readonly Stopwatch stopWatch = new();
    private string currentTime = "00::00::00";

    public MainWindow()
    {
      InitializeComponent();

      timer.Tick += Timer_Tick;
      timer.Interval = new TimeSpan(0, 0, 1);
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
      if (stopWatch.IsRunning)
      {
        TimeSpan timeSpan = stopWatch.Elapsed;
        currentTime = $"{timeSpan.Hours:00} :: {timeSpan.Minutes:00} :: {timeSpan.Seconds:00}";
        DisplayTime.Text = currentTime;
      }
    }

    private void StopWatch()
    {
      stopWatch.Stop();
      if (currentTime.Length > 0)
      {
        ElapsedItems.Items.Insert(0, currentTime);
        currentTime = "";
      }
      Start.Content = "Start";
      Start.IsEnabled = true;
      Stop.IsEnabled = false;
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
      StopWatch();

      stopWatch.Reset();
      DisplayTime.Text = currentTime = "00 :: 00 :: 00";
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
      if (stopWatch.IsRunning)
      {
        StopWatch();
      }
      else
      {
        stopWatch.Start();
        timer.Start();
        Start.Content = "Stop";
        Start.Background = Brushes.Red;
        Start.IsEnabled = false;
        Stop.IsEnabled = true;
      }
    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
      StopWatch();
    }
  }
}