using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using ReactiveUI;

namespace Caliburn.Micro
{
    public class PropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
        public virtual bool IsNotifying
        {
            get { return AreChangeNotificationsEnabled(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
        public virtual void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {

            if (IsNotifying)
            {
                if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
                {
                    Execute.OnUIThread(() => this.RaisePropertyChanged(propertyName));
                }
                else
                {
                    this.RaisePropertyChanged(propertyName);
                }
            }

        }


        public virtual void Refresh()
        {
            NotifyOfPropertyChange(string.Empty);
        }
    }


}
