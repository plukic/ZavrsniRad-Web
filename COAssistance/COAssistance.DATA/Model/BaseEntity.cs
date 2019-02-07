using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
