using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokemonServerNotifier.Commands
{
    public class ExitApplicationCommand : CommandBase<ExitApplicationCommand>
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
