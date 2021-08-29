using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;
using Exiled.API.Features;
using System.ComponentModel;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
}
