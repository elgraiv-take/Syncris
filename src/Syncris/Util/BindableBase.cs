using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Syncris.Util
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage,T value,[CallerMemberName]string name = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                RaisePropertyChanged(name);
                return true;
            }
            return false;
        }
        protected bool SetProperty<T>(ref T storage, T value, params string[] relatedProperties)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                foreach(var name in relatedProperties)
                {
                    RaisePropertyChanged(name);
                }
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
