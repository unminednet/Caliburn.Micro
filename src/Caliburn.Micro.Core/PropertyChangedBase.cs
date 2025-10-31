using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading;
using ReactiveUI;

namespace Caliburn.Micro
{


    /// <summary>
    ///     PropertyChangedBase implementation inherited from ReactiveUI.ReactiveObject
    ///
    ///     BREAKING CHANGE: UI thread synchronization does not work except for Refresh() and NotifyOfPropertyChange()
    ///
    ///     Another solution would be to implement IReactiveObject in the original
    ///     PropertyChangedBase class, but that does not work well with AOT
    /// </summary>
    /// <seealso cref="ReactiveUI.ReactiveObject" />
    /// <seealso cref="ReactiveUI.IReactiveObject" />
    /// <seealso cref="Caliburn.Micro.INotifyPropertyChangedEx" />
    public class PropertyChangedBase : ReactiveObject, INotifyPropertyChangedEx
    {
        public virtual bool IsNotifying
        {
            get { return AreChangeNotificationsEnabled(); }
            set { throw new NotSupportedException("Use DelayChangeNotifications()"); }
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
