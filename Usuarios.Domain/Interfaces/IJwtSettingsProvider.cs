﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Infrastructure.Interfaces;

public interface IJwtSettingsProvider
{
    string SecretKey { get; }
}
