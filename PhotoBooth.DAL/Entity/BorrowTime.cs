using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.DAL.Entity
{
    public class BorrowTime : EntityBase
    {
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
    }
}
