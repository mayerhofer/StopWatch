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
    private const string startTime = "00 :: 00 :: 00";
    private string currentTime = startTime;

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
      Start.IsEnabled = true;
      Stop.IsEnabled = false;
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
      StopWatch();

      stopWatch.Reset();
      DisplayTime.Text = currentTime = startTime;
    }

    private void Start_Click(object sender, RoutedEventArgs e)
    {
      if (!stopWatch.IsRunning)
      {
        stopWatch.Start();
        timer.Start();
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