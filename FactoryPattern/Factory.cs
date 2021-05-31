using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt.FactoryPattern
{
    public abstract class Factory
    {
        public abstract Enemy Create(string type);
    }
}