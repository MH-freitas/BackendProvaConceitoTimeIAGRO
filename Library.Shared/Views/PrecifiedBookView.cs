using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Shared.Views
{
    public record PrecifiedBookView
    (
      int Id,
      string? Name,
      double Price,
      double PriceWithShipping
    );

}
