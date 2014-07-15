﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Localization.Resources;
using Microsoft.Practices.ServiceLocation;
using Tools;
using TrackApi.Classes;

namespace Track.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        #region properties
        private readonly INavigationService _navigationService;
        public RelayCommand ConnectionViewCommand { get; private set; }
        private readonly Helper _helper;

        public const string StationsPropertyName = "Stations";
        private ObservableCollection<string> _stations;
        public ObservableCollection<string> Stations
        {
            get
            {
                return _stations;
            }

            set
            {
                if (_stations == value)
                {
                    return;
                }

                _stations = value;
                RaisePropertyChanged(StationsPropertyName);
            }
        }

        public const string DatePropertyName = "Date";
        private string _date = DateTime.Now.ToString("MM/dd/yyyy");
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged(DatePropertyName);
            }
        }

        public const string TimePropertyName = "Time";
        private string _time = DateTime.Now.ToShortTimeString();
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                RaisePropertyChanged(TimePropertyName);
            }
        }

        public const string FromPropertyName = "From";
        private string _from;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                RaisePropertyChanged(FromPropertyName);
            }
        }

        public const string ToPropertyName = "To";
        private string _to;
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
                RaisePropertyChanged(ToPropertyName);
            }
        }
        #endregion

        public SearchViewModel(INavigationService navigationService)
        {
            _helper = new Helper();
            _navigationService = navigationService;
            Stations = new ObservableCollection<string>();
            ConnectionViewCommand = new RelayCommand(() =>
            {
                if (!CheckForValidValues()) 
                    return;
                var station = new Station { TimeStamp = DateTime.Now, Name = From };
                Deployment.Current.Dispatcher.BeginInvoke(() => ServiceLocator.Current.GetInstance<StationOverviewViewModel>().Station = station);
                From = string.Empty;
                _navigationService.NavigateTo(ViewModelLocator.StationOverviewPageUri);
            });
        }

        private bool CheckForValidValues()
        {
            if (!string.IsNullOrWhiteSpace(From) && Stations.Contains(From.Trim())) 
                return true;
            Deployment.Current.Dispatcher.BeginInvoke(() => Message.ShowToast(AppResources.MessageValidStationName));
            return false;
        }
    }
}
