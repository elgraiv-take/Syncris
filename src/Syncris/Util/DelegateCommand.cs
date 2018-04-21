using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Syncris.Util
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool m_isExcecutable = true;
        public bool IsExcecutable {
            get
            {
                return m_isExcecutable;
            }
            set
            {
                if (m_isExcecutable != value)
                {
                    m_isExcecutable = value;
                    CanExecuteChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsExcecutable;
        }

        public DelegateCommand(Action action)
        {
            m_action = (o) => action();
        }
        public DelegateCommand(Action<object> action)
        {
            m_action = action;
        }

        private Action<object> m_action;

        public void Execute(object parameter)
        {
            m_action?.Invoke(parameter);
        }
    }
}
