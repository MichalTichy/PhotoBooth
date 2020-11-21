using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.DAL
{
    public class UnitOfWorkRegistry : UnitOfWorkRegistryBase
    {
        protected override Stack<IUnitOfWork> GetStack()
        {
            var temp = new Stack<IUnitOfWork>();
            var t = AlternateRegistry;
            temp.Push(GetCurrent());
            while (this.AlternateRegistry != null)
            {
                temp.Push(t.GetCurrent());
                t = AlternateRegistry;
            }
            return temp; 
        }

    }
}
