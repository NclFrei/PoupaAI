﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Infrastructure.Configuration;

public class JWTSettings
{
    public string SecretKey { get; set; } = string.Empty;
}
