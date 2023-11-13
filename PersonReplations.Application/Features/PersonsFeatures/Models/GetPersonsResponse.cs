﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Features.PersonsFeatures.Models
{
  public class GetPersonsResponse : PaginationInfo
  {
    public IEnumerable<PersonsListItem> Persons { get; set; } = new List<PersonsListItem>();
  }
}
