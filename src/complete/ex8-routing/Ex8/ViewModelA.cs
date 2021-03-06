﻿using System;
using ReactiveUI;
using Splat;

namespace Ex8
{
    public class ViewModelA : ReactiveObject,  IRoutableViewModel
    {
        public ViewModelA(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            ViewModelInstanceCount.Increment();
            ViewModelInstanceId = ViewModelInstanceCount.InstanceCount;

            //NOTE: although it's very tempting to use this.WhenNavigatedTo() to
            // prime data and stuff, don't do that, only use it for the purposes
            // of aquiring and releasing disposable resources. For example, it's
            // reasonable to use it to acquire a subscription to hot observables
            // where the subscription is required while the view this view model
            // reprents is actually on screen.
            //
            // Instead, define a ReactiveCommand which does the work and trigger
            // it when the view model is attached to the view. Look at the ViewA
            // constructor for how to trigger this command.
            GetDataAndStuffCommand = ReactiveCommand.Create();
            GetDataAndStuffCommand.Subscribe(_ => this.Log().Info("GettingDataAndStuff in ViewModel A"));
        }

        public string ViewModelInstanceId { get; private set; }

        public ReactiveCommand<object> GetDataAndStuffCommand { get; private set; }

        public string UrlPathSegment { get { return "something"; } }
        public IScreen HostScreen { get; private set; }
    }
}
