﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt.FactoryPattern
{
    public abstract class Factory
    {
        public abstract GameObject Create(string type);
    }
}
