﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typer
{
    public interface ITimeProvider
    {
        DateTime GetUtcNow();
    }
}
