using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;

namespace PokemonServerNotifier.Commands
{
    public class RefreshServerStatusCommand : CommandBase<RefreshServerStatusCommand>
    {
        private TaskbarIcon taskbarIcon;
        public override async void Execute(object parameter)
        {
            taskbarIcon = parameter as TaskbarIcon;
            FancyBalloon balloon = new FancyBalloon();
            PokemonGOServerChecker checker = new PokemonGOServerChecker();
            var status = await checker.CheckServerStatus();
            balloon.BalloonText = status.ToString();
            var window = (GetTaskbarWindow(parameter) as MainWindow);
            window.currentStatus = status;
            balloon.BulbImage.Source = new BitmapImage(new Uri("Icons/" + status.ToString() + ".png", UriKind.Relative));
            //show balloon and close it after 4 seconds
            (parameter as TaskbarIcon)?.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000);
            Timer timer = new Timer(5000);
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            taskbarIcon.CloseBalloon();
        }
    }
}
