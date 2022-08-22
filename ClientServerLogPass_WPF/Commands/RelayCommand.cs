using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLogPass_WPF.Commands
{
    public class RelayCommand : CommandBase
    {
        private Action<string> _execute;
        public RelayCommand(Action<string> execute)
        {
            _execute = execute;
        }
        public override void Execute(object parameter)
        {
            _execute.Invoke(parameter as string);
        }
    }
}
