﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Win32;
namespace WeChatExport.ViewModel
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public string DbPath { get; set; }
        public ICommand Launch
        {
            get
            {
                return new DevExpress.Mvvm.DelegateCommand<string>(f =>
                {
                    
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
