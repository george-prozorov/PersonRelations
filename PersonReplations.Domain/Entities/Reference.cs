using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Domain.Entities
{
  public class Reference : EntityBase
  {
    public ReferenceTypes ReferenceTypeId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
  }
}
