﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.ComponentModel.Composition;

namespace WorkBalance.ViewModel
{
    public class ViewModelBase : ReactiveObject, IPartImportsSatisfiedNotification
    {
        [Import]
        public IMessageBus MessageBus { get; set; }

        #region Implementation of IPartImportsSatisfiedNotification

        public virtual void OnImportsSatisfied()
        {
        }

        #endregion
    }
}
