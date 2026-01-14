using Avalonia.Controls;
using Avalonia.Interactivity;
using Lopushok.Hardik.Connector;
using Lopushok.Hardik.Dao;
using Lopushok.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lopushok.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
}