using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace PokemonServerNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServerStatus currentStatus = ServerStatus.Unknown;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Timer timer = new Timer(10000);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            this.Hide();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(async () =>
           {
               FancyBalloon balloon = new FancyBalloon();
               PokemonGOServerChecker checker = new PokemonGOServerChecker();
               var status = await checker.CheckServerStatus();
               if (status == currentStatus)
                   return;
               currentStatus = status;
               balloon.BulbImage.Source = new BitmapImage(new Uri("Icons/" + status.ToString() + ".png", UriKind.Relative));
               balloon.BalloonText = status.ToString();

                //show balloon and close it after 4 seconds
               NotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
               Timer closeTimer = new Timer(5000);
               closeTimer.AutoReset = false;
               closeTimer.Elapsed += CloseTimer_Elapsed;
               closeTimer.Start();

           }));

        }

        private void CloseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NotifyIcon.CloseBalloon();
        }
    }
}
