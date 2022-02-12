using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel.Attributes
{
    public class JoiningTableAttribute : Attribute
    {
        public JoiningTableAttribute(params string[] tables)
        {

        }
    }
}
